import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ComponentState } from 'app/core/shared/shared.types';
import { CoursesStateActions } from 'app/modules/courses/courses.component';

@Component({
  selector: 'app-navcomponent',
  templateUrl: './navcomponent.component.html',
  styleUrls: ['./navcomponent.component.scss']
})
export class NavcomponentComponent implements OnInit {

  private state: ComponentState = ComponentState.List
  private redirect: ComponentState | null = null

  @Input() stateActions: CoursesStateActions | null = null
  @Output() onChangeState = new EventEmitter<ComponentState>();

  constructor() { }

  ngOnInit(): void {
    this.onChangeState.emit(this.state);
  }

  public create(): void {
    this.state = ComponentState.Create;
    console.log(this.onChangeState)
    this.notifyChangeState();
  }

  public edit(): void {
    this.redirect = this.state;
    this.state = ComponentState.Edit;
    this.notifyChangeState();
  }

  public details(): void {
    this.redirect = ComponentState.List;
    this.state = ComponentState.Details;
    this.notifyChangeState();
  }

  public back(): void {
    this.state = this.redirect != null ? this.redirect : ComponentState.List;
    this.redirect = null;
    this.notifyChangeState();
  }

  private notifyChangeState(){
    this.onChangeState?.emit(this.state);
  }
}
