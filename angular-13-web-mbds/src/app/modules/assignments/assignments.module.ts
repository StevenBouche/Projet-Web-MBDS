import { AssignmentCreateComponent } from "./create/create.component";
import { AssignmentDetailsComponent } from "./details/details.component";
import { AssignmentListComponent } from "./list/list.component";
import { AssignmentsComponent } from "./assignments.component";
import { AssignmentsRoutes } from "./assignments.routing";
import { CommonModule } from "@angular/common";
import { EditComponent } from "./edit/edit.component";
import { NavcomponentModule } from "app/shared/navcomponent/navcomponent.module";
import { NgModule } from "@angular/core";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { RouterModule } from "@angular/router";
import { SharedModule } from "app/shared/shared.module";
//MATERIAL
import { MatAutocompleteModule } from "@angular/material/autocomplete";
import { MatDatepickerModule } from "@angular/material/datepicker";
import { MatButtonModule } from "@angular/material/button";
import { MatCardModule } from "@angular/material/card";
import { MatCheckboxModule } from "@angular/material/checkbox";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatIconModule } from "@angular/material/icon";
import { MatInputModule } from "@angular/material/input";
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";
import { MatSlideToggleModule } from "@angular/material/slide-toggle";
import { WorkStudentComponent } from './work-student/work-student.component';
import { WorkProfessorComponent } from './work-professor/work-professor.component';
import { MatTabsModule } from '@angular/material/tabs';

import {DragDropModule} from '@angular/cdk/drag-drop';
import { WorkProfessorItemComponent } from './work-professor/work-professor-item/work-professor-item.component';
import { MatSortModule } from "@angular/material/sort";
import { MatTableModule } from "@angular/material/table";
import { ScrollingModule } from '@angular/cdk/scrolling';


@NgModule({
  declarations: [
    AssignmentsComponent,
    AssignmentListComponent,
    AssignmentDetailsComponent,
    AssignmentCreateComponent,
    EditComponent,
    WorkStudentComponent,
    WorkProfessorComponent,
    WorkProfessorItemComponent,
  ],
  imports: [
    RouterModule.forChild(AssignmentsRoutes),
    DragDropModule,
    SharedModule,
    RouterModule,
    NgbModule,
    MatCheckboxModule,
    MatFormFieldModule,
    MatIconModule,
    MatButtonModule,
    MatInputModule,
    MatAutocompleteModule,
    MatDatepickerModule,
    MatProgressSpinnerModule,
    NavcomponentModule,
    MatCardModule,
    MatSlideToggleModule,
    MatTabsModule,
    MatTableModule,
    MatSortModule
    MatTabsModule,
    ScrollingModule
  ]
})
export class AssignmentsModule {}
