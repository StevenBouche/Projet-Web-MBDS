import { Component, Input, OnInit, ViewChild } from "@angular/core";
import {CollectionViewer, DataSource} from '@angular/cdk/collections';
import { CdkVirtualScrollViewport } from '@angular/cdk/scrolling';
import {
  CdkDragDrop,
  moveItemInArray,
  transferArrayItem,
} from "@angular/cdk/drag-drop";
import { WorksService } from "app/core/works/works.service";
import { Assignment } from "app/core/assignments/assignments.type";
import { Work } from "app/core/works/works.type";
import { FormGroup } from "@angular/forms";
import { ToastrService } from "ngx-toastr";
import {BehaviorSubject, Observable, Subscription} from 'rxjs';


export interface ClassePageIndex{
  pageIndex:number;
  label:string;
}
@Component({
  selector: "app-assignment-work-professor",
  templateUrl: "./work-professor.component.html",
  styleUrls: ["./work-professor.component.scss"],
})

export class WorkProfessorComponent implements OnInit {
  @Input() assignment: Assignment | null = null;
  created: Work[] = [];
  submitted: Work[] = [];
  evaluated: Work[] = [];
  createdPageindex: ClassePageIndex = {label: "created", pageIndex: 1};
  submittedPageindex: ClassePageIndex = {label: "submitted", pageIndex: 1};
  evaluatedPageindex: ClassePageIndex = {label: "evaluated", pageIndex: 1};



  drop(event: CdkDragDrop<string[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );
    } else {
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );
    }
  }

  constructor(
    private _worksService: WorksService,
    private toast: ToastrService,
  ) {}


  async getWorks(pageIndex: number, state: number): Promise<Work[]> {
    let response = await this._worksService.getAllSearchAsync({
      assignmentId: this.assignment!.id,
      pagesize: 3,
      page: pageIndex,
      state: state,
    });
    return response.results;
  }
  async ngOnInit(): Promise<void> {

    this.created = await this.getWorks(1, 0);
    this.submitted = await this.getWorks(1, 1);
    this.evaluated = await this.getWorks(1, 2);

  }

  async onSave(form: FormGroup): Promise<void> {
    try {
      const response = await this._worksService.updateEvalAsync({
        grade: form.value.grade,
        comment: form.value.comment,
        id: form.value.id,
      });
      if (response) {
        this.toast.success("Work evaluation saved");
      }
    } catch (error) {
      console.log(error);
    }
  }

  async onSubmit(form: FormGroup): Promise<void> {
    await this.onSave(form);
    try {
      const response = await this._worksService.submitEvalAsync({
        id: form.value.id,
      });
      if (response) {
        this.toast.success("Work evaluation submitted !");
        this.ngOnInit();
      }
    } catch (error) {
      console.log(error);
    }
  }

  async onScrollCreated() {
    this.createdPageindex.pageIndex++;
    this.created = [...this.created .concat(await this.getWorks(this.createdPageindex.pageIndex, 0))];
  }

  async onScrollSubmitted() {
    this.submittedPageindex.pageIndex++;
    this.submitted  = [...this.submitted .concat(await this.getWorks(this.submittedPageindex.pageIndex, 1))];
  }

  async onScrollEvaluated() {
    this.evaluatedPageindex.pageIndex++;
    this.evaluated  = [...this.evaluated .concat(await this.getWorks(this.evaluatedPageindex.pageIndex, 2))];
  }



}
