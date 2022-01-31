import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { CoursesService } from "app/core/courses/courses.service";
import { Course } from "app/core/courses/courses.type";
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class CourseResolver implements Resolve<Course>
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
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Course>
  {
    console.log('resolve')
    return this.service.getById(route.params['id']);
  }
}
