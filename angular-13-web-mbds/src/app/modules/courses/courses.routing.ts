import { NgModule } from '@angular/core';
import { Route, RouterModule } from '@angular/router';
import { CoursesComponent } from './courses.component';
import { CourseCreateComponent } from './create/create.component';
import { CourseDetailsComponent } from './details/details.component';
import { EditComponent } from './edit/edit.component';
import { CourseListComponent } from './list/list.component';
import { CourseResolver } from './resolvers/course.resolver';

const coursesRoutes: Route[] = [
  { path: 'list', component: CourseListComponent },
  { path: 'details/:id', resolve : { initialData: CourseResolver }, component: CourseDetailsComponent },
  { path: 'edit/:id', resolve : { initialData: CourseResolver }, component: EditComponent },
  { path: 'create', component: CourseCreateComponent }
];

@NgModule(
  {
    imports: [RouterModule.forChild(coursesRoutes)],
  }
)
export class CoursesRoutingModule { }
