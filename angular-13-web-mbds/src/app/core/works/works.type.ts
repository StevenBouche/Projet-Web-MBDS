import { Assignment } from "../assignments/assignments.type";
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
  stateLabel: string,
  assignment: Assignment;
  User: User;
}

export interface WorkSearchForm {
  term: string
}

export interface WorkSearchFormResults {
  term: string;
  results: Work[];
}

export interface WorkPaginationForm {
  pagesize: number;
  page: number;
  assignmentId: number;
  state: number;
}

export interface WorkPaginationResult {
  pageSize: number;
  page: number;
  totalPage: number;
  total: number;
  assignmentId: number;
  state: number;
  results: Array<Work>
}
