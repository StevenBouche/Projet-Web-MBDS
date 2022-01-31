import { NgModule } from '@angular/core';
import { Route, RouterModule } from '@angular/router';
import { CourseCreateComponent } from './create/create.component';
import { CourseDetailsComponent } from './details/details.component';
import { EditComponent } from './edit/edit.component';
import { CourseListComponent } from './list/list.component';
import { CourseDetailsResolver } from './resolvers/coursedetails.resolver';
import { CourseEditResolver } from './resolvers/courseedit.resolver';

const coursesRoutes: Route[] = [
  { path: 'list', component: CourseListComponent },
  { path: 'details/:id', resolve : { initialData: CourseDetailsResolver }, component: CourseDetailsComponent },
  { path: 'edit/:id', resolve : { initialData: CourseEditResolver }, component: EditComponent },
  { path: 'create', component: CourseCreateComponent }
];

@NgModule(
  {
    imports: [RouterModule.forChild(coursesRoutes)],
  }
)
export class CoursesRoutingModule { }
