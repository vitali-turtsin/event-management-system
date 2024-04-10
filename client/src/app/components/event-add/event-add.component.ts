import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { IEvent } from '../../models/IEvent';
import { EventService } from '../../services/event.service';
import { Router, RouterModule } from '@angular/router';
import { TitleBannerComponent } from '../title-banner/title-banner.component';
import { CustomDatetimePipe } from '../../pipes/custom-datetime.pipe';
import { tap } from 'rxjs';

@Component({
  selector: 'app-add-event',
  standalone: true,
  imports: [
    FormsModule,
    RouterModule,
    TitleBannerComponent,
    CustomDatetimePipe,
  ],
  templateUrl: './event-add.component.html',
  styleUrl: './event-add.component.scss',
})
export class EventAddComponent {
  event: IEvent = {
    name: '',
    dateTime: new Date().toUTCString(),
    location: '',
    description: '',
  };

  constructor(private eventService: EventService, private router: Router) {}

  onSubmit(): void {
    if (this.event.dateTime) {
      this.event.dateTime = new Date(this.event.dateTime).toISOString();
    }

    this.eventService
      .post(this.event)
      .pipe(
        tap((response: any) => {
          if (response.id) this.router.navigate(['/']);
        })
      )
      .subscribe();
  }
}
