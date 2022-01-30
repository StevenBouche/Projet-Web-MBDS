import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { UserIdentity } from 'app/core/authentification/auth.types';
import { AuthentificationService } from 'app/core/authentification/authentification.service';
import { CoursesService } from 'app/core/courses/courses.service';
import { Course } from 'app/core/courses/courses.type';
import { ComponentState, StateAction } from 'app/core/shared/shared.types';
import { Subject, takeUntil } from 'rxjs';

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
  styleUrls: ['./courses.component.scss']
})
export class CoursesComponent implements OnInit, OnDestroy {

  private courseSelected: Course | null = null
  private user: UserIdentity | null = null
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

  private _unsubscribeAll: Subject<any> = new Subject<any>();

  constructor(
    private _coursesService: CoursesService,
    private _authentificationService: AuthentificationService,
  ) { }

  ngOnDestroy(): void {
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }

  ngOnInit(): void {

    this._authentificationService.identity
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe((user: UserIdentity | null) => {
        this.user = user;
        this.refreshStateActions();
      })

    this._coursesService.courseSelected
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe((course: Course | null) => {
        this.courseSelected = course;
        this.refreshStateActions();
      })
  }

  public onChangeState(state: ComponentState){
    this.state = state;
    this.refreshStateActions();
  }

  private refreshStateActions() {

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
