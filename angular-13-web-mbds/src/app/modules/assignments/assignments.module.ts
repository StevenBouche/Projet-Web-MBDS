import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AssignmentsComponent } from './assignments.component';
import { AssignmentListComponent, } from './list/list.component';
import { AssignmentDetailsComponent } from './details/details.component';
import { SharedModule } from 'app/shared/shared.module';
import { RouterModule } from '@angular/router';
import { AssignmentsRoutes } from './assignments.routing';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { AssignmentCreateComponent } from './create/create.component';
import { EditComponent } from './edit/edit.component';
import { MatInputModule } from '@angular/material/input';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { NavcomponentModule } from 'app/shared/navcomponent/navcomponent.module';
import { MatCardModule } from '@angular/material/card';


@NgModule({
  declarations: [
    AssignmentsComponent,
    AssignmentListComponent,
    AssignmentDetailsComponent,
    AssignmentCreateComponent,
    EditComponent
  ],
  imports: [
RouterModule.forChild(AssignmentsRoutes),
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
    MatCardModule
  ]
})
export class AssignmentsModule { }
