export interface Assignment {
  id: number;
  label: string;
  state: number;
  stateLabel: string
  delivryDate: Date;
  courseId: number;
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

export interface AssignmentDetails {
  id: number;
  label: string;
  state: number;
  stateLabel: string;
  delivryDate: Date;
  courseId: number;
  courseName: string;
  courseDescription: string;
  coursePictureId: number;
}


export interface AssignmentSearchForm {
  term: string
}

export interface AssignmentSearchFormResults {
  term: string;
  results: Assignment[];
}
