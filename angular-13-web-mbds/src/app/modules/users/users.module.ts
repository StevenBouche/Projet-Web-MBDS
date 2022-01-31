import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { UsersComponent } from "./users.component";
import { UserDetailsComponent } from "./details-edit/details.component";
import { SharedModule } from "app/shared/shared.module";
import { RouterModule } from "@angular/router";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { MatCheckboxModule } from "@angular/material/checkbox";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatIconModule } from "@angular/material/icon";
import { MatButtonModule } from "@angular/material/button";
import { EditComponent } from "./edit/edit.component";
import { MatInputModule } from "@angular/material/input";
import { MatAutocompleteModule } from "@angular/material/autocomplete";
import { MatDatepickerModule } from "@angular/material/datepicker";
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";
import { NavcomponentModule } from "app/shared/navcomponent/navcomponent.module";

import { UsersRoutingModule } from "./users-routing.module";

@NgModule({
  declarations: [
    UsersComponent,
    UserDetailsComponent,
    EditComponent,
  ],
  imports: [
    UsersRoutingModule,
    CommonModule,
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
  ],
})
export class UsersModule {}
