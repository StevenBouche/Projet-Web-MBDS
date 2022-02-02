import { Component, Input, OnInit } from "@angular/core";
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
    private toast: ToastrService
  ) {}

  async ngOnInit(): Promise<void> {
    let responseCreated = await this._worksService.getAllSearchAsync({
      assignmentId: this.assignment!.id,
      pagesize: 10,
      page: 1,
      state: 0,
    });
    this.created = responseCreated.results;

    let responseSubmitted = await this._worksService.getAllSearchAsync({
      assignmentId: this.assignment!.id,
      pagesize: 10,
      page: 1,
      state: 1,
    });
    this.submitted = responseSubmitted.results;

    let responseEvaluated = await this._worksService.getAllSearchAsync({
      assignmentId: this.assignment!.id,
      pagesize: 10,
      page: 1,
      state: 2,
    });
    this.evaluated = responseEvaluated.results;
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
}
