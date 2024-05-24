import { IBaseEntity } from './IBaseEntity';
import { IOrganization } from './IOrganization';
import { IPerson } from './IPerson';

export interface IEvent extends IBaseEntity {
  name: string;
  dateTime: string;
  location: string;
  description?: string;
  people?: IPerson[];
  organizations?: IOrganization[];
}
