import { Component, OnInit } from '@angular/core';
import { ComponentStateService } from 'app/core/componentstate/componentstate.service';
import { ComponentState } from 'app/core/componentstate/componentstate.types';

@Component({
  selector: 'app-assignment-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.scss']
})
export class AssignmentDetailsComponent implements OnInit {

  constructor(private _stateService: ComponentStateService) { }

  ngOnInit(): void {
    this._stateService.setState(ComponentState.Details);
  }

}
