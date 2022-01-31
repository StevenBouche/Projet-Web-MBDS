import { Component, OnInit } from '@angular/core';
import { ComponentStateService } from 'app/core/componentstate/componentstate.service';
import { ComponentState } from 'app/core/componentstate/componentstate.types';
import { CoursesService } from 'app/core/courses/courses.service';


@Component({
  selector: 'app-course-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent implements OnInit {

  constructor(private _coursesService: CoursesService, private _stateService: ComponentStateService) { }

  ngOnInit(): void {
    this._stateService.setState(ComponentState.Edit);
  }

}
