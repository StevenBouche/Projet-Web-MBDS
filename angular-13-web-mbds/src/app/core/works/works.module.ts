import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { WorksService } from './works.service';

@NgModule({
    imports  : [
        HttpClientModule
    ],
    providers: [
        WorksService
    ]
})
export class WorksModule
{
}
