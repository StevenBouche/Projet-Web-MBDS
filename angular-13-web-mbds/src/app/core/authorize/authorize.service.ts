/* eslint-disable @typescript-eslint/member-ordering */

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { authorizeActionUser, authorizeMap } from './authorize.data';
import { AuthorizationAction } from './authorize.type';
import { BehaviorSubject } from 'rxjs';
import { ApiService } from 'app/core/api/api.service';
import { AuthentificationService } from '../authentification/authentification.service';
import { UserIdentity } from '../authentification/auth.types';
import { IdentityService } from '../identity/identity.service';
import { Assignment } from '../assignments/assignments.type';

@Injectable({
  providedIn: 'root'
})
export class AuthorizeService {


    private store: {

    } = {

    };

    private identity: UserIdentity | null = null;

    constructor(toastr: ToastrService, private _identityService: IdentityService){
        this._identityService.identity.subscribe((identity: UserIdentity | null) => {
            this.identity = identity;
        });
    }

    public canEditAssignmentState(assignment: Assignment | null): boolean {
      return this.identity != null && assignment != null && this.identity.id != assignment.course.user.id;
    }
}







