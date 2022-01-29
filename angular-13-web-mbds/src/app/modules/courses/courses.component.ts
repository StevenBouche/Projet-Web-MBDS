import { Component, OnDestroy, OnInit } from '@angular/core';
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
  refresh: StateAction;
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

  public stateActions: CoursesStateActions = {
    back: { view: false, disabled: true },
    create: { view: false, disabled: true },
    details: { view: false, disabled: true },
    update: { view: false, disabled: true },
    delete: { view: false, disabled: true },
    refresh: { view: false, disabled: true }
  }

  private _unsubscribeAll: Subject<any> = new Subject<any>();

  constructor(
    private _router: Router,
    private _activatedRoute: ActivatedRoute,
    private _coursesService: CoursesService,
    private _authentificationService: AuthentificationService
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

    this._coursesService.stateComponent
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe((state: ComponentState) => {
        this.state = state;
        this.refreshStateActions();
      })

    this._coursesService.courseSelected
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe((course: Course | null) => {
        this.courseSelected = course;
        this.refreshStateActions();
      })
  }

  public create(): void {
    console.log(this._activatedRoute)
    this._coursesService.setStateComponent(ComponentState.Create);
    this._router.navigate(['create'], { relativeTo: this._activatedRoute });
  }

  public edit(): void {
    console.log(this._activatedRoute)
    this._router.navigate([`details/${this.courseSelected?.id}`], { relativeTo: this._activatedRoute });
  }

  public details(): void {
    console.log(this._activatedRoute)
    this._router.navigate([`edit/${this.courseSelected?.id}`], { relativeTo: this._activatedRoute });
  }

  private refreshStateActions() {
    this.stateActions.back.view = this.state != ComponentState.None && this.state != ComponentState.List;
    this.stateActions.back.disabled = !this.stateActions.back.view;

    this.stateActions.create.view = this.state === ComponentState.List;
    this.stateActions.create.disabled = false;

    this.stateActions.details.view = this.state === ComponentState.List;
    this.stateActions.details.disabled = this.courseSelected === null;

    this.stateActions.update.view = this.state === ComponentState.List;
    this.stateActions.update.disabled = this.courseSelected === null;

    this.stateActions.delete.view = false;
    this.stateActions.delete.disabled = this.courseSelected === null;

    this.stateActions.refresh.view = this.state === ComponentState.List;
    this.stateActions.refresh.disabled = false;
  }
}
