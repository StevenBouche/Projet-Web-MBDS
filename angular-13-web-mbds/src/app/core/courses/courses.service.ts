/* eslint-disable @typescript-eslint/member-ordering */

import { Injectable } from '@angular/core';
import { HttpClient, HttpEvent, HttpEventType, HttpRequest, HttpResponse } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { ApiService } from 'app/core/api/api.service';
import { Course, CourseFormCreate, CourseFormUpdate, CourseSearchForm, CourseSearchFormResults } from './courses.type';
import { PaginationForm, PaginationResult } from '../api/api.types';
import { Assignment } from '../assignments/assignments.type';
import { BehaviorSubject, Observable } from 'rxjs';
import { ProgressAction } from '../core.types';

@Injectable({
  providedIn: 'root'
})
export class CoursesService extends ApiService {

  private store: {
    courseSelected: Course | null,
    assignmentsCourse: Array<Assignment>,
    pagination: PaginationForm
  } = {
      courseSelected: null,
      pagination: { page: 1, pagesize: 20 },
      assignmentsCourse: []
    };

  private readonly _assignmentsCourse = new BehaviorSubject<Array<Assignment>>(this.store.assignmentsCourse);
  private readonly _courseSelected = new BehaviorSubject<Course | null>(this.store.courseSelected);
  private readonly _pagination$ = new BehaviorSubject<PaginationResult<Course> | null>(null);

  public readonly assignmentsCourse = this._assignmentsCourse.asObservable();
  public readonly courseSelected = this._courseSelected.asObservable();
  public readonly pagination = this._pagination$.asObservable();

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

  public async setCourseSelected(course: Course): Promise<void> {
    this.store.courseSelected = this.store.courseSelected != null && this.store.courseSelected.id === course.id ? null : course;
    if(this.store.courseSelected != null){
      let resultAssignments = await this.executeGetAsync<Array<Assignment>>(`${this.baseUrl}/courses/${this.store.courseSelected.id}/assignments`);
      this.store.assignmentsCourse = resultAssignments;
      this._assignmentsCourse.next(resultAssignments);
    }
    this._courseSelected.next(this.store.courseSelected);
  }

  public async createAsync(form: CourseFormCreate): Promise<Course> {
    return this.executePostAsync<CourseFormCreate, Course>(`${this.baseUrl}/courses`, form);
  }

  public async updateAsync(form: CourseFormUpdate): Promise<Course> {
    return this.executePutAsync<CourseFormCreate, Course>(`${this.baseUrl}/courses`, form);
  }

  public getById(id: number): Observable<Course> {
    return this.http.get<Course>(`${this.baseUrl}/courses/${id}`);
  }

  public async getByIdAsync(id: number): Promise<Course> {
    return this.executeGetAsync<Course>(`${this.baseUrl}/courses/${id}`);
  }

  public async getAllAsync() {
    let result = await this.executePostAsync<PaginationForm, PaginationResult<Course>>(`${this.baseUrl}/courses/all`, this.store.pagination);
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
      `${this.baseUrl}/courses/mine`,
      form
    );
  }

  public async getAssignmentsOfCourseAsync(id: number, form: PaginationForm) {
    return this.executePostAsync<PaginationForm, PaginationResult<Assignment>>(
      `${this.baseUrl}/courses/${id}/assignments`,
      form
    );
  }

  public uploadPicture(id: number, file: File, callback: ProgressAction): void {

    callback({ value: 0, filename: file.name });

    const observer = {
      next: (event: any) => {
        if (event.type === HttpEventType.UploadProgress) callback({value: Math.round(100 * event.loaded / event.total), filename: file.name });
        else if (event instanceof HttpResponse) callback({ value: 100, filename: file.name});
      },
      error: (err: any) => {
        callback({ value: 0, filename: file.name });
      }
    }

    this.upload(id, file).subscribe(observer);
  }

  private upload(id: number, file: File): Observable<HttpEvent<any>> {

    const formData: FormData = new FormData();

    formData.append('file', file);

    const req = new HttpRequest('POST', `${this.baseUrl}/courseimages/upload/${id}`, formData, {
        reportProgress: true,
        responseType: 'json'
    });

    return this.http.request(req);
  }

  public sourceImage(idpicture: number){
    return idpicture ? `${this.baseUrl}/courseimages/${idpicture}` : 'assets/images/bg/bg1.jpg';
  }

  public sourceImageUser(idpicture: number){
    return idpicture ? `${this.baseUrl}/userprofilimages/${idpicture}` : 'assets/images/users/user1.jpg';
  }
}







