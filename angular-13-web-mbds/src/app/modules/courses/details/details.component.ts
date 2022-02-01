import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Assignment } from 'app/core/assignments/assignments.type';
import { ComponentStateService } from 'app/core/componentstate/componentstate.service';
import { ComponentState } from 'app/core/componentstate/componentstate.types';

import { CoursesService } from 'app/core/courses/courses.service';
import { Course } from 'app/core/courses/courses.type';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-course-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.scss']
})
export class CourseDetailsComponent implements OnInit, OnDestroy {

  public courseSelected: Course | null = null;
  public assignmentsCourse: Array<Assignment> = []
  private _unsubscribeAll: Subject<any> = new Subject<any>();

  constructor(
    private _coursesService: CoursesService,
    private _stateService: ComponentStateService,
    private _route: ActivatedRoute,
    private _router: Router
    ) { }

  ngOnDestroy(): void {
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }

  ngOnInit(): void {

    this.courseSelected = this._route.snapshot.data.initialData.course;
    this.assignmentsCourse = this._route.snapshot.data.initialData.assignments;

    this._stateService.setState(ComponentState.Details);
  }

  public sourceImage(idpicture: number){
    return this._coursesService.sourceImage(idpicture);
  }

  public sourceImageUser(idpicture: number){
    return this._coursesService.sourceImageUser(idpicture);
  }

  public detailsAssignments(id: number){
    this._router.navigate(
      [`/assignments/details/${id}`],
      { queryParams: { 'redirect': `/courses/details/${this.courseSelected?.id}` } }
    );
  }
}
