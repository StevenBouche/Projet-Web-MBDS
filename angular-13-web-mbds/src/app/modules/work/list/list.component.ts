import { Component, OnInit } from '@angular/core';
import { ComponentStateService } from 'app/core/componentstate/componentstate.service';
import { ComponentState } from 'app/core/componentstate/componentstate.types';

@Component({
  selector: 'app-work-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class WorkListComponent implements OnInit {


  constructor(private _stateService: ComponentStateService) { }

  ngOnInit(): void {
    this._stateService.setState(ComponentState.List);
  }

}
