import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ComponentState } from 'app/core/componentstate/componentstate.types';
import { CoursesStateActions } from 'app/modules/courses/courses.component';

@Component({
  selector: 'app-navcomponent',
  templateUrl: './navcomponent.component.html',
  styleUrls: ['./navcomponent.component.scss']
})
export class NavcomponentComponent implements OnInit {

  @Input() stateActions: CoursesStateActions | null = null

  @Output() onCreate: EventEmitter<any> = new EventEmitter()
  @Output() onEdit: EventEmitter<any> = new EventEmitter()
  @Output() onDetails: EventEmitter<any> = new EventEmitter()
  @Output() onBack: EventEmitter<any> = new EventEmitter()

  constructor() { }

  ngOnInit(): void {
    //this.onChangeState.emit(this.state);
  }
}
