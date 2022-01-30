import { Component, OnInit } from '@angular/core';
import { CoursesService } from 'app/core/courses/courses.service';
import { ComponentState } from 'app/core/shared/shared.types';

@Component({
  selector: 'app-course-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.scss']
})
export class CourseDetailsComponent implements OnInit {

  constructor(private _coursesService: CoursesService) { }

  ngOnInit(): void {
    
  }

}
