import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialogRef, MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Dosya } from 'src/app/models/Dosya';
import { AlertService } from 'src/app/services/alert.service';
import { ApiService } from 'src/app/services/api.service';
import { ConfirmDialogComponent } from '../../dialogs/confirm-dialog/confirm-dialog.component';
import { DosyaDialogComponent } from '../../dialogs/dosya-dialog/dosya-dialog.component';
import { Kategori } from 'src/app/models/Kategori';
import { ActivatedRoute } from '@angular/router';
import { Sonuc } from 'src/app/models/Sonuc';

@Component({
  selector: 'app-admin-dosya',
  templateUrl: './admin-dosya.component.html',
  styleUrls: ['./admin-dosya.component.css']
})
export class AdminDosyaComponent implements OnInit {
  dosyalar: Dosya[];
  dataSource: any;
  kategoriler : Kategori[];
  katId: number;
  secKat:Kategori;
  uyeId:number;
  displayedColumns=['Adi','Uzanti','Tarih','UyeKadi','Goruntulenme','detay'];
  @ViewChild(MatSort) sort:MatSort;
  @ViewChild(MatPaginator) paginator:MatPaginator;
  dialogRef:MatDialogRef<DosyaDialogComponent>;
  dialogRefConfirm:MatDialogRef<ConfirmDialogComponent>;

  constructor(
    public apiServis:ApiService,
    public matDialog:MatDialog,
    public alert:AlertService,
    public route:ActivatedRoute
  ) { }

  ngOnInit()  {
    this.KategoriListele();
    this.uyeId=parseInt(localStorage.getItem("uid"));
    this.route.params.subscribe(p=>{
      if(p['katId']){
        this.katId = p['katId'];
        this.KategoriById();
      }
    })
  }

  KategoriById(){
    this.apiServis.KategoriById(this.katId).subscribe((d: any) =>{
      this.secKat= d;
      this.DosyaListele();
    })
  }

  DosyaListele(){
    this.apiServis.DosyaListeByKatId(this.katId).subscribe((d: any)=>{
      this.dosyalar = d;
      this.dataSource = new MatTableDataSource(d);
      this.dataSource.sort=this.sort;
      this.dataSource.paginator=this.paginator;
    });
  }

  KategoriListele(){
    this.apiServis.KategoriListe().subscribe((d: any)=>{
      this.kategoriler = d;
    });
  }

  KategoriSec(kat:Kategori){
    this.katId = kat.KategoriId;
    this.DosyaListele();
  }

  Ekle(){
    var yeniKayit:Dosya=new Dosya();
    this.dialogRef=this.matDialog.open(DosyaDialogComponent,{
      width:'800px',
      data:{
        kayit:yeniKayit,
        islem:'ekle'
      }
    });
    this.dialogRef.afterClosed().subscribe(d=>{
      if(d){
        yeniKayit =d;
        yeniKayit.Tarih=new Date();
        yeniKayit.Goruntulenme=0;
        yeniKayit.UyeId = this.uyeId;
        this.apiServis.DosyaEkle(yeniKayit).subscribe((s:any)=>{
          this.alert.AlertUygula(s);
          if(s.islem){
            this.DosyaListele();
          }
        })
      }
    });
  }

  Duzenle(kayit : Dosya){

    this.dialogRef=this.matDialog.open(DosyaDialogComponent,{
      width:'800px',
      data:{
        kayit:kayit,
        islem:'duzenle'
      }
    });
    this.dialogRef.afterClosed().subscribe(d=>{
      if(d){
        kayit.Adi = d.Adi;
        this.apiServis.DosyaDuzenle(kayit).subscribe((s:any)=>{
          this.alert.AlertUygula(s);
          if(s.islem){
            this.DosyaListele();
          }
        });
      }
    });
  }
  
  Detay(kayit : Dosya){

    this.dialogRef=this.matDialog.open(DosyaDialogComponent,{
      width:'800px',
      data:{
        kayit:kayit,
        islem:'detay'
      }
    });

  }

  Sil(kayit: Dosya) {
    this.dialogRefConfirm = this.matDialog.open(ConfirmDialogComponent,{
      width:'400px'
    })
    this.dialogRefConfirm.componentInstance.dialogMesaj=kayit.Adi + " Dosyası silinecektir, onaylıyor musunuz ?";
    this.dialogRefConfirm.afterClosed().subscribe(d=>{
      if(d){
        this.apiServis.DosyaSil(kayit.DosyaId).subscribe((s:any)=>{
          this.alert.AlertUygula(s);
          if(s.islem){
            this.DosyaListele();
          }
        });
      }
    })
  }
}
