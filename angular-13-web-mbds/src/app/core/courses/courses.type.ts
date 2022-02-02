import { User } from "../users/users.types";

export interface CourseFormCreate {
  name: string;
  description: string;
}

export interface CourseFormUpdate {
  id: number;
  name: string;
  description: string;
}

export interface CourseStats {
  totalworks: number;
  totalassignments: number;
}


export interface Course {
  id: number;
  name: string;
  description: string;
  pictureId: number;
  user: User;
  stats: CourseStats;
}

export interface CourseSearchForm {
  term: string
}

export interface CourseSearchFormResults {
  term: string;
  results: Course[];
}

export interface CoursePaginationForm {
  userId: number | null;
  pagesize: number;
  page: number;
  courseName: string;
  username: string;
}

export interface CoursePaginationResult {
  pageSize: number;
  page: number;
  totalPage: number;
  total: number;
  courseName: string;
  username: string;
  results: Array<Course>
}
