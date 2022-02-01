import { User } from "../users/users.types";

export interface WorkFormCreate {
  name: string;
  description: string;
}

export interface WorkFormSubmitEvaluation {
  id: number;
}

export interface WorkFormUpdateEvaluation {
  id: number;
  grade: number;
  comment: string;
}

export interface WorkFormSubmitWork {
  id: number;
}

export interface WorkFormUpdateWork {
  id: number;
  label: string;
  description: string;
  assignmentId: number;
}

export interface Work {
  id: number;
  label: string;
  grade: number;
  description: string;
  comment: string;
  state: number,
  assignmentId: number;
  userId: User;
}

export interface WorkSearchForm {
  term: string
}

export interface WorkSearchFormResults {
  term: string;
  results: Work[];
}
