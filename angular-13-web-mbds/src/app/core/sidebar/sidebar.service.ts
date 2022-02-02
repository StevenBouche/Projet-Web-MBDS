import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { AuthentificationService } from '../authentification/authentification.service';
import { IdentityService } from '../identity/identity.service';
import { ROUTES_PROFESSOR, ROUTES_STUDENT } from './sidebar.data';
import { RouteInfo } from './sidebar.types';

@Injectable({
  providedIn: 'root'
})
export class SidebarService{

  private _routeItems = new BehaviorSubject<RouteInfo[]>([]);
  readonly routeItems = this._routeItems.asObservable();

    constructor(private _identity: IdentityService){
      this._identity.identity.subscribe(identity => {
        if(identity?.role === 'PROFESSOR')
          this._routeItems.next(ROUTES_PROFESSOR);
        else if(identity?.role === 'STUDENT')
          this._routeItems.next(ROUTES_STUDENT);
      })
    }
}
