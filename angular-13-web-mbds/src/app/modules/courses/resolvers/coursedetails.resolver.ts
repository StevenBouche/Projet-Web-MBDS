import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { CoursesService } from "app/core/courses/courses.service";
import { Course } from "app/core/courses/courses.type";
import { forkJoin, map, Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class CourseDetailsResolver implements Resolve<Course>
{
  /**
   * Constructor
   */
  constructor(
      private service: CoursesService
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
      this.service.getById(id),
      this.service.getAssignmentsOfCourse(id)
    ]).pipe(
      map(resp => {
        return { course: resp[0], assignments: resp[1] }
      }
    ));
  }
}
