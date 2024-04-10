import { IBaseSearch } from './IBaseSearch';

export interface IOrganizationSearch extends IBaseSearch {
  name?: string;
  registrationNumber?: string;
}
