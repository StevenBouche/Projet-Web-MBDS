import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { PaginationForm, PaginationResult } from 'app/core/api/api.types';
import { AssignmentsService } from 'app/core/assignments/assignments.service';
import { Assignment } from 'app/core/assignments/assignments.type';
import { ComponentStateService } from 'app/core/componentstate/componentstate.service';
import { ComponentState } from 'app/core/componentstate/componentstate.types';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-assignment-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class AssignmentListComponent implements OnInit, OnDestroy {

  public hideRequiredControl = new FormControl(false);

  public assignmentSelected: Assignment | null = null
  public paginationResult: PaginationResult<Assignment> | null = null;

  get page() { return this._assignmentsService.page; }
  set page(value) { this._assignmentsService.page = value; }

  get pageSize() { return this._assignmentsService.pagesize; }
  set pageSize(value) { this._assignmentsService.pagesize = value; }

  get total() { return this.paginationResult != null ? this.paginationResult.total : 0; }

  private _unsubscribeAll: Subject<any> = new Subject<any>();

  constructor(private _assignmentsService: AssignmentsService, private _stateService: ComponentStateService) { }

  ngOnDestroy(): void {
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }

  ngOnInit(): void {
    this._assignmentsService.pagination
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe((paginationResult: PaginationResult<Assignment> | null) => {
        this.paginationResult = paginationResult;
      })

      this._assignmentsService.assignmentSelected
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe((assignment: Assignment | null) => {
        this.assignmentSelected = assignment;
      })

      this._stateService.setState(ComponentState.List);
      this._assignmentsService.getAllAsync();
  }

  onClickItem(assignment: Assignment){
    this._assignmentsService.setAssignmentSelected(assignment);
  }
}
