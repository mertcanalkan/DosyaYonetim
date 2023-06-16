import { Component, OnInit } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { ApiService } from 'src/app/services/api.service';
import { Kategori } from 'src/app/models/Kategori';

@Component({
  selector: 'app-main-nav',
  templateUrl: './main-nav.component.html',
  styleUrls: ['./main-nav.component.css']
})
export class MainNavComponent implements OnInit{
  kadi :string;
  kategoriler : Kategori[]
  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );

  constructor(private breakpointObserver: BreakpointObserver,
    public apiServis:ApiService
    ) {}
  ngOnInit(): void {
    this.KategoriListele();
    if (this.apiServis.oturumKontrol){
      this.kadi = localStorage.getItem("kadi");
    };
  }
  OturumKapat(){
    localStorage.clear();
    location.href="/login"
  }
  KategoriListele(){
    this.apiServis.KategoriListe().subscribe((d : any) => {
      this.kategoriler = d;
    });
  }

}
