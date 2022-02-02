import { AfterViewInit, ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild } from "@angular/core";
import { FormBuilder, FormGroup, ValidatorFn, Validators, AbstractControl } from "@angular/forms";
import { Course, CoursePaginationResult } from "../../../core/courses/courses.type";
import { CoursesService } from "../../../core/courses/courses.service";
import { AssignmentsService } from "../../../core/assignments/assignments.service";
import { FormControl } from "@angular/forms";
import {
  Subject,
  takeUntil,
  debounceTime,
  distinctUntilChanged,
} from "rxjs";
import { ToastrService } from "ngx-toastr";
import { ComponentStateService } from "app/core/componentstate/componentstate.service";
import { ComponentState } from "app/core/componentstate/componentstate.types";
import { ActivatedRoute } from "@angular/router";
import { MatTableDataSource } from "@angular/material/table";
import { PaginationResult } from "app/core/api/api.types";
import { IdentityService } from "app/core/identity/identity.service";
import { UserIdentity } from "app/core/authentification/auth.types";
import { LiveAnnouncer } from "@angular/cdk/a11y";
import { MatSort, Sort } from "@angular/material/sort";

@Component({
  selector: "app-assignment-create",
  templateUrl: "./create.component.html",
  styleUrls: ["./create.component.scss"],
})
export class AssignmentCreateComponent implements OnInit, OnDestroy, AfterViewInit {

  // Form
  form: FormGroup;

  // Table
  @ViewChild(MatSort) sort!: MatSort;
  courses: Course[] = [];
  displayedColumns: string[] = ['id', 'name', 'owner'];
  dataSource = new MatTableDataSource<Course>([]);

  // Pagination
  private _paginationResult: CoursePaginationResult | null = null
  private _page = 1;
  private _pageSize = 5;
  get page() { return this._page; }
  set page(value) { this._page = value; this.getCourses(); }
  get pageSize() { return this._pageSize; }
  set pageSize(value) { this._pageSize = value; this.getCourses();}
  get total() { return this._paginationResult != null ? this._paginationResult.total : 0; }

  // Input filter table
  searchInputCourse: FormControl = new FormControl();
  private _searchValue = ''

  // Selection
  public selectedCourse: Course | null = null;

  //Others
  private _unsubscribeAll: Subject<any> = new Subject<any>();
  private identity: UserIdentity | null = null
  public isLoading: boolean = false;
  public courseIsPreload = false;

  constructor(
    private _formBuilder: FormBuilder,
    private courseService: CoursesService,
    private assignmentService: AssignmentsService,
    private toast: ToastrService,
    private _stateService: ComponentStateService,
    private _activatedRoute: ActivatedRoute,
    private _identityService: IdentityService,
    private _liveAnnouncer: LiveAnnouncer,
    ) {

    this.form = this._formBuilder.group({
      label: ['', [Validators.required]],
      courseId: ['', [Validators.required]],
      delivryDate: ['', [Validators.required]],
    });
  }

  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
  }

  ngOnDestroy(): void {
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }

  ngOnInit() {

    this._activatedRoute.queryParamMap
      .subscribe(async (params) => {
        const courseId = params.get('course');
        if(courseId != null){
          this.courseIsPreload = true;
          let response = await this.courseService.getByIdAsync(Number(courseId));
          this.updateMySelection(response);
          console.log(this.form)
        }
      });

      this.handleSearchCourse();

      this._stateService.setState(ComponentState.Create);
  }

  public getOptionText(option: Course | null) {
    return option ? option.name : "";
  }

  public onClickTable(row: Course): void{
    this.updateMySelection(row)
  }

  private updateMySelection(event: Course | null) {
    this.selectedCourse = event;
    this.form.controls['courseId'].setValue(event ? event.id : '');
  }

  private handleSearchCourse(): void {

    this._identityService.identity.pipe(takeUntil(this._unsubscribeAll)).subscribe(identity => { this.identity = identity; })

    this.searchInputCourse.valueChanges.pipe(takeUntil(this._unsubscribeAll),debounceTime(500),distinctUntilChanged())
      .subscribe(async (value: string | null) => {
        if(value) {
          this._searchValue = value;
          this._page = 1;
          this.getCourses()
        }
        else this._searchValue = ''
      });

      // Initialize table
      this.getCourses()
  }

  private async getCourses() : Promise<void>{

    if(!this.identity)
      return;

    this._paginationResult = await this.courseService.getAllSearchPaginationAsync({ page: this._page, pagesize: this._pageSize, courseName: this._searchValue, username: '', userId: this.identity.id });
    this.dataSource = new MatTableDataSource<Course>(this._paginationResult.results);

    if(this.selectedCourse){
      let element = this._paginationResult.results.find(u => this.selectedCourse != null && u.id === this.selectedCourse.id);
      this.updateMySelection(element ? element : null);
    }
  }

  async create(): Promise<void> {

    if(!this.selectedCourse)
      return;

    const assignment = this.form.getRawValue();
    assignment.courseId = this.selectedCourse.id;
    this.isLoading=true;

    try {
      const response = await this.assignmentService.createAsync(assignment);
      if(response) this.toast.success('Assignment created');
    } catch (error) {
      console.log(error);
    }

    this.isLoading=false;
  }

  public announceSortChange(sortState: Sort) {
    if (sortState.direction) {
      this._liveAnnouncer.announce(`Sorted ${sortState.direction}ending`);
    } else {
      this._liveAnnouncer.announce('Sorting cleared');
    }
  }
}

