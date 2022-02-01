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

export const routes: Route[] = [
  { path: 'list', component: WorkListComponent },
  { path: 'details/:id', component: WorkDetailsComponent },
  { path: 'edit/:id', component: WorkEditComponent },
  { path: 'create', component: WorkCreateComponent }
];


@NgModule({
  declarations: [
    WorkComponent,
    WorkListComponent,
    WorkEditComponent,
    WorkCreateComponent,
    WorkDetailsComponent
  ],
  imports: [
    RouterModule.forChild(routes),
    SharedModule,
    RouterModule,
    NavcomponentModule,
  ]
})
export class WorkModule { }
