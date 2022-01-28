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

@Injectable({
  providedIn: 'root'
})
export class AuthorizeService extends ApiService {

    private store: {
        authorizationAdminDashboard: AuthorizationAction;
    } = {
        authorizationAdminDashboard: { read: false, create: false, update: false, delete: false}
    };

    private identity: UserIdentity | null = null;

    private authorizeMenu: Map<string, boolean> = new Map();

    private _authorizationAdminDashboard = new BehaviorSubject<AuthorizationAction>(this.store.authorizationAdminDashboard);
    readonly authorizationAdminDashboard = this._authorizationAdminDashboard.asObservable();

    constructor(http: HttpClient, toastr: ToastrService, private authService: AuthentificationService){

        super(http, toastr);

        this.authService.identity.subscribe((identity: UserIdentity | null) => {
            this.identity = identity;
            this.setAuthorizeMenu();
            //this.setAuthorizeAction();
        });
    }

    private setAuthorizeMenu(): void {

        this.authorizeMenu.clear();

        if(this.identity === undefined ||  this.identity === null || this.identity.role === undefined){
            return;
        }

        let isAuthorized = false;

        for (const [key, value] of authorizeMap) {
          isAuthorized = value.includes(this.identity.role);
          if(isAuthorized){
              break;
          }
          this.authorizeMenu.set(key, isAuthorized);
          isAuthorized = false;
        }
    }

    private readonly serviceName = 'Authorization';

    getName(): string {
        return this.serviceName;
    }

  /*  menuItemAuthorizationIsHidden(item: FuseNavigationItem): boolean{
        return this.authorizeMenu.has(item.id) ? !this.authorizeMenu.get(item.id) : true;
    }

    private sortRoleById(roles: string[]): string[]{
        return roles.sort((n1,n2) => n1.localeCompare(n2));
    }

    private getBestRole(roles: string[]): string | null {

        if(roles === undefined){
            return null;
        }

        if(roles.length === 0){
            return null;
        }

        return [...this.sortRoleById(roles)][0];
    }

    private setAuthorizeAction(): void {

        const defaults: AuthorizationAction = { read: false, create: false, update: false, delete: false};

        if(this.identity === undefined || this.identity === null || this.identity.role === undefined){
            this._authorizationAdminDashboard.next(defaults);
            return;
        }

        const role: string = this.getBestRole(this.identity.roles);

        if(authorizeActionUser.has(role.authority)){
            this._authorizationAdminDashboard.next(authorizeActionUser.get(role.authority));
        }
        else {
            this._authorizationAdminDashboard.next(defaults);
        }
    }*/
}







