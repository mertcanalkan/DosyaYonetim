import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Dosya } from 'src/app/models/Dosya';
import { Uye } from 'src/app/models/Uye';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-uyedosya',
  templateUrl: './uyedosya.component.html',
  styleUrls: ['./uyedosya.component.css']
})
export class UyedosyaComponent implements OnInit {

  dosyalar:Dosya[];
  uyeId:number;
  uye:Uye;
    constructor(
      public apiServis:ApiService,
      public route: ActivatedRoute
    ) { }
  
    ngOnInit(): void {
      this.route.params.subscribe(p => {
        if(p['uyeId']){
          this.uyeId=p['uyeId'];
          this.UyeById();
          this.DosyaListeByUyeId();
        }
      });
    }
  
    UyeById(){
      this.apiServis.UyeById(this.uyeId).subscribe((d:any)=>{
        this.uye = d;
      })
    }
  
    DosyaListeByUyeId(){
      this.apiServis.DosyaListeByUyeId(this.uyeId).subscribe((d:any)=>{
        this.dosyalar= d;
      })
    }
  }
