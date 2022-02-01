import { Course } from "../courses/courses.type";

export interface Assignment {
  id: number;
  label: string;
  state: number;
  stateLabel: string
  delivryDate: Date;
  course: Course;
}

export interface AssignmentFormCreate {
  label: string;
  state: number;
  delivryDate: Date;
  courseId: number;
}

export interface AssignmentFormUpdate {
  id: number;
  label: string;
  state: number;
  delivryDate: Date;
  courseId: number;
}
