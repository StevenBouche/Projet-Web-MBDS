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

@NgModule({
  declarations: [
    AssignmentsComponent,
    AssignmentListComponent,
    AssignmentDetailsComponent,
    AssignmentCreateComponent,
    EditComponent,
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
    MatCardModule,
  ],
})
export class AssignmentsModule {}
