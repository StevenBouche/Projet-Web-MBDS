export interface ApiErrorResponse {
  statuscode: number;
  message: string;
}

export interface PaginationForm {
  pagesize: number;
  page: number;
}

export interface PaginationResult<T> {
  pageSize: number;
  page: number;
  totalPage: number;
  total: number;
  results: T[]
}
