import { IBaseEntity } from "./IBaseEntity";

export interface IListParticipant extends IBaseEntity {
  name: string;
  code: string;
  isOrganization: boolean;
}
