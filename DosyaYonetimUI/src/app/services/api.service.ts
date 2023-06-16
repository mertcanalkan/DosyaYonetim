import { Yorum } from './../models/Yorum';
import { Uye } from './../models/Uye';
import { Dosya } from '../models/Dosya';
import { Kategori } from './../models/Kategori';

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  apiUrl = "https://localhost:44313/api/";

  constructor(
    public http: HttpClient
  ) { }

  tokenAl(kadi: string,parola:string){
    var data ="username="+kadi+"&password="+parola+"&grant_type=password";
    var reqHeader = new HttpHeaders({"Content-Type":"application/x-www-form-urlencoded"});
    return this.http.post(this.apiUrl+"/token",data,{headers:reqHeader});
  }
  oturumKontrol() {
    if (localStorage.getItem("token")) {
      return true;
    }
    else {
      return false;
    }
  }

  yetkiKontrol(yetkiler: any[]) {
    var sonuc: boolean = false;

    var uyeYetkiler: string[] = JSON.parse(localStorage.getItem("uyeYetkileri"));

    if (uyeYetkiler) {
      yetkiler.forEach(element => {
        if (uyeYetkiler.indexOf(element) > -1) {
          sonuc = true;
        }
      });
    }

    return sonuc;
  }

  /*   Oturum İşlemleri Bitiş  */


  /*  API  */

  KategoriListe() {
    return this.http.get(this.apiUrl + "/kategoriliste/")
  }
  KategoriById(katId: number) {
    return this.http.get(this.apiUrl + "/kategoribyid/" + katId);
  }
  KategoriEkle(kat: Kategori) {
    return this.http.post(this.apiUrl + "/kategoriekle", kat);
  }
  KategoriDuzenle(kat: Kategori) {
    return this.http.put(this.apiUrl + "/kategoriduzenle", kat);
  }
  KategoriSil(katId: number) {
    return this.http.delete(this.apiUrl + "/kategorisil/" + katId);
  }

  DosyaListe() {
    return this.http.get(this.apiUrl + "/dosyaliste");
  }
  DosyaListeSonEklenenler(s: number) {
    return this.http.get(this.apiUrl + "/dosyalistesoneklenenler/" + s);
  }
  DosyaListeByKatId(katId: number) {
    return this.http.get(this.apiUrl + "/dosyalistebykatid/" + katId);
  }
  DosyaListeByUyeId(uyeId: number) {
    return this.http.get(this.apiUrl + "/dosyalistebyuyeid/" + uyeId);
  }
  DosyaById(dosyaId: number) {
    return this.http.get(this.apiUrl + "/dosyabyid/" + dosyaId);
  }
  DosyaEkle(dosya: Dosya) {
    return this.http.post(this.apiUrl + "/dosyaekle", dosya);
  }
  DosyaDuzenle(dosya: Dosya) {
    return this.http.put(this.apiUrl + "/dosyaduzenle", dosya);
  }
  DosyaSil(dosyaId: number) {
    return this.http.delete(this.apiUrl + "/dosyasil/" + dosyaId);
  }

  UyeListe() {
    return this.http.get(this.apiUrl + "/uyeliste");
  }
  UyeById(uyeId: number) {
    return this.http.get(this.apiUrl + "/uyebyid/" + uyeId);
  }
  UyeEkle(uye: Uye) {
    return this.http.post(this.apiUrl + "/uyeekle", uye);
  }
  UyeDuzenle(uye: Uye) {
    return this.http.put(this.apiUrl + "/uyeduzenle", uye);
  }
  UyeSil(uyeId: number) {
    return this.http.delete(this.apiUrl + "/uyesil/" + uyeId);
  }

  YorumListe() {
    return this.http.get(this.apiUrl + "/yorumliste");
  }
  YorumListeByUyeId(uyeId: number) {
    return this.http.get(this.apiUrl + "/yorumlistebyuyeid/" + uyeId);
  }
  YorumListeByDosyaId(yorumId: number) {
    return this.http.get(this.apiUrl + "/yorumlistebydosyaid/" + yorumId);
  }
  YorumListeSonEklenenler(s: number) {
    return this.http.get(this.apiUrl + "/yorumliste/" + s);
  }
  YorumById(yorumId: number) {
    return this.http.get(this.apiUrl + "/yorumbyid/" + yorumId);
  }
  YorumEkle(yorum: Yorum) {
    return this.http.post(this.apiUrl + "/yorumekle", yorum);
  }
  YorumDuzenle(yorum: Yorum) {
    return this.http.put(this.apiUrl + "/yorumduzenle", yorum);
  }
  YorumSil(yorumId: number) {
    return this.http.delete(this.apiUrl + "/yorumsil/" + yorumId);
  }
}
