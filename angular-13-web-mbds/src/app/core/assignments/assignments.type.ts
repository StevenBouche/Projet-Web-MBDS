import { Course } from "../courses/courses.type";

export interface Assignment {
  id: number;
  label: string;
  state: number;
  stateLabel: string
  delivryDate: Date;
  deliveryDateLabel: string;
  course: Course;
  haveWork: boolean;
  createAt: Date;
  updateAt: Date;
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

export interface AssignmentSearchForm {
  courseId: number | null;
  term: string
}

export interface AssignmentSearchFormResults {
  courseId: number | null;
  term: string;
  results: Assignment[];
}

