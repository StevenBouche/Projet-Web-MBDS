import { Component, OnInit } from '@angular/core';
import { ComponentStateService } from 'app/core/componentstate/componentstate.service';
import { ComponentState } from 'app/core/componentstate/componentstate.types';

@Component({
  selector: 'app-work-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class WorkEditComponent implements OnInit {

  constructor(private _stateService: ComponentStateService) { }

  ngOnInit(): void {
    this._stateService.setState(ComponentState.Edit);
  }

}
