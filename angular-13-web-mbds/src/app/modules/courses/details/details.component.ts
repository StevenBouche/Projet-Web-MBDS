import { LiveAnnouncer } from '@angular/cdk/a11y';
import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { Assignment } from 'app/core/assignments/assignments.type';
import { ComponentStateService } from 'app/core/componentstate/componentstate.service';
import { ComponentState } from 'app/core/componentstate/componentstate.types';

import { CoursesService } from 'app/core/courses/courses.service';
import { Course } from 'app/core/courses/courses.type';
import { ImageHelper } from 'app/core/helpers/image.helper';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-course-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.scss']
})
export class CourseDetailsComponent implements OnInit, OnDestroy, AfterViewInit {

  public courseSelected: Course | null = null;
  public assignmentsCourse: Array<Assignment> = []
  private _unsubscribeAll: Subject<any> = new Subject<any>();

  displayedColumns: string[] = ['id', 'label', 'state', 'deliverydate', 'action'];
  dataSource = new MatTableDataSource<Assignment>([]);

  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private _coursesService: CoursesService,
    private _stateService: ComponentStateService,
    private _route: ActivatedRoute,
    private _router: Router,
    private _liveAnnouncer: LiveAnnouncer,
    public imageHelper: ImageHelper
    ) { }

  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
  }

  ngOnDestroy(): void {
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }

  ngOnInit(): void {

    this.courseSelected = this._route.snapshot.data.initialData.course;
    this.assignmentsCourse = this._route.snapshot.data.initialData.assignments;
    this.dataSource = new MatTableDataSource(this.assignmentsCourse);
    this._stateService.setState(ComponentState.Details);
  }

  public detailsAssignments(id: number){
    this._router.navigate(
      [`/assignments/details/${id}`],
      { queryParams: { 'redirect': `/courses/details/${this.courseSelected?.id}` } }
    );
  }

  /** Announce the change in sort state for assistive technology. */
  public announceSortChange(sortState: Sort) {
    // This example uses English messages. If your application supports
    // multiple language, you would internationalize these strings.
    // Furthermore, you can customize the message to add additional
    // details about the values being sorted.
    if (sortState.direction) {
      this._liveAnnouncer.announce(`Sorted ${sortState.direction}ending`);
    } else {
      this._liveAnnouncer.announce('Sorting cleared');
    }
  }
}
