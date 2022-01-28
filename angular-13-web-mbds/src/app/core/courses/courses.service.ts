/* eslint-disable @typescript-eslint/member-ordering */

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { ApiService } from 'app/core/api/api.service';
import { Course, CourseFormCreate, CourseFormUpdate } from './courses.type';
import { PaginationForm, PaginationResult } from '../api/api.types';
import { Assignment } from '../assignments/assignments.type';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CoursesService extends ApiService {


  private store: {
    paginationForm: PaginationForm
  } = {
    paginationForm: { pagesize: 20, page: 1,}
  };

  private _pagination$ = new BehaviorSubject<PaginationResult<Course>>(
    {
      pageSize: this.store.paginationForm.pagesize,
      page: this.store.paginationForm.page,
      totalPage: 0,
      total: 0,
      results: []
    }
  );

    getName(): string {
      return "Courses";
    }

    constructor(http: HttpClient, toastr: ToastrService){
        super(http, toastr);
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

    public async getAllAsync(form: PaginationForm){
      return this.executePostAsync<PaginationForm, PaginationResult<Course>>(
        `${this.baseUrl}/courses/mine`,
        form
      );
    }

    public async getAllIsMineAsync(form: PaginationForm){
      return this.executePostAsync<PaginationForm, PaginationResult<Course>>(
        `${this.baseUrl}/courses/all`,
        form
      );
    }

    public async getAssignmentsOfCourseAsync(id: number, form: PaginationForm){
      return this.executePostAsync<PaginationForm, PaginationResult<Assignment>>(
        `${this.baseUrl}/courses/${id}/assignments`,
        form
      );
    }
}







