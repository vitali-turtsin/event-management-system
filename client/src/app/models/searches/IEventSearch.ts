import { IBaseSearch } from './IBaseSearch';

export interface IEventSearch extends IBaseSearch {
  name?: string;
  startDate?: Date;
  location?: string;
}
