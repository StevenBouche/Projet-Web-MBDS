/* eslint-disable @typescript-eslint/member-ordering */

import { Injectable } from "@angular/core";
import {
  HttpClient,
} from "@angular/common/http";
import { ToastrService } from "ngx-toastr";
import { ApiService } from "app/core/api/api.service";
import {
  Work,
  WorkFormCreate,
  WorkFormSubmitEvaluation,
  WorkFormSubmitWork,
  WorkFormUpdateEvaluation,
  WorkFormUpdateWork,
  WorkPaginationForm,
  WorkPaginationResult,
} from "./works.type";
import { PaginationForm, PaginationResult } from "../api/api.types";
import { Assignment } from "../assignments/assignments.type";
import { BehaviorSubject, catchError, map, Observable, of } from "rxjs";
import { ProgressAction } from "../core.types";

@Injectable({
  providedIn: "root",
})
export class WorksService extends ApiService {
  private store: {
    courseSelected: Work | null;
    assignmentsWork: Array<Assignment>;
    pagination: PaginationForm;
  } = {
    courseSelected: null,
    pagination: { page: 1, pagesize: 20 },
    assignmentsWork: [],
  };

  private readonly _assignmentsWork = new BehaviorSubject<Array<Assignment>>(
    this.store.assignmentsWork
  );
  private readonly _courseSelected = new BehaviorSubject<Work | null>(
    this.store.courseSelected
  );
  private readonly _pagination$ =
    new BehaviorSubject<PaginationResult<Work> | null>(null);

  public readonly assignmentsWork = this._assignmentsWork.asObservable();
  public readonly courseSelected = this._courseSelected.asObservable();
  public readonly pagination = this._pagination$.asObservable();

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
    return "Works";
  }

  constructor(http: HttpClient, toastr: ToastrService) {
    super(http, toastr);
  }

  public async setWorkSelected(course: Work): Promise<void> {
    this.store.courseSelected =
      this.store.courseSelected != null &&
      this.store.courseSelected.id === course.id
        ? null
        : course;
    if (this.store.courseSelected != null) {
      let resultAssignments = await this.executeGetAsync<Array<Assignment>>(
        `${this.baseUrl}/courses/${this.store.courseSelected.id}/assignments`
      );
      this.store.assignmentsWork = resultAssignments;
      this._assignmentsWork.next(resultAssignments);
    }
    this._courseSelected.next(this.store.courseSelected);
  }

  public async createAsync(form: WorkFormCreate): Promise<Work> {
    return this.executePostAsync<WorkFormCreate, Work>(
      `${this.baseUrl}/worksubmits`,
      form
    );
  }

  public async submitEvalAsync(form: WorkFormSubmitEvaluation): Promise<Work> {
    return this.executePutAsync<WorkFormSubmitEvaluation, Work>(
      `${this.baseUrl}/worksubmits/submit-evaluation`,
      form
    );
  }

  public async submitWorkAsync(form: WorkFormSubmitWork): Promise<Work> {
    return this.executePutAsync<WorkFormSubmitWork, Work>(
      `${this.baseUrl}/worksubmits/submit-work`,
      form
    );
  }

  public async updateEvalAsync(form: WorkFormUpdateEvaluation): Promise<Work> {
    return this.executePutAsync<WorkFormUpdateEvaluation, Work>(
      `${this.baseUrl}/worksubmits/evaluation`,
      form
    );
  }

  public async updateWorkAsync(form: WorkFormUpdateWork): Promise<Work> {
    return this.executePutAsync<WorkFormUpdateWork, Work>(
      `${this.baseUrl}/worksubmits/work`,
      form
    );
  }

  public async getAllSearchAsync(form: WorkPaginationForm): Promise<WorkPaginationResult> {
    return this.executePostAsync<WorkPaginationForm, WorkPaginationResult>(
      `${this.baseUrl}/worksubmits/search`,
      form
    );
  }

  public getById(id: number): Observable<Work> {
    return this.http.get<Work>(`${this.baseUrl}/worksubmits/${id}`);
  }

  public async getByIdAsync(id: number): Promise<Work> {
    return this.executeGetAsync<Work>(`${this.baseUrl}/worksubmits/${id}`);
  }

  public async getAllAsync() {
    let result = await this.executePostAsync<
      PaginationForm,
      PaginationResult<Work>
    >(`${this.baseUrl}/worksubmits/all`, this.store.pagination);
    this._pagination$.next(result);
  }


  // public uploadPicture(id: number, file: File, callback: ProgressAction): void {
  //   callback({ value: 0, filename: file.name });

  //   const observer = {
  //     next: (event: any) => {
  //       if (event.type === HttpEventType.UploadProgress)
  //         callback({
  //           value: Math.round((100 * event.loaded) / event.total),
  //           filename: file.name,
  //         });
  //       else if (event instanceof HttpResponse)
  //         callback({ value: 100, filename: file.name });
  //     },
  //     error: (err: any) => {
  //       callback({ value: 0, filename: file.name });
  //     },
  //   };

  //   this.upload(id, file).subscribe(observer);
  // }

  // public getImageFileWork(id: number): Observable<File | null> {
  //   return this.http
  //     .get(`${this.baseUrl}/courseimages/course/${id}`, {
  //       responseType: "blob",
  //     })
  //     .pipe(
  //       map((blob: Blob) => new File([blob], "current", { type: blob.type })),
  //       catchError<File | null, Observable<File | null>>((_) => of(null))
  //     );
  // }

  // private upload(id: number, file: File): Observable<HttpEvent<any>> {
  //   const formData: FormData = new FormData();

  //   formData.append("file", file);

  //   const req = new HttpRequest(
  //     "POST",
  //     `${this.baseUrl}/courseimages/upload/${id}`,
  //     formData,
  //     {
  //       reportProgress: true,
  //       responseType: "json",
  //     }
  //   );

  //   return this.http.request(req);
  // }


}
