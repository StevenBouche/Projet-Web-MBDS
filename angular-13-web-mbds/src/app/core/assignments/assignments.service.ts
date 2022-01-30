/* eslint-disable @typescript-eslint/member-ordering */

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { ApiService } from 'app/core/api/api.service';
import { PaginationForm, PaginationResult } from '../api/api.types';
import { Assignment, AssignmentFormCreate, AssignmentFormUpdate } from './assignments.type';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AssignmentsService extends ApiService {


  private store: {
    paginationForm: PaginationForm
  } = {
    paginationForm: { pagesize: 20, page: 1,}
  };

  private _pagination$ = new BehaviorSubject<PaginationResult<Assignment>>(
    {
      pageSize: this.store.paginationForm.pagesize,
      page: this.store.paginationForm.page,
      totalPage: 0,
      total: 0,
      results: []
    }
  );

    getName(): string {
      return "Assignments";
    }

    constructor(http: HttpClient, toastr: ToastrService){
        super(http, toastr);
    }

    public async createAsync(form: AssignmentFormCreate): Promise<Assignment> {
      return this.executePostAsync<AssignmentFormCreate, Assignment>(`${this.baseUrl}/assignments`, form);
    }

    public async updateAsync(form: AssignmentFormUpdate): Promise<Assignment> {
      return this.executePutAsync<AssignmentFormCreate, Assignment>(`${this.baseUrl}/assignments`, form);
    }

    public async getByIdAsync(id: number): Promise<Assignment> {
      return this.executeGetAsync<Assignment>(`${this.baseUrl}/assignments/${id}`);
    }

    public async getAllAsync(form: PaginationForm){
      return this.executePostAsync<PaginationForm, PaginationResult<Assignment>>(
        `${this.baseUrl}/assignments/mine`,
        form
      );
    }

    public async getAllIsMineAsync(form: PaginationForm){
      return this.executePostAsync<PaginationForm, PaginationResult<Assignment>>(
        `${this.baseUrl}/assignments/all`,
        form
      );
    }

    public async getAssignmentsOfAssignmentAsync(id: number, form: PaginationForm){
      return this.executePostAsync<PaginationForm, PaginationResult<Assignment>>(
        `${this.baseUrl}/assignments/${id}/assignments`,
        form
      );
    }
}







