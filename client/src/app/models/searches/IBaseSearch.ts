export interface IBaseSearch {
  pageNumber?: number;
  pageSize?: number;
  isPagingEnabled?: boolean;
  ids?: string[];
  sortField?: string;
  isAscending?: boolean;
}
