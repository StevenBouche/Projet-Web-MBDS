import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { ComponentState } from 'app/core/shared/shared.types';
import { CoursesStateActions } from '../courses/courses.component';
import { Assignment } from 'app/core/assignments/assignments.type';

@Component({
  selector: 'app-assignments',
  templateUrl: './assignments.component.html',
  styleUrls: ['./assignments.component.scss']
})
export class AssignmentsComponent implements OnInit {

  private assignmentSelected: Assignment | null = null

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
    this._router.navigate(['create'], {relativeTo: this._activatedRoute});
  }

  public onChangeState(state: ComponentState){
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
    this.stateActions.details.disabled = this.assignmentSelected === null;

    this.stateActions.update.view = this.state === ComponentState.List || this.state === ComponentState.Details;
    this.stateActions.update.disabled = this.assignmentSelected === null;

    this.stateActions.delete.view = false;
    this.stateActions.delete.disabled = this.assignmentSelected === null;
  }
}
