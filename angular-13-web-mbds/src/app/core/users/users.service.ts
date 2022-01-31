/* eslint-disable @typescript-eslint/member-ordering */

import { Injectable } from "@angular/core";
import { HttpClient, HttpEvent, HttpEventType, HttpRequest, HttpResponse } from "@angular/common/http";
import { ToastrService } from "ngx-toastr";
import { ApiService } from "app/core/api/api.service";
import { PaginationForm, PaginationResult } from "../api/api.types";
import {
  User, UserFormUpdate, UserFormCreate,
} from "./users.types";
import { BehaviorSubject, Observable } from "rxjs";
import { ProgressAction } from "../core.types";

@Injectable({
  providedIn: "root",
})
export class UsersService extends ApiService {
  private store: {
    assignmentSelected: User | null;
    pagination: PaginationForm;
  } = {
    assignmentSelected: null,
    pagination: { page: 1, pagesize: 20 },
  };

  private _assignmentSelected = new BehaviorSubject<User | null>(
    this.store.assignmentSelected
  );
  private _pagination$ = new BehaviorSubject<PaginationResult<User> | null>(
    null
  );

  public assignmentSelected = this._assignmentSelected.asObservable();
  public pagination = this._pagination$.asObservable();

  get page() {
    return this.store.pagination.page;
  }
  set page(value) {
    this.store.pagination.page = value;
    this.getAllAsync();
  }

  get pagesize() {
    return this.store.pagination.pagesize;
  }
  set pagesize(value) {
    this.store.pagination.pagesize = value;
    this.getAllAsync();
  }

  getName(): string {
    return "Users";
  }

  constructor(http: HttpClient, toastr: ToastrService) {
    super(http, toastr);
  }

  public setUserSelected(assignment: User) {
    this.store.assignmentSelected = this.store.assignmentSelected != null && this.store.assignmentSelected.id === assignment.id ? null : assignment;
    this._assignmentSelected.next(this.store.assignmentSelected);
  }

  public async createAsync(form: UserFormCreate): Promise<User> {
    return this.executePostAsync<UserFormCreate, User>(
      `${this.baseUrl}/users`,
      form
    );
  }

  public async updateAsync(form: UserFormUpdate): Promise<User> {
    return this.executePutAsync<UserFormUpdate, User>(
      `${this.baseUrl}/users`,
      form
    );
  }

  public async getByIdAsync(id: number): Promise<User> {
    return this.executeGetAsync<User>(
      `${this.baseUrl}/users/${id}`
    );
  }

  public async getAllAsync() {
    let result = await this.executePostAsync<
      PaginationForm,
      PaginationResult<User>
    >(`${this.baseUrl}/users/mine`, this.store.pagination);
    this._pagination$.next(result);
  }

  public async getAllIsMineAsync(form: PaginationForm) {
    return this.executePostAsync<PaginationForm, PaginationResult<User>>(
      `${this.baseUrl}/users/all`,
      form
    );
  }

  public async getUsersOfUserAsync(
    id: number,
    form: PaginationForm
  ) {
    return this.executePostAsync<PaginationForm, PaginationResult<User>>(
      `${this.baseUrl}/users/${id}/users`,
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


  public sourceImageUser(idpicture: number){
    return idpicture ? `${this.baseUrl}/userprofilimages/${idpicture}` : 'assets/images/users/user1.jpg';
  }
}
