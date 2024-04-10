import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { EventAddComponent } from './components/event-add/event-add.component';
import { ParticipantDetailsComponent } from './components/participant-details/participant-details.component';
import { AddParticipantComponent } from './components/event-details/event-details.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'lisa-yritus', component: EventAddComponent },
  { path: 'yritused/:id', component: AddParticipantComponent },
  { path: 'osalejad/:id', component: ParticipantDetailsComponent },
];
