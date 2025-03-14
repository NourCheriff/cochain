export interface BaseResponse<T> {
  status: RequestExecution;
  data: T;
  responseMessage: string;
  totalCount: number;
  errors: string[];
}

export enum RequestExecution {
  successful = 1,
  failed,
  error
}