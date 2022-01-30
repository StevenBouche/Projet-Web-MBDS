/* eslint-disable @typescript-eslint/member-ordering */

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { ApiService } from 'app/core/api/api.service';
import { Course, CourseFormCreate, CourseFormUpdate, CourseSearchForm, CourseSearchFormResults } from './courses.type';
import { PaginationForm, PaginationResult } from '../api/api.types';
import { Assignment } from '../assignments/assignments.type';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CoursesService extends ApiService {

  private store: {
    courseSelected: Course | null,
    pagination: PaginationForm
  } = {
      courseSelected: null,
      pagination: { page: 1, pagesize: 20 }
    };

  private _courseSelected = new BehaviorSubject<Course | null>(this.store.courseSelected);
  private _pagination$ = new BehaviorSubject<PaginationResult<Course> | null>(null);

  public courseSelected = this._courseSelected.asObservable();
  public pagination = this._pagination$.asObservable();

  get page() { return this.store.pagination.page; }
  set page(value) {
    this.store.pagination.page = value;
    this.getAllAsync();
  }

  get pagesize() { return this.store.pagination.pagesize; }
  set pagesize(value) {
    this.store.pagination.pagesize = value;
    this.getAllAsync();
  }

  getName(): string {
    return "Courses";
  }

  constructor(http: HttpClient, toastr: ToastrService) {
    super(http, toastr);
  }

  public setCourseSelected(course: Course) {
    this.store.courseSelected = this.store.courseSelected != null && this.store.courseSelected.id === course.id ? null : course;
    this._courseSelected.next(this.store.courseSelected);
  }

  public async createAsync(form: CourseFormCreate): Promise<Course> {
    return this.executePostAsync<CourseFormCreate, Course>(`${this.baseUrl}/courses`, form);
  }

  public async updateAsync(form: CourseFormUpdate): Promise<Course> {
    return this.executePutAsync<CourseFormCreate, Course>(`${this.baseUrl}/courses`, form);
  }

  public async getByIdAsync(id: number): Promise<Course> {
    return this.executeGetAsync<Course>(`${this.baseUrl}/courses/${id}`);
  }

  public async getAllAsync() {
    let result = await this.executePostAsync<PaginationForm, PaginationResult<Course>>(`${this.baseUrl}/courses/mine`, this.store.pagination);
    this._pagination$.next(result);
  }

  public async getAllSearchAsync(term: string): Promise<CourseSearchFormResults> {
    return this.executePostAsync<CourseSearchForm, CourseSearchFormResults>(
      `${this.baseUrl}/courses/search`,
      {term}
    );
  }

  public async getAllIsMineAsync(form: PaginationForm) {
    return this.executePostAsync<PaginationForm, PaginationResult<Course>>(
      `${this.baseUrl}/courses/all`,
      form
    );
  }

  public async getAssignmentsOfCourseAsync(id: number, form: PaginationForm) {
    return this.executePostAsync<PaginationForm, PaginationResult<Assignment>>(
      `${this.baseUrl}/courses/${id}/assignments`,
      form
    );
  }
}







