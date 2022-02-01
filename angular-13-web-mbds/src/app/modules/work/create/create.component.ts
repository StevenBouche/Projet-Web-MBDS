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

@Component({
  selector: "app-work-create",
  templateUrl: "./create.component.html",
  styleUrls: ["./create.component.scss"],
})
export class WorkCreateComponent implements OnInit, OnDestroy {
  form: FormGroup;
  assignments: Assignment[] = [];

  searchInputControl: FormControl = new FormControl();
  private _unsubscribeAll: Subject<any> = new Subject<any>();
  public selectedAssignment: Assignment | null = null;
  public isLoading: boolean = false;


  updateMySelection(event: Assignment) {
    this.selectedAssignment = event;
    this.form.controls['assignmentId'].setValue(event.id);
  }

  getOptionText(option: Assignment | null) {
    return option ? option.label : "";
  }

  constructor(
    private _assignmentService: AssignmentsService,
    private _changeDetectorRef: ChangeDetectorRef,
    private _formBuilder: FormBuilder,
    private _stateService: ComponentStateService,
    private _workService: WorksService,
    private toast: ToastrService
  ) {

    this.form = this._formBuilder.group({
      assignmentId: ['', [Validators.required]],
      description: ['', [Validators.required]],
      label: ['', [Validators.required]],
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
      .subscribe(async (value: string | Assignment | null) => {
        if(typeof value === 'string' || value instanceof String) {
          let response = await this._assignmentService.getAllSearchAsync(value as string);
          this.assignments = response.results;
          this._changeDetectorRef.markForCheck();
        }
      });

      this._stateService.setState(ComponentState.Create);
  }

  async create(): Promise<void> {
    if(this.selectedAssignment) {
      const assignment = this.form.getRawValue();
      assignment.assignmentId = this.selectedAssignment.id;
      this.isLoading=true;
      try {
        const response = await this._assignmentService.createAsync(
          assignment
        );
        if(response) {
          this.toast.success('Assignment created');
          this.isLoading=false;
        }
      } catch (error) {
        console.log(error);
      }
    }
  }
}

