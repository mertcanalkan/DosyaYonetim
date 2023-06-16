import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/Login/login.component';
import { AdminMainComponent } from './components/Admin/admin-main/admin-main.component';
import { AdminDosyaComponent } from './components/Admin/admin-dosya/admin-dosya.component';
import { AdminUyeComponent } from './components/Admin/admin-uye/admin-uye.component';
import { AdminKategoriComponent } from './components/Admin/admin-kategori/admin-kategori.component';
import { AuthGuard } from './services/AuthGuard';
import { DosyaComponent } from './components/Dosya/dosya.component';
import { KategoriComponent } from './components/Kategori/kategori.component';
import { UyedosyaComponent } from './components/uyedosya/uyedosya.component';

const routes: Routes = [
  {
    path: '', 
    component: HomeComponent
  },
  {
    path: 'login',
    component:LoginComponent
  },
  {
    path: 'dosya/:dosyaId',
    component: DosyaComponent
  },
  {
    path: 'kategori/:katId',
    component: KategoriComponent
  },
  {
    path: 'uyedosya/:uyeId',
    component: UyedosyaComponent
  },
  {
    path: 'admin/main',
    component:AdminMainComponent,
    canActivate: [AuthGuard],
    data:{
      yetkiler:['Admin'],
      gerigit: '/login'
    }
  },
  {
    path: 'admin/kategori',
    component:AdminKategoriComponent,
    canActivate: [AuthGuard],
    data:{
      yetkiler:['Admin'],
      gerigit: '/login'
    }
  },
  {
    path: 'admin/dosya',
    component:AdminDosyaComponent
  },
  {
    path: 'admin/dosya/:katId',
    component:AdminDosyaComponent
  },
  {
    path: 'admin/uye',
    component:AdminUyeComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
