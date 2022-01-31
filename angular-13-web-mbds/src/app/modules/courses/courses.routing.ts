import { NgModule } from '@angular/core';
import { Route, RouterModule } from '@angular/router';
import { CoursesComponent } from './courses.component';
import { CourseCreateComponent } from './create/create.component';
import { CourseDetailsComponent } from './details/details.component';
import { CourseListComponent } from './list/list.component';

const coursesRoutes: Route[] = [

];

@NgModule(
  {
    imports: [RouterModule.forChild(coursesRoutes)],
  }
)
export class CoursesRoutingModule {}
