import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { CoursesService } from "app/core/courses/courses.service";
import { Course, CourseTreeNode } from "app/core/courses/courses.type";
import { forkJoin, map, Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class StudentWorkResolver implements Resolve<CourseTreeNode[]>
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
    return this.service.getCourseTreeNode();
  }
}
