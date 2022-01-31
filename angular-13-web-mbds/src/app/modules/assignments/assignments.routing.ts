import { Route } from '@angular/router';
import { AssignmentCreateComponent } from './create/create.component';
import { AssignmentDetailsComponent } from './details/details.component';
import { EditComponent } from './edit/edit.component';
import { AssignmentListComponent } from './list/list.component';

export const AssignmentsRoutes: Route[] = [
  { path: 'list', component: AssignmentListComponent },
  { path: 'details/:id', component: AssignmentDetailsComponent },
  { path: 'edit/:id', component: EditComponent },
  { path: 'create', component: AssignmentCreateComponent }
];
