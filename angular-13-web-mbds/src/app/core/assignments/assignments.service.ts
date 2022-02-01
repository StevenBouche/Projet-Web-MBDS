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
} from "./assignments.type";
import { BehaviorSubject } from "rxjs";

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

  private _assignmentSelected = new BehaviorSubject<Assignment | null>(
    this.store.assignmentSelected
  );
  private _pagination$ = new BehaviorSubject<PaginationResult<Assignment> | null>(
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
    return "Assignments";
  }

  constructor(http: HttpClient, toastr: ToastrService) {
    super(http, toastr);
  }

  public setAssignmentSelected(assignment: Assignment) {
    this.store.assignmentSelected = this.store.assignmentSelected != null && this.store.assignmentSelected.id === assignment.id ? null : assignment;
    this._assignmentSelected.next(this.store.assignmentSelected);
  }

  public async createAsync(form: AssignmentFormCreate): Promise<Assignment> {
    return this.executePostAsync<AssignmentFormCreate, Assignment>(
      `${this.baseUrl}/assignments`,
      form
    );
  }

  public async updateAsync(form: AssignmentFormUpdate): Promise<Assignment> {
    return this.executePutAsync<AssignmentFormCreate, Assignment>(
      `${this.baseUrl}/assignments`,
      form
    );
  }

  public async getByIdAsync(id: number): Promise<Assignment> {
    return this.executeGetAsync<Assignment>(
      `${this.baseUrl}/assignments/${id}`
    );
  }

  public async getAllAsync() {
    let result = await this.executePostAsync<
      PaginationForm,
      PaginationResult<Assignment>
    >(`${this.baseUrl}/assignments/all`, this.store.pagination);
    this._pagination$.next(result);
  }

  public async getAllIsMineAsync(form: PaginationForm) {
    return this.executePostAsync<PaginationForm, PaginationResult<Assignment>>(
      `${this.baseUrl}/assignments/mine`,
      form
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
}
