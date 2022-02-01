import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
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
  ) {
    this.form = this._formBuilder.group({
      label: ['', [Validators.required]],
      courseId: ['', [Validators.required]],
      delivryDate: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
    this._stateService.setState(ComponentState.Edit);
  }

  public update(): void {

  }
}
