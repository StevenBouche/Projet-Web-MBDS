import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthentificationService } from './authentification.service';
import { AuthUtils } from './auth.utils';

@Injectable()
export class AuthInterceptor implements HttpInterceptor
{
    /**
     * Constructor
     */
    constructor(private _authService: AuthentificationService)
    {
    }

    /**
     * Intercept
     *
     * @param req
     * @param next
     */
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>>
    {
        // Clone the request object
        let newReq = req.clone();

        // Request
        //
        // If the access token didn't expire, add the Authorization header.
        // We won't add the Authorization header if the access token expired.
        // This will force the server to return a "401 Unauthorized" response
        // for the protected API routes which our response interceptor will
        // catch and delete the access token from the local storage while logging
        // the user out from the app.
        const token = this._authService.getAuth();
        const jwtToken = token?.jwtToken.accessToken;

        console.log(jwtToken)
        
        if (jwtToken && jwtToken && !AuthUtils.isTokenExpired(jwtToken) )
        {
            newReq = req.clone({
                headers: req.headers.set('Authorization', 'Bearer ' + jwtToken)
            });
        }

        // Response
        return next.handle(newReq);
    }
}
