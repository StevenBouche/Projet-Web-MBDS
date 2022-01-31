import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { UsersService } from './users.service';

@NgModule({
    imports  : [
        HttpClientModule
    ],
    providers: [
      UsersService
    ]
})
export class UsersModule
{
}
