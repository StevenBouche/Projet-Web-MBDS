import { Component, OnDestroy, OnInit } from '@angular/core';
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

  constructor(private _coursesService: CoursesService, private _stateService: ComponentStateService) { }

  ngOnDestroy(): void {
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }

  ngOnInit(): void {
    this._coursesService.courseSelected
    .pipe(takeUntil(this._unsubscribeAll))
    .subscribe((course: Course | null) => {
      this.courseSelected = course;
    })

    this._coursesService.assignmentsCourse
    .pipe(takeUntil(this._unsubscribeAll))
    .subscribe((a: Array<Assignment>) => {
      this.assignmentsCourse = a;
    })

    this._stateService.setState(ComponentState.Details);
  }

  public sourceImage(idpicture: number){
    return this._coursesService.sourceImage(idpicture);
  }

  public sourceImageUser(idpicture: number){
    return this._coursesService.sourceImageUser(idpicture);
  }

}
