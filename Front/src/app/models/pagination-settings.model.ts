export interface PaginationSettings {
    pageNumber: number;
    pageSize: number;
  }

  export interface PaginatedResult<T> {
    data: T[];
    totalCount: number;
  }