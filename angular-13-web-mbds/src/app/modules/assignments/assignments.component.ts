import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { CoursesStateActions } from '../courses/courses.component';
import { Assignment } from 'app/core/assignments/assignments.type';
import { ComponentState } from 'app/core/componentstate/componentstate.types';
import BaseComponent, { NavigationAction } from '../base/basecomponent';
import { ComponentStateService } from 'app/core/componentstate/componentstate.service';
import { filter, takeUntil } from 'rxjs';

@Component({
  selector: 'app-assignments',
  templateUrl: './assignments.component.html',
  styleUrls: ['./assignments.component.scss'],
  providers: [ComponentStateService]
})
export class AssignmentsComponent extends BaseComponent implements OnInit {

  private redirect : string | null = null;
  private assignmentSelected: Assignment | null = null

  constructor(
    _stateService: ComponentStateService,
    _router: Router,
    _activatedRoute: ActivatedRoute
  ) {
    super(_stateService, _router, _activatedRoute)
  }

  ngOnInit(): void {
    this.OnInit();

    this._activatedRoute.queryParamMap
      .subscribe((params) => {
        this.redirect = params.get('redirect');
      });
  }

  protected getNavigationUrl(state: ComponentState, isback: boolean): NavigationAction {
    let url: string | null = null;

    if(isback && this.redirect){
      return { url: this.redirect, relativeToComponent: false };
    }

    switch (state) {
      case ComponentState.List: url = 'list'; break;
      case ComponentState.Create: url = 'create'; break;
      case ComponentState.Details: url = `details/${this.assignmentSelected?.id}`; break;
      case ComponentState.Edit: url = `edit/${this.assignmentSelected?.id}`; break;
    }

    return { url: url, relativeToComponent: true };
  }

  protected refreshStateActions() {

    if (!this.stateActions) return;

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
