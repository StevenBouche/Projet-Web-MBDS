import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AssignmentDetails } from 'app/core/assignments/assignments.type';
import { ComponentStateService } from 'app/core/componentstate/componentstate.service';
import { ComponentState } from 'app/core/componentstate/componentstate.types';

@Component({
  selector: 'app-assignment-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.scss']
})
export class AssignmentDetailsComponent implements OnInit {

  public assignment: AssignmentDetails | null = null;

  constructor(
    private _stateService: ComponentStateService,
    private _route: ActivatedRoute,
    ) { }

  ngOnInit(): void {
    this.assignment = this._route.snapshot.data.initialData.assignment;
    this._stateService.setState(ComponentState.Details);
  }

}
