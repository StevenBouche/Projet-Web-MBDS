import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { AssignmentsService } from "app/core/assignments/assignments.service";
import { forkJoin, map, Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AssignmentDetailsResolver implements Resolve<any>
{
  /**
   * Constructor
   */
  constructor(
      private service: AssignmentsService
  )
  {
  }

  /**
   * Use this resolver to resolve initial
   *
   * @param route
   * @param state
   */
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any>
  {
    let id = route.params['id'];
    return forkJoin([
      this.service.getAssignmentDetails(id)
    ]).pipe(
      map(resp => {
        return { assignment: resp[0] }
      }
    ));
  }
}
