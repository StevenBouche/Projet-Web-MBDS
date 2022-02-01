import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { CoursesStateActions } from '../courses/courses.component';
import { ComponentState } from 'app/core/componentstate/componentstate.types';
import { User } from 'app/core/users/users.types';


@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {

  private userSelected: User | null = null

  private state: ComponentState = ComponentState.None

  get listMode() { return this.state === ComponentState.List }
  get detailsMode() { return this.state === ComponentState.Details }
  get editMode() { return this.state === ComponentState.Edit }
  get createMode() { return this.state === ComponentState.Create }

  public stateActions: CoursesStateActions = {
    back: { view: false, disabled: true },
    create: { view: false, disabled: true },
    details: { view: false, disabled: true },
    update: { view: false, disabled: true },
    delete: { view: false, disabled: true }
  }

  constructor(
    private _router: Router,
    private _activatedRoute: ActivatedRoute
  ) { }

  ngOnInit(): void {
  }

  public create(): void {
    console.log(this._activatedRoute)
    this._router.navigate(['create'], { relativeTo: this._activatedRoute });
  }

  public onChangeState(state: ComponentState) {
    console.log('onChangeState', state)
    this.state = state;
    this.refreshStateActions();
  }

  private refreshStateActions() {

    this.stateActions.back.view = this.state != ComponentState.None && this.state != ComponentState.List;
    this.stateActions.back.disabled = !this.stateActions.back.view;

    this.stateActions.create.view = this.state === ComponentState.List;
    this.stateActions.create.disabled = false;

    this.stateActions.details.view = this.state === ComponentState.List;
    this.stateActions.details.disabled = this.userSelected === null;

    this.stateActions.update.view = this.state === ComponentState.List || this.state === ComponentState.Details;
    this.stateActions.update.disabled = this.userSelected === null;

    this.stateActions.delete.view = false;
    this.stateActions.delete.disabled = this.userSelected === null;
  }
}
