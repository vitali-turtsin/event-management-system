import { Component, OnInit } from '@angular/core';
import { PersonService } from '../../services/person.service';
import { OrganizationService } from '../../services/organization.service';
import { IOrganization } from '../../models/IOrganization';
import { IEvent } from '../../models/IEvent';
import { EventService } from '../../services/event.service';
import { Router, RouterModule } from '@angular/router';
import { IPerson } from '../../models/IPerson';
import { TitleBannerComponent } from '../title-banner/title-banner.component';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { PaymentMethodService } from '../../services/payment-method.service';
import { IPaymentMethod } from '../../models/IPaymentMethod';
import { tap } from 'rxjs';

export type Participant = {
  id?: string;
  isOrganization: boolean;
  firstName: string;
  lastName: string;
  personalCode: string;
  numberOfParticipants: number;
  description: string;
  paymentMethodId: string;
  eventId?: string;
};

type ListParticipant = {
  id: string;
  name: string;
  code: string;
  updatedAt: Date;
  isOrganization: boolean;
};

@Component({
  selector: 'app-event-details',
  standalone: true,
  imports: [CommonModule, TitleBannerComponent, FormsModule, RouterModule],
  templateUrl: './event-details.component.html',
  styleUrl: './event-details.component.scss',
})
export class AddParticipantComponent implements OnInit {
  eventId?: string;
  event?: IEvent;
  eventParticipants: ListParticipant[] = [];

  paymentMethods: IPaymentMethod[] = [];

  newParticipant: Participant = {
    isOrganization: false,
    firstName: '',
    lastName: '',
    personalCode: '',
    paymentMethodId: '',
    numberOfParticipants: 0,
    description: '',
  };

  constructor(
    private router: Router,
    private eventService: EventService,
    private personService: PersonService,
    private organizationService: OrganizationService,
    private paymentMethodService: PaymentMethodService
  ) {}

  ngOnInit(): void {
    this.eventId = this.router.url.split('/')[2];

    this.eventService.get(this.eventId).subscribe((event) => {
      this.event = event;

      this.eventParticipants = [
        ...(event.people?.map((person) => ({
          id: person.id!,
          name: person.firstName + ' ' + person.lastName,
          code: person.personalCode,
          updatedAt: person.updatedAt!,
          isOrganization: false,
        })) || []),
        ...(event.organizations?.map((organization) => ({
          id: organization.id!,
          name: organization.name,
          code: organization.registrationNumber,
          updatedAt: organization.updatedAt!,
          isOrganization: true,
        })) || []),
      ];
    });

    this.paymentMethodService.getAll().subscribe((paymentMethods) => {
      this.paymentMethods = paymentMethods;

      if (paymentMethods[0].id)
        this.newParticipant.paymentMethodId = paymentMethods[0].id;
    });
  }

  onSubmit(): void {
    if (!this.eventId) return;

    this.newParticipant.isOrganization
      ? this.createOrganization()
      : this.createPerson();
  }

  removeParticipant(id?: string): void {
    if (!id) return;

    if (this.eventParticipants.find((p) => p.id === id)?.isOrganization) {
      this.organizationService.delete(id).subscribe(() => {
        this.eventParticipants = this.eventParticipants.filter(
          (p) => p.id !== id
        );
      });

      return;
    }

    this.personService.delete(id).subscribe(() => {
      this.eventParticipants = this.eventParticipants.filter(
        (p) => p.id !== id
      );
    });
  }

  switchParticipantType(): void {
    this.newParticipant.isOrganization = !this.newParticipant.isOrganization;
  }

  private createPerson() {
    const person: IPerson = {
      firstName: this.newParticipant.firstName,
      lastName: this.newParticipant.lastName,
      personalCode: this.newParticipant.personalCode,
      eventId: this.eventId!,
      paymentMethodId: this.newParticipant.paymentMethodId,
      description: this.newParticipant.description,
    };

    this.personService
      .post(person)
      .pipe(
        tap((response: any) => {
          if (response.id) {
            this.eventParticipants.push({
              id: response.id,
              name: person.firstName + ' ' + person.lastName,
              code: person.personalCode,
              updatedAt: person.updatedAt || new Date(),
              isOrganization: false,
            });
          }
        })
      )
      .subscribe();
  }

  private createOrganization() {
    const organization: IOrganization = {
      name: this.newParticipant.firstName + ' ' + this.newParticipant.lastName,
      description: this.newParticipant.description,
      registrationNumber: this.newParticipant.personalCode,
      numberOfParticipants: this.newParticipant.numberOfParticipants,
      eventId: this.eventId!,
      paymentMethodId: this.newParticipant.paymentMethodId,
    };

    this.organizationService
      .post(organization)
      .pipe(
        tap((response: any) => {
          if (response.id) {
            this.eventParticipants.push({
              id: response.id,
              name: organization.name,
              code: organization.registrationNumber,
              updatedAt: organization.updatedAt || new Date(),
              isOrganization: true,
            });
          }
        })
      )
      .subscribe();
  }
}
