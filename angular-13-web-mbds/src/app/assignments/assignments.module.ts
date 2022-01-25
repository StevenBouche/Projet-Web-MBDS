import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { Routes, RouterModule } from "@angular/router";

import { AssignmentsComponent } from "./assignments.component";

const routes: Routes = [
  {
    path: "",
    data: {
      title: "Home",
      urls: [{ title: "home", url: "/home" }, { title: "Home" }],
    },
    component: AssignmentsComponent,
  },
];

@NgModule({
  imports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    RouterModule.forChild(routes),
  ],
  declarations: [
  ],
})
export class AssignmentsModule {}
