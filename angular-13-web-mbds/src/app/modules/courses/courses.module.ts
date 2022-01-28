import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CoursesComponent } from './courses.component';
import { CourseListComponent, } from './list/list.component';
import { CourseDetailsComponent } from './details/details.component';
import { SharedModule } from 'app/shared/shared.module';
import { RouterModule } from '@angular/router';
import { coursesRoutes } from './courses.routing';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { CourseCreateComponent } from './create/create.component';
import { EditComponent } from './edit/edit.component';
import { MatInputModule } from '@angular/material/input';

@NgModule({
  declarations: [
    CoursesComponent,
    CourseListComponent,
    CourseDetailsComponent,
    CourseCreateComponent,
    EditComponent
  ],
  imports: [
    RouterModule.forChild(coursesRoutes),
    SharedModule,
    RouterModule,
    NgbModule,
    MatCheckboxModule,
    MatFormFieldModule,
    MatIconModule,
    MatButtonModule,
    MatInputModule
  ]
})
export class CoursesModule { }
