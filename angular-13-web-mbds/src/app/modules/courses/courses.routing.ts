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
      {
        path: '',
        component: CourseListComponent,
        resolve: {
          //tasks    : AdvertsResolver
        }
      },
      {
        path: 'create',
        component: CourseCreateComponent,
        resolve: {
          //tasks    : AdvertsResolver
        }
      },
      {
        path: 'details/:id',
        component: CourseDetailsComponent,
        resolve: {
          //tasks    : AdvertsResolver
        }
      },
      {
        path: 'edit/:id',
        component: CourseDetailsComponent,
        resolve: {
          //tasks    : AdvertsResolver
        }
      }
    ]
  }
];
