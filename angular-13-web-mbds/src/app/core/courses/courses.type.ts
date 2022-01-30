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
  userId: number;
}

export interface CourseSearchForm {
  term: string
}

export interface CourseSearchFormResults {
  term: string;
  results: Course[];
}

