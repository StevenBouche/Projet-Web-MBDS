import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { AuthorizeService } from '../authorize/authorize.service';
import { AuthModule } from '../authentification/auth.module';

@NgModule({
    imports  : [
        HttpClientModule,
        AuthModule
    ],
    providers: [
        AuthorizeService,
    ]
})
export class AuthorizeModule
{
}
