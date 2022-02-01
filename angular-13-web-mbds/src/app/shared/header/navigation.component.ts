import { Component, EventEmitter, Output, OnInit, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';
import { UserIdentity } from 'app/core/authentification/auth.types';
import { IdentityService } from 'app/core/identity/identity.service';
import { UsersService } from 'app/core/users/users.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html'
})
export class NavigationComponent implements OnInit, OnDestroy {

  @Output() toggleSidebar = new EventEmitter<void>();

  public showSearch = false;
  public user?: UserIdentity | null;
  private subscription$?: Subscription | null;


  constructor(
    private _router: Router,
    private _identityService: IdentityService,
    private _userService: UsersService,
  ) {
  }

  ngOnDestroy(): void {
    if(this.subscription$){
      this.subscription$.unsubscribe();
    }
  }

  ngOnInit(): void {
    this.subscription$ = this._identityService.identity.subscribe(user => this.user = user);
  }

  public signOut(): void
  {
      this._router.navigate(['/sign-out']);
  }

  public sourceImageUser(userId: number, pictureId: number | null){
    return this._userService.sourceImageUser(userId, pictureId);
  }

}
