import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Dosya } from 'src/app/models/Dosya';
import { Sonuc } from 'src/app/models/Sonuc';
import { Yorum } from 'src/app/models/Yorum';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-dosya',
  templateUrl: './dosya.component.html',
  styleUrls: ['./dosya.component.css']
})
export class DosyaComponent implements OnInit {
  dosyaId:number;
  dosya: Dosya;
  yorumlar:Yorum[];
  constructor(
    public apiServis:ApiService,
    public route:ActivatedRoute
  ) { }

  ngOnInit() {
    this.route.params.subscribe(p=>{
      if(p['dosyaId']){
        this.dosyaId=p['dosyaId'];
        this.DosyaById();
        this.DosyaYorumListe();
      }
    })
  }
  DosyaById(){
    this.apiServis.DosyaById(this.dosyaId).subscribe((d: any)=>{
      this.dosya= d;
      this.DosyaGoruntulendiYap();
    });
  }
  DosyaGoruntulendiYap(){
    this.dosya.Goruntulenme += 1;
    this.apiServis.DosyaDuzenle(this.dosya).subscribe();
  }
  DosyaYorumListe(){
    this.apiServis.YorumListeByDosyaId(this.dosyaId).subscribe((d:any)=>{
      this.yorumlar =d;
    });
  }
  YorumEkle(yorumMetni:string){
    var yorum: Yorum = new Yorum();
    var uyeId:number =parseInt(localStorage.getItem("uid"));
    yorum.DosyaId=this.dosyaId;
    yorum.UyeId=uyeId;
    yorum.YorumIcerik = yorumMetni;
    yorum.Tarih =new Date();

    this.apiServis.YorumEkle(yorum).subscribe((d: any)=>{
      if(d.islem){
        this.DosyaYorumListe();
      }
    });
  }
}
