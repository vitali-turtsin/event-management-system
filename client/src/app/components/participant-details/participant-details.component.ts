import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { OrganizationService } from '../../services/organization.service';
import { PaymentMethodService } from '../../services/payment-method.service';
import { PersonService } from '../../services/person.service';
import { Participant } from '../event-details/event-details.component';
import { IPaymentMethod } from '../../models/IPaymentMethod';
import { tap } from 'rxjs';
import { IOrganization } from '../../models/IOrganization';
import { IPerson } from '../../models/IPerson';
import { CommonModule } from '@angular/common';
import { TitleBannerComponent } from '../title-banner/title-banner.component';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-participant-details',
  standalone: true,
  imports: [CommonModule, TitleBannerComponent, FormsModule, RouterModule],
  templateUrl: './participant-details.component.html',
  styleUrl: './participant-details.component.scss',
})
export class ParticipantDetailsComponent implements OnInit {
  participantId?: string;
  participant?: Participant;
  paymentMethods: IPaymentMethod[] = [];

  constructor(
    private router: Router,
    private personService: PersonService,
    private organizationService: OrganizationService,
    private paymentMethodService: PaymentMethodService
  ) {}

  ngOnInit(): void {
    this.participantId =
      this.router.url.split('/').pop()?.replace('?isOrganization=true', '') ||
      '';
    const isOrganization = this.router.url.includes('isOrganization=true');

    this.paymentMethodService.getAll().subscribe((paymentMethods) => {
      this.paymentMethods = paymentMethods;
    });

    if (isOrganization) {
      this.organizationService
        .get(this.participantId)
        .subscribe((organization) => {
          if (organization) {
            this.participant = {
              id: this.participantId,
              isOrganization: true,
              firstName: organization.name,
              lastName: '',
              personalCode: organization.registrationNumber,
              paymentMethodId: organization.paymentMethodId,
              numberOfParticipants: organization.numberOfParticipants,
              description: organization.description || '',
              eventId: organization.eventId,
            };
          }
        });
    } else {
      this.personService.get(this.participantId).subscribe((person) => {
        if (person) {
          this.participant = {
            id: this.participantId,
            isOrganization: false,
            firstName: person.firstName,
            lastName: person.lastName,
            personalCode: person.personalCode,
            paymentMethodId: person.paymentMethodId,
            numberOfParticipants: 1,
            description: person.description || '',
            eventId: person.eventId,
          };
        }
      });
    }
  }

  onSubmit(): void {
    if (!this.participant) return;

    this.participant.isOrganization
      ? this.editOrganization()
      : this.editPerson();
  }

  private editPerson() {
    const person: IPerson = {
      id: this.participantId,
      firstName: this.participant!.firstName,
      lastName: this.participant!.lastName,
      personalCode: this.participant!.personalCode,
      eventId: this.participant!.eventId!,
      paymentMethodId: this.participant!.paymentMethodId,
      description: this.participant!.description,
    };

    this.personService
      .put(person, person.id!)
      .pipe(
        tap((response: any) => {
          if (!response)
            this.router.navigate(['/yritused', this.participant!.eventId]);
        })
      )
      .subscribe();
  }

  private editOrganization() {
    const organization: IOrganization = {
      id: this.participantId,
      name: this.participant!.firstName + ' ' + this.participant!.lastName,
      description: this.participant!.description,
      registrationNumber: this.participant!.personalCode,
      numberOfParticipants: this.participant!.numberOfParticipants,
      eventId: this.participant!.eventId!,
      paymentMethodId: this.participant!.paymentMethodId,
    };

    this.organizationService
      .put(organization, organization.id!)
      .pipe(
        tap((response: any) => {
          if (!response)
            this.router.navigate(['/yritused', this.participant!.eventId]);
        })
      )
      .subscribe();
  }
}
