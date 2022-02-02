import { Component, Input, OnInit } from "@angular/core";
import {
  CdkDragDrop,
  moveItemInArray,
  transferArrayItem,
} from "@angular/cdk/drag-drop";
import { WorksService } from "app/core/works/works.service";
import { Assignment } from "app/core/assignments/assignments.type";
import { Work } from "app/core/works/works.type";
import { AssignmentsService } from "app/core/assignments/assignments.service";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ToastrService } from "ngx-toastr";
import { Router } from "@angular/router";

@Component({
  selector: "app-assignment-work-student",
  templateUrl: "./work-student.component.html",
  styleUrls: ["./work-student.component.scss"],
})
export class WorkStudentComponent implements OnInit {
  @Input() assignment: Assignment | null = null;
  work: Work | null = null;
  form: FormGroup;

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
    private _assignmentService: AssignmentsService,
    private _formBuilder: FormBuilder,
    private toast: ToastrService,
    private _router: Router
  ) {
    // Create the form
    this.form = this._formBuilder.group({
      label: ["", Validators.required],
      description: ["", Validators.required],
    });
  }

  async ngOnInit(): Promise<void> {
    let response = await this._assignmentService.getWorkById(
      this.assignment!.id
    );
    this.work = response;
    if (this.work) {
      this.form.patchValue({
        label: this.work.label,
        description: this.work.description,
      });
    }
  }

  async save(): Promise<void> {
    if (this.work) {
      try {
        const response = await this._worksService.updateWorkAsync({
          label: this.form.value.label,
          description: this.form.value.description,
          id: this.work.id,
          assignmentId: this.assignment!.id,
        });
        if (response) {
          this.toast.success("Work saved");
        }
      } catch (error) {
        console.log(error);
      }
    }
  }

  async submit(): Promise<void> {
    if (this.work && this.assignment && this.assignment.state === 0 ) {
      await this.save();
      try {
        const response = await this._worksService.submitWorkAsync({
          id: this.work.id,
        });
        if (response) {
          this.toast.success("Work submitted !");
          this.ngOnInit();
        }
      } catch (error) {
        console.log(error);
      }
    }
    else {
      this.toast.error('Assignment is closed, cannot submit your work !')
    }
  }

  public createWork(): void{

    console.log(this.assignment)
    if(!this.assignment)
      return;

    if(this.assignment.state === 1)
      return;

    this._router.navigate(
      [`/works/create`],
      { queryParams: { 'redirect': `/assignments/details/${this.assignment.id}`,  'course': `${this.assignment.course.id}`, 'assignment': `${this.assignment.id}` } }
    );
  }
}
