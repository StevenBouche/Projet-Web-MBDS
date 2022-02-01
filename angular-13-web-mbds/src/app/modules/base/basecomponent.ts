import { ActivatedRoute, Router } from "@angular/router";
import { ComponentStateService } from "app/core/componentstate/componentstate.service";
import { ComponentState, ComponentStateActions } from "app/core/componentstate/componentstate.types";
import { Subject, takeUntil } from "rxjs";

export interface NavigationAction{
  url: string | null;
  relativeToComponent: boolean;
}

export default abstract class BaseComponent {

  protected _unsubscribeAll: Subject<any> = new Subject<any>();

  private _lastState: ComponentState | null = null
  private _state: ComponentState | null = null
  public stateActions: ComponentStateActions | null = null

  get state() { return this._state; }
  set state(value) {
    this._stateService.setState(value)
  }

  constructor(
    protected _stateService: ComponentStateService,
    protected _router: Router,
    protected _activatedRoute: ActivatedRoute
  ) {

  }

  protected abstract getNavigationUrl(state: ComponentState, isback: boolean): NavigationAction;
  protected abstract refreshStateActions(): void;

  protected OnInit() {
    this._stateService.state
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe(state => this.onStateChange(state))

    this._stateService.stateAction
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe(state => this.handleStateAction(state))
  }

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
  }
}
