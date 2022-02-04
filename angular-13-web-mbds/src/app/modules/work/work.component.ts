import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthentificationService } from 'app/core/authentification/authentification.service';
import { ComponentStateService } from 'app/core/componentstate/componentstate.service';
import { ComponentState } from 'app/core/componentstate/componentstate.types';
import { IdentityService } from 'app/core/identity/identity.service';
import BaseComponent, { NavigationAction } from '../base/basecomponent';

@Component({
  selector: 'app-work',
  templateUrl: './work.component.html',
  styleUrls: ['./work.component.scss'],
  providers: [ComponentStateService]
})
export class WorkComponent extends BaseComponent implements OnInit {

  private _title = 'Work'

  protected getComponentName(): string{
    return this._title;
  }

  constructor(
    _identityService: IdentityService,
    _stateService: ComponentStateService,
    _router: Router,
    _activatedRoute: ActivatedRoute,
    _ref: ChangeDetectorRef
    ) {
      super(_identityService, _stateService, _router, _activatedRoute, _ref)
    }

  ngOnInit(): void {
    super.ngOnInit();

  /*  this._activatedRoute.queryParamMap
      .subscribe((params) => {
        this.redirect = params.get('redirect');
      });*/
  }

  protected getNavigationUrl(state: ComponentState, isback: boolean): NavigationAction {
    let url: string | null = null;

    if (isback && this.redirect) {
      return { url: this.redirect, relativeToComponent: false };
    }

    switch (state) {
      case ComponentState.List: url = 'list'; break;
      case ComponentState.Create: url = 'create'; break;
      //case ComponentState.Details: url = `details/${this.courseSelected?.id}`; break;
      //case ComponentState.Edit: url = `edit/${this.courseSelected?.id}`; break;
    }
    return { url: url, relativeToComponent: true };
  }

  protected refreshStateActions(): void {
    if (!this.stateActions || !this.state) return;

    this.stateActions.back.view = this.state != ComponentState.None && this.state != ComponentState.List;
    this.stateActions.back.disabled = !this.stateActions.back.view;

    this.stateActions.create.view = this.state === ComponentState.List;
    this.stateActions.create.disabled = false;

    this.stateActions.details.view = this.state === ComponentState.List;
    // this.stateActions.details.disabled = this.courseSelected === null;

    this.stateActions.update.view = (this.state === ComponentState.List || this.state === ComponentState.Details);
    // this.stateActions.update.disabled = this.courseSelected === null;

    this.stateActions.delete.view = false;
    // this.stateActions.delete.disabled = this.courseSelected === null;
  }
}
