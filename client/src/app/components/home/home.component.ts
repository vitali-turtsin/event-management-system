import { Component, OnInit } from '@angular/core';
import { EventService } from '../../services/event.service';
import { IEvent } from '../../models/IEvent';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent implements OnInit {
  futureEvents: IEvent[] = [];
  pastEvents: IEvent[] = [];
  homeHtmlText =
    'Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium <b>doloremque laudantium</b>, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo.Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non';

  constructor(private eventService: EventService) {}

  ngOnInit(): void {
    this.loadEvents();
  }

  loadEvents(): void {
    const search = {
      sortField: 'updatedAt',
    };

    this.eventService.getAll(search).subscribe((events) => {
      this.futureEvents = events?.filter(
        (e) => new Date(e.dateTime) > new Date()
      );

      this.pastEvents = events?.filter(
        (e) => new Date(e.dateTime) < new Date()
      );
    });
  }

  removeEvent(id?: string): void {
    if (!id) return;

    this.eventService.delete(id).subscribe(() => {
      this.loadEvents();
    });
  }
}
