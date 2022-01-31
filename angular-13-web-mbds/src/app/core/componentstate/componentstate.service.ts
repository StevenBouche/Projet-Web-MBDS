/* eslint-disable @typescript-eslint/member-ordering */

import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { ComponentState, ComponentStateActions } from './componentstate.types';

@Injectable({
  providedIn: 'root'
})
export class ComponentStateService {

  private store: {
    state: ComponentState | null,
    stateAction: ComponentStateActions
  } = {
      state: null,
      stateAction: {
        back: { view: false, disabled: true },
        create: { view: false, disabled: true },
        details: { view: false, disabled: true },
        update: { view: false, disabled: true },
        delete: { view: false, disabled: true }
      }
    };

  private readonly _state = new BehaviorSubject<ComponentState | null>(this.store.state);
  private readonly _stateAction = new BehaviorSubject<ComponentStateActions>(this.store.stateAction)

  public readonly state = this._state.asObservable();
  public readonly stateAction = this._stateAction.asObservable();

  constructor() {

  }

  public setState(state: ComponentState | null){
    this.store.state = state;
    this._state.next(state);
  }

  public setStateAction(state: ComponentStateActions){
    this.store.stateAction = state;
    this._stateAction.next(state);
  }
}







