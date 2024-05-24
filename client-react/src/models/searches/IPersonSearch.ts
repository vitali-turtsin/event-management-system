import { IBaseSearch } from './IBaseSearch';

export interface IPersonSearch extends IBaseSearch {
  name?: string;
  personalCode?: string;
}
