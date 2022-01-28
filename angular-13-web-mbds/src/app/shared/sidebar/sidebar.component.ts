import { Component, OnInit, OnDestroy } from '@angular/core';
import { SidebarService } from 'app/core/sidebar/sidebar.service';
import { RouteInfo } from 'app/core/sidebar/sidebar.types';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html'
})
export class SidebarComponent implements OnInit, OnDestroy {

  showMenu = '';
  showSubMenu = '';

  public sidebarnavItems: RouteInfo[] = [];
  private subscription$?: Subscription | null;

  // this is for the open close
  addExpandClass(element: string) {
    if (element === this.showMenu) {
      this.showMenu = '0';
    } else {
      this.showMenu = element;
    }
  }

  constructor(
    private _sidebarService: SidebarService
  ) {}

  ngOnDestroy(): void {
    if(this.subscription$){
      this.subscription$.unsubscribe();
    }
  }

  // End open close
  ngOnInit() {
    this.subscription$ = this._sidebarService.routeItems.subscribe((routes: RouteInfo[]) => this.sidebarnavItems = routes);
  }
}
