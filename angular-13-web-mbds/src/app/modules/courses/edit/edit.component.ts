import { Component, OnInit } from '@angular/core';
import { CoursesService } from 'app/core/courses/courses.service';
import { ComponentState } from 'app/core/shared/shared.types';

@Component({
  selector: 'app-course-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent implements OnInit {

  constructor(private _coursesService: CoursesService) { }

  ngOnInit(): void {
    this._coursesService.setStateComponent(ComponentState.Edit);
  }

}
