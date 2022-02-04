import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AssignmentsService } from 'app/core/assignments/assignments.service';
import { ComponentStateService } from 'app/core/componentstate/componentstate.service';
import { ComponentState } from 'app/core/componentstate/componentstate.types';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-assignment-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent implements OnInit {

  form: FormGroup;
  public isLoading: boolean = false;

  get canUpdate() { return this.form.invalid || this.form.disabled; }

  constructor(
    private _stateService: ComponentStateService,
    private _formBuilder: FormBuilder,
    private _route: ActivatedRoute,
    private _service: AssignmentsService,
    private _toastr: ToastrService
  ) {
    this.form = this._formBuilder.group({
      id: ['', [Validators.required]],
      label: ['', [Validators.required]],
      delivryDate: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
    const assignment = this._route.snapshot.data.initialData.assignment;
    this._service.setAssignmentDetailsSelected(assignment);

    this.form.patchValue({
      id: assignment.id,
      label: assignment.label,
      delivryDate: assignment.delivryDate
    });

    this._stateService.setState(ComponentState.Edit);
  }

  public async update(): Promise<void> {

    if (this.canUpdate) return;

    this.form.disable();

    try {
      const ass = this.form.getRawValue();
      await this._service.updateAsync(ass);
      this._toastr.success('Course is updated');
    }
    catch (error) {
      this.form.enable();
    }
  }
}
