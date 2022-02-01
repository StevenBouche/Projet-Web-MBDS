import { ChangeDetectorRef, Component, OnDestroy, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, ValidatorFn, Validators, AbstractControl } from "@angular/forms";
import { Course } from "../../../core/courses/courses.type";
import { CoursesService } from "../../../core/courses/courses.service";
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

@Component({
  selector: "app-assignment-create",
  templateUrl: "./create.component.html",
  styleUrls: ["./create.component.scss"],
})
export class AssignmentCreateComponent implements OnInit, OnDestroy {
  form: FormGroup;
  courses: Course[] = [];

  searchInputControl: FormControl = new FormControl();
  private _unsubscribeAll: Subject<any> = new Subject<any>();
  public selectedCourse: Course | null = null;
  public isLoading: boolean = false;


  updateMySelection(event: Course) {
    this.selectedCourse = event;
    this.form.controls['courseId'].setValue(event.id);
  }

  getOptionText(option: Course | null) {
    return option ? option.name : "";
  }

  constructor(
    private _formBuilder: FormBuilder,
    private courseService: CoursesService,
    private assignmentService: AssignmentsService,
    private toast: ToastrService,
    private _stateService: ComponentStateService,
    private _changeDetectorRef: ChangeDetectorRef
  ) {

    this.form = this._formBuilder.group({
      label: ['', [Validators.required]],
      courseId: ['', [Validators.required]],
      delivryDate: ['', [Validators.required]],
    });
  }


  ngOnDestroy(): void {

  }

  ngOnInit() {
    this.searchInputControl.valueChanges
      .pipe(
        takeUntil(this._unsubscribeAll),
        debounceTime(500),
        distinctUntilChanged()
      )
      .subscribe(async (value: string | Course | null) => {
        if(typeof value === 'string' || value instanceof String) {
          let response = await this.courseService.getAllSearchAsync(value as string);
          this.courses = response.results;
          this._changeDetectorRef.markForCheck();
        }
      });

      this._stateService.setState(ComponentState.Create);
  }

  async create(): Promise<void> {
    if(this.selectedCourse) {
      const assignment = this.form.getRawValue();
      assignment.courseId = this.selectedCourse.id;
      this.isLoading=true;
      try {
        const response = await this.assignmentService.createAsync(
          assignment
        );
        if(response) {
          this.toast.success('Assignment created');
        }
      } catch (error) {
        console.log(error);
      }
    }
  }
}

