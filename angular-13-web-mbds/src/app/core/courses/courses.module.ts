import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { CoursesService } from './courses.service';

@NgModule({
    imports  : [
        HttpClientModule
    ],
    providers: [
        CoursesService
    ]
})
export class CoursesModule
{
}
