/* eslint-disable @typescript-eslint/member-ordering */

import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ToastrService } from "ngx-toastr";
import { ApiService } from "app/core/api/api.service";
import { PaginationForm, PaginationResult } from "../api/api.types";
import {
  Assignment,
  AssignmentFormCreate,
  AssignmentFormUpdate,
  AssignmentSearchFormResults,
  AssignmentSearchForm,
} from "./assignments.type";
import { BehaviorSubject, Observable } from "rxjs";
import { User } from "../users/users.types";
import { Work } from "../works/works.type";

@Injectable({
  providedIn: "root",
})
export class AssignmentsService extends ApiService {
  private store: {
    assignmentSelected: Assignment | null;
    pagination: PaginationForm;
  } = {
    assignmentSelected: null,
    pagination: { page: 1, pagesize: 20 },
  };

  private _assignmentSelected = new BehaviorSubject<Assignment | null>(this.store.assignmentSelected);
  private _pagination$ = new BehaviorSubject<PaginationResult<Assignment> | null>(null);

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
    return "Assignments";
  }

  constructor(http: HttpClient, toastr: ToastrService) {
    super(http, toastr);
  }

  public setAssignmentSelected(assignment: Assignment | null) {

    if(this.store.assignmentSelected != null){
      if(assignment != null) this.store.assignmentSelected = this.store.assignmentSelected.id === assignment.id ? null : assignment;
      else this.store.assignmentSelected = null;
    }
    else if(assignment != null) this.store.assignmentSelected = assignment;

    this._assignmentSelected.next(this.store.assignmentSelected);
  }

  public setAssignmentDetailsSelected(assignment: Assignment | null) {
    this._assignmentSelected.next(assignment);
  }

  public async createAsync(form: AssignmentFormCreate): Promise<Assignment> {
    return this.executePostAsync<AssignmentFormCreate, Assignment>(
      `${this.baseUrl}/assignments`,
      form
    );
  }

  public async updateAsync(form: AssignmentFormUpdate): Promise<Assignment> {
    return this.executePutAsync<AssignmentFormUpdate, Assignment>(
      `${this.baseUrl}/assignments`,
      form
    );
  }

  public async getByIdAsync(id: number): Promise<Assignment> {
    return this.executeGetAsync<Assignment>(
      `${this.baseUrl}/assignments/${id}`
    );
  }

  public getAssignmentDetails(id: number): Observable<Assignment>{
    return this.http.get<Assignment>(
      `${this.baseUrl}/assignments/${id}/details`
    );
  }

  public async getAllAsync() {
    let result = await this.executePostAsync<
      PaginationForm,
      PaginationResult<Assignment>
    >(`${this.baseUrl}/assignments/all`, this.store.pagination);
    this.setPagination(result);
  }

  public async getAllSearchAsync(term: string, courseId: number | null = null): Promise<AssignmentSearchFormResults> {
    return this.executePostAsync<AssignmentSearchForm, AssignmentSearchFormResults>(
      `${this.baseUrl}/assignments/search`,
      {term, courseId}
    );
  }

  public async getAllIsMineAsync(form: PaginationForm) {
    return this.executePostAsync<PaginationForm, PaginationResult<Assignment>>(
      `${this.baseUrl}/assignments/mine`,
      form
    );
  }

  public async getWorkById(id: number): Promise<Work> {
    return this.executeGetAsync<Work>(
      `${this.baseUrl}/assignments/${id}/work/mine`
    );
  }

  public async getAssignmentsOfAssignmentAsync(
    id: number,
    form: PaginationForm
  ) {
    return this.executePostAsync<PaginationForm, PaginationResult<Assignment>>(
      `${this.baseUrl}/assignments/${id}/assignments`,
      form
    );
  }

  public async changeStateAssignment(id: number, state: boolean){
    const method = state ? 'open' : 'close';
    return this.executePutAsync<any, Assignment>(
      `${this.baseUrl}/assignments/${method}/${id}`,
      { id: id }
    );
  }

  private setPagination(pagination: PaginationResult<Assignment>): void{
    if(this.store.assignmentSelected !== null){
      let element = pagination.results.find(u => this.store.assignmentSelected != null && u.id === this.store.assignmentSelected.id)
      this.setAssignmentSelected(element ? element : null)
    }
    this._pagination$.next(pagination);
  }
}
