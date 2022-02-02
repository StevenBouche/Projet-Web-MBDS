import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { PaginationForm, PaginationResult } from 'app/core/api/api.types';
import { ComponentStateService } from 'app/core/componentstate/componentstate.service';
import { ComponentState } from 'app/core/componentstate/componentstate.types';
import { CoursesService } from 'app/core/courses/courses.service';
import { Course } from 'app/core/courses/courses.type';
import { ImageHelper } from 'app/core/helpers/image.helper';
import { debounceTime, distinctUntilChanged, Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-course-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class CourseListComponent implements OnInit, OnDestroy {

  public hideRequiredControl = new FormControl(false);

  public courseSelected: Course | null = null
  public paginationResult: PaginationResult<Course> | null = null;

  get page() { return this._coursesService.page; }
  set page(value) { this._coursesService.page = value; }

  get pageSize() { return this._coursesService.pagesize; }
  set pageSize(value) { this._coursesService.pagesize = value; }

  get total() { return this.paginationResult != null ? this.paginationResult.total : 0; }

  searchInputCourse: FormControl = new FormControl();
  searchInputUser: FormControl = new FormControl();

  private _unsubscribeAll: Subject<any> = new Subject<any>();

  constructor(
    private _coursesService: CoursesService,
    private _stateService: ComponentStateService,
    private imageHelper: ImageHelper
    ) {

    }

  ngOnDestroy(): void {
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }

  ngOnInit(): void {

    this._coursesService.pagination
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe((paginationResult: PaginationResult<Course> | null) => {
        this.paginationResult = paginationResult;
      })

      this._coursesService.courseSelected
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe((course: Course | null) => {
        this.courseSelected = course;
      })

      this.searchInputCourse.valueChanges
      .pipe(takeUntil(this._unsubscribeAll),debounceTime(500),distinctUntilChanged())
      .subscribe(async (value: string | null) => {
        if(value != null) {
          this._coursesService.setCourseNamePagination(value)
        }
      });

      this.searchInputUser.valueChanges
      .pipe(takeUntil(this._unsubscribeAll),debounceTime(500),distinctUntilChanged())
      .subscribe(async (value: string | null) => {
        if(value != null) {
          this._coursesService.setUserNamePagination(value)
        }
      });

      this._stateService.setState(ComponentState.List);
      this._coursesService.getAllAsync();
  }

  public onClickItem(course: Course): void{
    this._coursesService.setCourseSelected(course);
  }
}
