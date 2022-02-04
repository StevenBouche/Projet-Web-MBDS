import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WorkComponent } from './work.component';
import { WorkCreateComponent } from './create/create.component';
import { WorkDetailsComponent } from './details/details.component';
import { WorkEditComponent } from './edit/edit.component';
import { WorkListComponent } from './list/list.component';
import { SharedModule } from 'app/shared/shared.module';
import { Route, RouterModule } from '@angular/router';
import { NavcomponentModule } from 'app/shared/navcomponent/navcomponent.module';

//MATERIAL
import { MatAutocompleteModule } from "@angular/material/autocomplete";
import { MatDatepickerModule } from "@angular/material/datepicker";
import { MatButtonModule } from "@angular/material/button";
import { MatCardModule } from "@angular/material/card";
import { MatCheckboxModule } from "@angular/material/checkbox";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatIconModule } from "@angular/material/icon";
import { MatInputModule } from "@angular/material/input";
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";
import { StudentWorkComponent } from './student-work/student-work.component';

export const routes: Route[] = [
  { path: 'list', component: WorkListComponent },
 // { path: 'details/:id', component: WorkDetailsComponent },
 // { path: 'edit/:id', component: WorkEditComponent },
  { path: 'create', component: WorkCreateComponent }
];


@NgModule({
  declarations: [
    WorkComponent,
    WorkListComponent,
    WorkEditComponent,
    WorkCreateComponent,
    WorkDetailsComponent,
    StudentWorkComponent,
  ],
  imports: [
    RouterModule.forChild(routes),
    SharedModule,
    RouterModule,
    NavcomponentModule,
    MatAutocompleteModule,
    MatDatepickerModule,
    MatButtonModule,
    MatCardModule,
    MatCheckboxModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatProgressSpinnerModule,
  ]
})
export class WorkModule { }
