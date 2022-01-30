import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavcomponentComponent } from './navcomponent.component';
import { SharedModule } from '../shared.module';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';



@NgModule({
  declarations: [
    NavcomponentComponent
  ],
  imports: [
    SharedModule,
    MatButtonModule,
    MatIconModule
  ],
  exports: [
    NavcomponentComponent
  ]
})
export class NavcomponentModule { }
