import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Dosya } from 'src/app/models/Dosya';
import { Kategori } from 'src/app/models/Kategori';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-kategori',
  templateUrl: './kategori.component.html',
  styleUrls: ['./kategori.component.css']
})
export class KategoriComponent implements OnInit {
dosyalar:Dosya[];
katId:number;
kat:Kategori;
  constructor(
    public apiServis:ApiService,
    public route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(p => {
      if(p['katId']){
        this.katId=p['katId'];
        this.KategoriById();
        this.DosyaListeByKatId();
      }
    });
  }

  KategoriById(){
    this.apiServis.KategoriById(this.katId).subscribe((d:any)=>{
      this.kat = d;
    })
  }

  DosyaListeByKatId(){
    this.apiServis.DosyaListeByKatId(this.katId).subscribe((d:any)=>{
      this.dosyalar= d;
    })
  }
}
