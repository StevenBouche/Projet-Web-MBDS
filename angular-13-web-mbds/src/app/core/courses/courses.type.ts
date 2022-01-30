import User from "../users/users.types";

export interface CourseFormCreate {
  name: string;
  description: string;
}

export interface CourseFormUpdate {
  id: number;
  name: string;
  description: string;
}

export interface Course {
  id: number;
  name: string;
  description: string;
  pictureId: number;
  user: User;
}

export interface CourseSearchForm {
  term: string
}

export interface CourseSearchFormResults {
  term: string;
  results: Course[];
}

