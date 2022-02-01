import { Component, OnInit } from '@angular/core';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { ActivatedRoute } from '@angular/router';
import { AssignmentsService } from 'app/core/assignments/assignments.service';
import { Assignment } from 'app/core/assignments/assignments.type';
import { UserIdentity } from 'app/core/authentification/auth.types';
import { AuthorizeService } from 'app/core/authorize/authorize.service';
import { ComponentStateService } from 'app/core/componentstate/componentstate.service';
import { ComponentState } from 'app/core/componentstate/componentstate.types';
import { ImageHelper } from 'app/core/helpers/image.helper';
import { environment } from 'environments/environment';

@Component({
  selector: 'app-assignment-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.scss']
})
export class AssignmentDetailsComponent implements OnInit {

  public assignment: Assignment | null = null;
  public user: UserIdentity | null = null;
  public loadingState = false;

  get canEditAssignmentState() {
    return this._authorizeService.isOwnerOfAssignment(this.assignment) || this.loadingState;
  }

  get isProfessor() { return this._authorizeService.isProfessor(); }
  get isStudent() { return this._authorizeService.isStudent(); }

  constructor(
    private _stateService: ComponentStateService,
    private _route: ActivatedRoute,
    private _service: AssignmentsService,
    private _authorizeService: AuthorizeService,
    public imageHelper: ImageHelper
    ) {
    }

  ngOnInit(): void {
    this.assignment = this._route.snapshot.data.initialData.assignment;
    this.user = this._route.snapshot.data.initialData.user;
    this._service.setAssignmentDetailsSelected(this.assignment);
    this._stateService.setState(ComponentState.Details);
  }

  public detailsCourse(idCourse : number){

  }

  public async changeState(event : MatSlideToggleChange) : Promise<void> {
    if(!this.assignment)
      return;

    this.loadingState = true;
    const result = await this._service.changeStateAssignment(this.assignment.id, event.checked)
    this.assignment.state = result.state;
    this.assignment.stateLabel = result.stateLabel;
    this._service.setAssignmentDetailsSelected(this.assignment);
    this.loadingState = false;
  }
}
