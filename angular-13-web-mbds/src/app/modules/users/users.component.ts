import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { ComponentState, ComponentStateActions, StateAction } from 'app/core/componentstate/componentstate.types';
import { CoursesStateActions } from '../courses/courses.component';
import { User } from 'app/core/users/users.types';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {

  private state: ComponentState = ComponentState.None

  get editMode() { return this.state === ComponentState.Edit }



  constructor(
    private _router: Router,
    private _activatedRoute: ActivatedRoute
  ) { }

  ngOnInit(): void {
  }

}
