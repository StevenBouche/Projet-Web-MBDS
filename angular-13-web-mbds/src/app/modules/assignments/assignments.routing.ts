import { Route } from '@angular/router';
import {AssignmentsComponent } from './assignments.component';
import { AssignmentCreateComponent } from './create/create.component';
import { AssignmentDetailsComponent } from './details/details.component';
import { AssignmentListComponent } from './list/list.component';

export const AssignmentsRoutes: Route[] = [
  {
    path: '',
    component:AssignmentsComponent,
    children: [

    ]
  }
];
