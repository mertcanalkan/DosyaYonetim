import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material.module';
import { HomeComponent } from './components/home/home.component';
import { MainNavComponent } from './components/main-nav/main-nav.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AlertDialogComponent } from './components/dialogs/alert-dialog/alert-dialog.component';
import { ConfirmDialogComponent } from './components/dialogs/confirm-dialog/confirm-dialog.component';
import { ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './components/Login/login.component';
import { AdminMainComponent } from './components/Admin/admin-main/admin-main.component';
import { AdminUyeComponent } from './components/Admin/admin-uye/admin-uye.component';
import { AdminDosyaComponent } from './components/Admin/admin-dosya/admin-dosya.component';
import { AdminKategoriComponent } from './components/Admin/admin-kategori/admin-kategori.component';
import { KategoriDialogComponent } from './components/dialogs/kategori-dialog/kategori-dialog.component';
import { DosyaDialogComponent } from './components/dialogs/dosya-dialog/dosya-dialog.component';
import { SafeHTMLPipe } from './pipes/safeHtml-pipe.pipe';
import { AlertService } from './services/alert.service';
import { ApiService } from './services/api.service';
import { AuthInterceptor } from './services/Authinterceptor';
import { AuthGuard } from './services/AuthGuard';
import { DosyaComponent } from './components/Dosya/dosya.component';
import { KategoriComponent } from './components/Kategori/kategori.component';
import { UyedosyaComponent } from './components/uyedosya/uyedosya.component';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    MainNavComponent,
    LoginComponent,
    DosyaComponent,
    KategoriComponent,
    UyedosyaComponent,
    //Admin
    AdminUyeComponent,
    AdminDosyaComponent,
    AdminKategoriComponent,
    AdminMainComponent,
    //Dialoglar
    KategoriDialogComponent,
    AlertDialogComponent,
    ConfirmDialogComponent,
    DosyaDialogComponent,
    SafeHTMLPipe,
 

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    HttpClientModule,
    ReactiveFormsModule,

    
  ],
  entryComponents: [
    AlertDialogComponent,
    ConfirmDialogComponent,
    KategoriDialogComponent

  ],
  providers: [AlertService,ApiService,SafeHTMLPipe, AuthGuard,
  {provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true},
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
