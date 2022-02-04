import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { CoursesStateActions } from '../courses/courses.component';
import { Assignment } from 'app/core/assignments/assignments.type';
import { ComponentState } from 'app/core/componentstate/componentstate.types';
import BaseComponent, { NavigationAction } from '../base/basecomponent';
import { ComponentStateService } from 'app/core/componentstate/componentstate.service';
import { filter, takeUntil } from 'rxjs';
import { AssignmentsService } from 'app/core/assignments/assignments.service';
import { AuthentificationService } from 'app/core/authentification/authentification.service';
import { IdentityService } from 'app/core/identity/identity.service';
import { AuthorizeService } from 'app/core/authorize/authorize.service';

@Component({
  selector: 'app-assignments',
  templateUrl: './assignments.component.html',
  styleUrls: ['./assignments.component.scss'],
  providers: [ComponentStateService, AssignmentsService]
})
export class AssignmentsComponent extends BaseComponent implements OnInit {


  private assignmentSelected: Assignment | null = null

  private _title = 'Assignment'

  protected getComponentName(): string {
    return this._title;
  }

  constructor(
    private _assignmentsService: AssignmentsService,
    private _authorizeService: AuthorizeService,
    _identityService: IdentityService,
    _stateService: ComponentStateService,
    _router: Router,
    _activatedRoute: ActivatedRoute,
    _ref: ChangeDetectorRef
  ) {
    super(_identityService,_stateService, _router, _activatedRoute, _ref)
  }

  ngOnDestroy(): void {
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }

  ngOnInit(): void {

    super.ngOnInit();

    this._assignmentsService.assignmentSelected
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe(ass => this.handleSelected(ass));

  }

  private handleSelected(ass: Assignment | null) {
    this.assignmentSelected = ass;
    this.refreshStateActions();
  }

  protected getNavigationUrl(state: ComponentState, isback: boolean): NavigationAction {
    let url: string | null = null;

    if (isback && this.redirect) {
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

    const canCreateAssignment = this._authorizeService.canCreateAssignment();
    const canUpdateAssignment = this._authorizeService.canUpdateAssignment();
    const isOwner = this._authorizeService.isOwnerOfAssignment(this.assignmentSelected);

    const assignmentIsClose = this.assignmentSelected != null && this.assignmentSelected.state === 1;

    console.log(isOwner, assignmentIsClose)

    this.stateActions.back.view = this.state != ComponentState.None && this.state != ComponentState.List;
    this.stateActions.back.disabled = !this.stateActions.back.view;

    this.stateActions.create.view = this.state === ComponentState.List && canCreateAssignment;
    this.stateActions.create.disabled = false;

    this.stateActions.details.view = this.state === ComponentState.List;
    this.stateActions.details.disabled = this.assignmentSelected === null ;

    this.stateActions.update.view = (this.state === ComponentState.List || this.state === ComponentState.Details) && canUpdateAssignment;
    this.stateActions.update.disabled = !isOwner || assignmentIsClose;

    this.stateActions.delete.view = false;
    this.stateActions.delete.disabled = this.assignmentSelected === null;
  }
}
