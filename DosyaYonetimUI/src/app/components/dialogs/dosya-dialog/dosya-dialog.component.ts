import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Dosya } from 'src/app/models/Dosya';
import { Kategori } from 'src/app/models/Kategori';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-dosya-dialog',
  templateUrl: './dosya-dialog.component.html',
  styleUrls: ['./dosya-dialog.component.css']
})
export class DosyaDialogComponent implements OnInit {
  dialogBaslik:string;
  yeniKayit:Dosya;
  islem:string;
  frm:FormGroup;
  kategoriler:Kategori[];
  
  constructor(
    public dialogRef:MatDialogRef<DosyaDialogComponent>,
    public frmBuild:FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data : any,
    public apiServis:ApiService
  ) { 

    this.islem = data.islem;
    if(this.islem=="ekle"){
      this.dialogBaslik="Dosya Ekle"
      this.yeniKayit=new Dosya();
    }
    if(this.islem=="duzenle"){
      this.dialogBaslik="Dosya Düzenle"
      this.yeniKayit=data.kayit;
    }
    if(this.islem=="detay"){
      this.dialogBaslik="Dosya İndirme İşlemi"
      this.yeniKayit=data.kayit;
    }
    this.frm=this.FormOlustur();
  }

  ngOnInit(): void {
    this.KategoriListele();
  }
  FormOlustur(){
    return this.frmBuild.group({
      Adi: [this.yeniKayit.Adi],
      Uzanti: [this.yeniKayit.Uzanti],
      KategoriId: [this.yeniKayit.KategoriId],     
    });
  }
  KategoriListele(){
    this.apiServis.KategoriListe().subscribe((d: any)=>{
      this.kategoriler = d;
    });
  }
}
