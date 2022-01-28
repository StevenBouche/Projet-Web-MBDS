import { NgModule } from '@angular/core';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AuthorizeService } from '../authorize/authorize.service';
import { AuthentificationService } from './authentification.service';
import { AuthInterceptor } from './auth.interceptor';

@NgModule({
    imports  : [
        HttpClientModule
    ],
    providers: [
        AuthentificationService,
        AuthorizeService,
        {
            provide : HTTP_INTERCEPTORS,
            useClass: AuthInterceptor,
            multi   : true
        }
    ]
})
export class AuthModule
{
}
