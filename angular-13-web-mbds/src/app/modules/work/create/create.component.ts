import { Component, OnInit } from '@angular/core';
import { ComponentStateService } from 'app/core/componentstate/componentstate.service';
import { ComponentState } from 'app/core/componentstate/componentstate.types';

@Component({
  selector: 'app-work-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class WorkCreateComponent implements OnInit {

  constructor(private _stateService: ComponentStateService) { }

  ngOnInit(): void {
    this._stateService.setState(ComponentState.Create);
  }

}
