import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthentificationService } from 'app/core/authentification/authentification.service';

@Component({
    selector     : 'auth-sign-in',
    templateUrl  : './sign-in.component.html',
    encapsulation: ViewEncapsulation.None,
})
export class AuthSignInComponent implements OnInit
{
    signInForm: FormGroup ;
    hide = true;

    constructor(
        private _activatedRoute: ActivatedRoute,
        private _authService: AuthentificationService,
        private _formBuilder: FormBuilder,
        private _router: Router
    )
    {
      // Create the form
      this.signInForm = this._formBuilder.group({
        name     : ['', Validators.required],
        password  : ['', Validators.required]
      });
    }

    ngOnInit(): void{}

    async signIn(): Promise<void>
    {
        if (this.signInForm.invalid)
        {
            return;
        }

        this.signInForm.disable();

        try {

            await this._authService.loginAsync({
                name: this.signInForm.value.name,
                password: this.signInForm.value.password
            });

            const redirectURL = this._activatedRoute.snapshot.queryParamMap.get('redirectURL') || '/signed-in-redirect';

            // Navigate to the redirect url
            this._router.navigateByUrl(redirectURL);
        }
        catch(error){
            this.signInForm.enable();
        }
    }
}
