import { Route } from '@angular/router';
import { AssignmentCreateComponent } from './create/create.component';
import { AssignmentDetailsComponent } from './details/details.component';
import { EditComponent } from './edit/edit.component';
import { AssignmentListComponent } from './list/list.component';
import { AssignmentDetailsResolver } from './resolvers/assignmentdetails.resolver';
import { AssignmentEditResolver } from './resolvers/assignmentedit.resolver';

export const AssignmentsRoutes: Route[] = [
  //{ path: 'list', component: AssignmentListComponent },
  { path: 'details/:id', resolve : { initialData: AssignmentDetailsResolver }, component: AssignmentDetailsComponent },
  { path: 'edit/:id', resolve : { initialData: AssignmentEditResolver },component: EditComponent },
  { path: 'create', component: AssignmentCreateComponent }
];
