import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { AssignmentsService } from './assignments.service';

@NgModule({
    imports  : [
        HttpClientModule
    ],
    providers: [
      AssignmentsService
    ]
})
export class AssignmentsModule
{
}
