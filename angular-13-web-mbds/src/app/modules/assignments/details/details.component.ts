import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AssignmentsService } from 'app/core/assignments/assignments.service';
import { AssignmentDetails } from 'app/core/assignments/assignments.type';
import { ComponentStateService } from 'app/core/componentstate/componentstate.service';
import { ComponentState } from 'app/core/componentstate/componentstate.types';
import { environment } from 'environments/environment';

@Component({
  selector: 'app-assignment-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.scss']
})
export class AssignmentDetailsComponent implements OnInit {

  public assignment: AssignmentDetails | null = null;

  constructor(
    private _stateService: ComponentStateService,
    private _route: ActivatedRoute,
    private _service: AssignmentsService
    ) { }

  ngOnInit(): void {
    this.assignment = this._route.snapshot.data.initialData.assignment;
    this._service.setAssignmentDetailsSelected(this.assignment);
    this._stateService.setState(ComponentState.Details);
  }

  public sourceImageCourse(idpicture: number){
    return idpicture ? `${environment.apiBaseUrl}/courseimages/${idpicture}` : 'assets/images/bg/bg1.jpg';
  }

  public detailsCourse(idCourse : number){

  }
}
