import { ChangeDetectorRef, Component, OnDestroy, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, ValidatorFn, Validators, AbstractControl } from "@angular/forms";
import { Assignment } from "../../../core/assignments/assignments.type";
import { AssignmentsService } from "../../../core/assignments/assignments.service";
import { FormControl } from "@angular/forms";
import {
  Subject,
  takeUntil,
  debounceTime,
  distinctUntilChanged,
} from "rxjs";
import { ToastrService } from "ngx-toastr";
import { ComponentStateService } from "app/core/componentstate/componentstate.service";
import { ComponentState } from "app/core/componentstate/componentstate.types";
import { WorksService } from "app/core/works/works.service";
import { Course } from "app/core/courses/courses.type";
import { CoursesService } from "app/core/courses/courses.service";
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: "app-work-create",
  templateUrl: "./create.component.html",
  styleUrls: ["./create.component.scss"],
})
export class WorkCreateComponent implements OnInit, OnDestroy {
  form: FormGroup;
  assignments: Assignment[] = [];
  courses: Course[] = [];


  searchCourseControl: FormControl = new FormControl();
  searchAssignmentControl: FormControl = new FormControl();
  private _unsubscribeAll: Subject<any> = new Subject<any>();
  public selectedAssignment: Assignment | null = null;
  public selectedCourse: Course | null = null;

  public isLoading: boolean = false;
  public isPreload: boolean = false;

  updateMyAssignment(event: Assignment) {
    this.selectedAssignment = event;
    this.form.controls['assignmentId'].setValue(event.id);
  }

  async updateMyCourse(event: Course) {
    this.selectedCourse = event;
    this.form.controls['courseId'].setValue(event.id);

    if (!this.isPreload) {
      //update auto search assigment
      let responseAssignment = await this._assignmentService.getAllSearchAsync("", this.selectedCourse.id);
      this.assignments = responseAssignment.results;
      this._changeDetectorRef.markForCheck();
    }
  }

  getOptionAssignmentText(option: Assignment | null) {
    return option ? option.label : "";
  }

  getOptionCourseText(option: Course | null) {
    return option ? option.name : "";
  }

  constructor(
    private _assignmentService: AssignmentsService,
    private _courseService: CoursesService,
    private _stateService: ComponentStateService,
    private _workService: WorksService,
    private _activatedRoute: ActivatedRoute,
    private _changeDetectorRef: ChangeDetectorRef,
    private _formBuilder: FormBuilder,
    private toast: ToastrService
  ) {

    this.form = this._formBuilder.group({
      assignmentId: ['', [Validators.required]],
      courseId: ['', [Validators.required]],
      description: ['', [Validators.required]],
      label: ['', [Validators.required]],
    });
  }


  ngOnDestroy(): void {

  }

  ngOnInit() {

    this._activatedRoute.queryParamMap
      .subscribe(async (params) => {
        const courseId = params.get('course');
        const assignmentId = params.get('assignment');
        if (courseId != null && assignmentId != null) {
          let response = await this._courseService.getByIdAsync(Number(courseId));
          let response2 = await this._assignmentService.getByIdAsync(Number(assignmentId));
          if (response.id === response2.course.id) {
            this.isPreload = true;
            this.updateMyCourse(response);
            this.updateMyAssignment(response2);
          }
        }
      });

    this.searchAssignmentControl.valueChanges
      .pipe(
        takeUntil(this._unsubscribeAll),
        debounceTime(500),
        distinctUntilChanged()
      )
      .subscribe(async (value: string | Assignment | null) => {
        if (typeof value === 'string' || value instanceof String) {
          let response = await this._assignmentService.getAllSearchAsync(value as string, this.selectedCourse?.id);
          this.assignments = response.results;
          this._changeDetectorRef.markForCheck();
        }
      });

    this.searchCourseControl.valueChanges
      .pipe(
        takeUntil(this._unsubscribeAll),
        debounceTime(500),
        distinctUntilChanged()
      )
      .subscribe(async (value: string | Course | null) => {
        if (typeof value === 'string' || value instanceof String) {
          //RESET selectedCourse value
          this.selectedCourse = null;
          let response = await this._courseService.getAllSearchAsync(value as string);
          this.courses = response.results;
          this._changeDetectorRef.markForCheck();
        }
      });

    this._stateService.setState(ComponentState.Create);
  }

  async create(): Promise<void> {
    if (this.selectedAssignment) {
      const work = this.form.getRawValue();
      work.assignmentId = this.selectedAssignment.id;
      this.isLoading = true;
      try {
        const response = await this._workService.createAsync(
          work
        );
        if (response) {
          this.toast.success('Work created');
          this.isLoading = false;
        }
      } catch (error) {
        console.log(error);
      }
    }
  }
}

