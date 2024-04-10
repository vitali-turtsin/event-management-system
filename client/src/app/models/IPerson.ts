import { IBaseEntity } from './IBaseEntity';
import { IPaymentMethod } from './IPaymentMethod';

export interface IPerson extends IBaseEntity {
  firstName: string;
  lastName: string;
  personalCode: string;
  description?: string;
  eventId: string;
  paymentMethodId: string;
  paymentMethod?: IPaymentMethod;
}
