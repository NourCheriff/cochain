export interface PaginationResponse<T> {
  items?: T[];
  totalSize: number
}

export enum DefaultPagination {
  defaultPageSize = 5,
  defaultPageNumber = 0
};
