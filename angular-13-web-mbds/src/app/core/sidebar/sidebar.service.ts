import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { ROUTES } from './sidebar.data';
import { RouteInfo } from './sidebar.types';

@Injectable({
  providedIn: 'root'
})
export class SidebarService{

  private _routeItems = new BehaviorSubject<RouteInfo[]>(ROUTES);
  readonly routeItems = this._routeItems.asObservable();

    constructor(){

    }

}
