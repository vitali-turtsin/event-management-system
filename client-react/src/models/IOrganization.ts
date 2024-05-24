import { IBaseEntity } from './IBaseEntity';
import { IPaymentMethod } from './IPaymentMethod';

export interface IOrganization extends IBaseEntity {
  name: string;
  registrationNumber: string;
  numberOfParticipants: number;
  description?: string;
  eventId: string;
  paymentMethodId: string;
  paymentMethod?: IPaymentMethod;
}
