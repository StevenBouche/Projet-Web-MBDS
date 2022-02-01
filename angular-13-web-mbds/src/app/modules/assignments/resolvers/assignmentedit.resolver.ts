import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { AssignmentsService } from "app/core/assignments/assignments.service";
import { AuthentificationService } from "app/core/authentification/authentification.service";

import { first, forkJoin, map, Observable, take } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AssignmentEditResolver implements Resolve<any>
{
  /**
   * Constructor
   */
  constructor(
      private service: AssignmentsService,
      private _authservice: AuthentificationService
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
      this.service.getAssignmentDetails(id),
      this._authservice.identity.pipe(take(1))
    ]).pipe(
      map(resp => {
        return { assignment: resp[0]}
      }
    ));
  }
}
