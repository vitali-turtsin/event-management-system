export interface IParticipant {
  id?: string;
  isOrganization: boolean;
  firstName: string;
  lastName: string;
  personalCode: string;
  numberOfParticipants: number;
  description: string;
  paymentMethodId: string;
  eventId?: string;
}
