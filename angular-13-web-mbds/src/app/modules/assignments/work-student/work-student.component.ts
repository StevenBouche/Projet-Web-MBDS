import { Component, Input, OnInit } from "@angular/core";
import {
  CdkDragDrop,
  moveItemInArray,
  transferArrayItem,
} from "@angular/cdk/drag-drop";
import { WorksService } from "app/core/works/works.service";
import { Assignment } from "app/core/assignments/assignments.type";
import { Work } from "app/core/works/works.type";
import { AssignmentsService } from "app/core/assignments/assignments.service";

@Component({
  selector: "app-assignment-work-student",
  templateUrl: "./work-student.component.html",
  styleUrls: ["./work-student.component.scss"],
})
export class WorkStudentComponent implements OnInit {

  @Input() assignment: Assignment | null = null;
  work: Work | null = null;




  drop(event: CdkDragDrop<string[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );
    } else {
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );
    }
  }

  constructor(

    private _worksService: WorksService,
    private _assignmentService: AssignmentsService,


  ) {
  }

  async ngOnInit(): Promise<void> {
    let response = await this._assignmentService.getWorkById(this.assignment!.id);
    this.work = response;
    console.log(this.work);

    // this.created = response;
    // this.searchInputControl.valueChanges
    //   .pipe(
    //     takeUntil(this._unsubscribeAll),
    //     debounceTime(500),
    //     distinctUntilChanged()
    //   )
    //   .subscribe(async (value: string | Course | null) => {
    //     if (typeof value === "string" || value instanceof String) {
    //       let response = await this.courseService.getAllSearchAsync(
    //         value as string
    //       );
    //       this.courses = response.results;
    //       this._changeDetectorRef.markForCheck();
    //     }
    //   });
    // this._stateService.setState(ComponentState.Create);
  }
}
