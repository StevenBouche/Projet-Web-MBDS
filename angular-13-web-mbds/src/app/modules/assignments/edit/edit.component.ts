import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AssignmentsService } from 'app/core/assignments/assignments.service';
import { ComponentStateService } from 'app/core/componentstate/componentstate.service';
import { ComponentState } from 'app/core/componentstate/componentstate.types';

@Component({
  selector: 'app-assignment-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent implements OnInit {

  form: FormGroup;
  public isLoading: boolean = false;

  constructor(
    private _stateService: ComponentStateService,
    private _formBuilder: FormBuilder,
    private _route: ActivatedRoute,
    private _service: AssignmentsService
  ) {
    this.form = this._formBuilder.group({
      id: ['', [Validators.required]],
      label: ['', [Validators.required]],
      courseId: ['', [Validators.required]],
      delivryDate: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
    const assignment = this._route.snapshot.data.initialData.assignment;
    this._service.setAssignmentDetailsSelected(assignment);

    this.form.patchValue({
      id: assignment.id,
      label: assignment.label,
      delivryDate: assignment.delivryDate,
      courseId : assignment.courseId
    });

    this._stateService.setState(ComponentState.Edit);
  }

  public update(): void {

  }
}
