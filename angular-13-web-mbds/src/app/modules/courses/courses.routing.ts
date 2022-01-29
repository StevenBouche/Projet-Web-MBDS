import { Route } from '@angular/router';
import { CoursesComponent } from './courses.component';
import { CourseCreateComponent } from './create/create.component';
import { CourseDetailsComponent } from './details/details.component';
import { CourseListComponent } from './list/list.component';

export const coursesRoutes: Route[] = [
  {
    path: '',
    component: CoursesComponent,
    children: [
      
    ]
  }
];
