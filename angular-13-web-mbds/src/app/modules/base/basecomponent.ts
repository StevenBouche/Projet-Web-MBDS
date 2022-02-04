import { AfterContentChecked, ChangeDetectorRef, Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { UserIdentity } from "app/core/authentification/auth.types";
import { AuthentificationService } from "app/core/authentification/authentification.service";
import { ComponentStateService } from "app/core/componentstate/componentstate.service";
import { ComponentState, ComponentStateActions } from "app/core/componentstate/componentstate.types";
import { IdentityService } from "app/core/identity/identity.service";
import { Subject, takeUntil } from "rxjs";

export interface NavigationAction{
  url: string | null;
  relativeToComponent: boolean;
}

@Component({ template: '' })
export default abstract class BaseComponent implements OnInit, AfterContentChecked {

  protected _unsubscribeAll: Subject<any> = new Subject<any>();

  private _lastState: ComponentState | null = null
  private _state: ComponentState | null = null
  public stateActions: ComponentStateActions | null = null
  public title : string | null = null;
  protected redirect: string | null = null;
  protected user: UserIdentity | null = null

  get state() { return this._state; }
  set state(value) {
    this._stateService.setState(value)
  }

  constructor(
    protected _identityService: IdentityService,
    protected _stateService: ComponentStateService,
    protected _router: Router,
    protected _activatedRoute: ActivatedRoute,
    protected _changeDetector: ChangeDetectorRef
  ) {

  }

  ngAfterContentChecked(): void {
    this._changeDetector.detectChanges();
  }

  ngOnInit(): void {

    this._activatedRoute.queryParamMap
      .subscribe((params) => {
        this.redirect = params.get('redirect');
      });
      
    this._identityService.identity
    .pipe(takeUntil(this._unsubscribeAll))
    .subscribe(user => this.handleUserIdentity(user))

    this._stateService.state
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe(state => this.onStateChange(state))

    this._stateService.stateAction
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe(state => this.handleStateAction(state))
  }

  protected abstract getNavigationUrl(state: ComponentState, isback: boolean): NavigationAction;
  protected abstract refreshStateActions(): void;
  protected abstract getComponentName(): string;



  public createNavigation(): void {
    this.navigate(ComponentState.Create);
  }

  public editNavigation(): void {
    this.navigate(ComponentState.Edit);
  }

  public backNavigation(): void {
    if (this._lastState)
      this.navigate(this._lastState, true);
  }

  public detailsNavigation(): void {
    this.navigate(ComponentState.Details);
  }

  private navigate(state: ComponentState, isback: boolean = false) {
    let action: NavigationAction = this.getNavigationUrl(state, isback);
    if (action.url){
      const obj = action.relativeToComponent ? { relativeTo: this._activatedRoute } : {};
      this._router.navigate([action.url], obj);
    }
  }

  private handleStateAction(handleStateAction: ComponentStateActions) {
    this.stateActions = handleStateAction;
  }

  private handleUserIdentity(user: UserIdentity | null) {
    this.user = user;
    this.refreshStateActions();
  }

  private onStateChange(state: ComponentState | null) {

    switch (state) {
      case ComponentState.Details:
      case ComponentState.Create:
        this._lastState = ComponentState.List; break;
      case ComponentState.Edit:
        this._lastState = this.state === ComponentState.Details ? ComponentState.Details : ComponentState.List; break;
      case ComponentState.List:
      default:
        this._lastState = null; break;
    }

    this._state = state;

    this.refreshStateActions();
    this.title = this.handleTitle();

  }

  private handleTitle() : string {
    const _title = this.getComponentName();
    switch (this._state) {
      case ComponentState.List: return `List ${_title}`
      case ComponentState.Create: return `Create ${_title}`
      case ComponentState.Details: return `Details ${_title}`
      case ComponentState.Edit: return `Edit ${_title}`
    }
    return _title;
  }
}
