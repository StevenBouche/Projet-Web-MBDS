import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { UserIdentity } from 'app/core/authentification/auth.types';
import { AuthentificationService } from 'app/core/authentification/authentification.service';
import { ComponentStateService } from 'app/core/componentstate/componentstate.service';
import { ComponentState, ComponentStateActions, StateAction } from 'app/core/componentstate/componentstate.types';
import { CoursesService } from 'app/core/courses/courses.service';
import { Course } from 'app/core/courses/courses.type';
import { retryWhen, Subject, takeUntil } from 'rxjs';
import BaseComponent, { NavigationAction } from '../base/basecomponent';

export interface CoursesStateActions {
  back: StateAction;
  create: StateAction;
  details: StateAction;
  update: StateAction;
  delete: StateAction;
}

@Component({
  selector: 'app-courses',
  templateUrl: './courses.component.html',
  styleUrls: ['./courses.component.scss'],
  providers: [ComponentStateService]
})
export class CoursesComponent extends BaseComponent implements OnInit, OnDestroy {

  private courseSelected: Course | null = null


  private _title = 'Course'

  protected getComponentName(): string{
    return this._title;
  }

  constructor(
    private _coursesService: CoursesService,
    _authentificationService: AuthentificationService,
    _stateService: ComponentStateService,
    _router: Router,
    _activatedRoute: ActivatedRoute,
    _ref: ChangeDetectorRef
  ) {
    super(_authentificationService, _stateService, _router, _activatedRoute, _ref)
  }

  ngOnDestroy(): void {
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }

  ngOnInit(): void {

    super.ngOnInit();

    this._coursesService.courseSelected
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe(course => this.handleCourseSelected(course))

  }

  private handleCourseSelected(course: Course | null) {
    console.log(course)
    this.courseSelected = course;
    this.refreshStateActions();
  }

  protected getNavigationUrl(state: ComponentState, isback: boolean): NavigationAction {
    let url: string | null = null;
    switch (state) {
      case ComponentState.List: url = 'list'; break;
      case ComponentState.Create: url = 'create'; break;
      case ComponentState.Details: url = `details/${this.courseSelected?.id}`; break;
      case ComponentState.Edit: url = `edit/${this.courseSelected?.id}`; break;
    }
    return { url: url, relativeToComponent: true };
  }

  protected refreshStateActions() {

    if (!this.stateActions || !this.state) return;

    const authorizeUserProfessor = this.user != null && this.user.role === 'PROFESSOR';

    this.stateActions.back.view = this.state != ComponentState.None && this.state != ComponentState.List;
    this.stateActions.back.disabled = !this.stateActions.back.view;

    this.stateActions.create.view = this.state === ComponentState.List && authorizeUserProfessor;
    this.stateActions.create.disabled = false;

    this.stateActions.details.view = this.state === ComponentState.List;
    this.stateActions.details.disabled = this.courseSelected === null;

    this.stateActions.update.view = (this.state === ComponentState.List || this.state === ComponentState.Details) && authorizeUserProfessor;
    this.stateActions.update.disabled = this.courseSelected === null;

    this.stateActions.delete.view = false;
    this.stateActions.delete.disabled = this.courseSelected === null;
  }
}
