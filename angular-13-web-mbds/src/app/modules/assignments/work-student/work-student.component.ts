import { Component, Input, OnInit } from "@angular/core";
import {
  CdkDragDrop,
  moveItemInArray,
  transferArrayItem,
} from "@angular/cdk/drag-drop";
import { WorksService } from "app/core/works/works.service";
import { Assignment } from "app/core/assignments/assignments.type";
import { Work } from "app/core/works/works.type";

@Component({
  selector: "app-assignment-work-student",
  templateUrl: "./work-student.component.html",
  styleUrls: ["./work-student.component.scss"],
})
export class WorkStudentComponent implements OnInit {

  @Input() assignment: Assignment | null = null;
  created: Work[] = [];
  submitted: Work[] = [];
  evaluated: Work[] = [];





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

  ) {
  }

  async ngOnInit(): Promise<void> {
    console.log(this.created);
    let response = await this._worksService.getAllSearchAsync({assignmentId: this.assignment!.id, pagesize: 10, page:1, state: 0});
    this.submitted = response.results;
    console.log(this.created);

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
