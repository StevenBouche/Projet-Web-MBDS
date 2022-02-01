import { NgModule } from "@angular/core";
import { UsersComponent } from "./users.component";
import { SharedModule } from "app/shared/shared.module";
import { RouterModule } from "@angular/router";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { MatCheckboxModule } from "@angular/material/checkbox";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatButtonModule } from "@angular/material/button";
import { EditComponent } from "./edit/edit.component";
import { MatInputModule } from "@angular/material/input";
import { MatIconModule } from '@angular/material/icon';
import { MatAutocompleteModule } from "@angular/material/autocomplete";
import { MatDatepickerModule } from "@angular/material/datepicker";
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";

@NgModule({
  declarations: [
    UsersComponent,
    EditComponent,
  ],
  imports: [
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
  ],
})
export class UsersModule {}
