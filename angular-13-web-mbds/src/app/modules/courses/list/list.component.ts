import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { PaginationResult } from 'app/core/api/api.types';
import { CoursesService } from 'app/core/courses/courses.service';
import { Course } from 'app/core/courses/courses.type';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class CourseListComponent implements OnInit, OnDestroy {

  private _page: number = 1;
  private _pageSize: number = 20;
  hideRequiredControl = new FormControl(false);

  paginationResult?: PaginationResult<Course>

  get page() { return this._page; }
  set page(value) {
    console.log(value)
    this._page = value;
  }

  get pageSize() { return this._pageSize; }
  set pageSize(value) {
    console.log(value)
    this._page = value;
  }

  private _unsubscribeAll: Subject<any> = new Subject<any>();

  constructor(private _coursesService: CoursesService) { }

  ngOnDestroy(): void {
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }

  ngOnInit(): void {
    this._coursesService.pagination
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe((paginationResult: PaginationResult<Course>) => {
        this.paginationResult = paginationResult;
      })
  }
}
