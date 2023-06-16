import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ApiService } from 'src/app/services/api.service';
import { ConfirmDialogComponent } from '../dialogs/confirm-dialog/confirm-dialog.component';
import { Dosya } from 'src/app/models/Dosya';
import { DosyaDialogComponent } from '../dialogs/dosya-dialog/dosya-dialog.component';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],

})
export class HomeComponent implements OnInit {
  dosyalar:Dosya[];
  confirmDialogRef: MatDialogRef<ConfirmDialogComponent>;
  dialogRef: any;
  matDialog: any;
  constructor(
    public apiServis : ApiService
  ) { }

  ngOnInit() {
    this.SonEklenenler();
  }
  SonEklenenler(){
    this.apiServis.DosyaListeSonEklenenler(5).subscribe((d:any)=>{
      this.dosyalar = d;
    });
  }
}
