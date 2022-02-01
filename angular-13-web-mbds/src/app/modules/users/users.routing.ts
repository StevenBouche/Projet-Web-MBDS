import { NgModule } from '@angular/core';
import { Route, RouterModule } from '@angular/router';
import { EditComponent } from './edit/edit.component';

const usersRoutes: Route[] = [
  { path: 'edit', component: EditComponent },
];

@NgModule(
  {
    imports: [RouterModule.forChild(usersRoutes)],
  }
)
export class UsersRoutingModule { }
