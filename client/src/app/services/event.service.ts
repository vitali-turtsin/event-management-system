import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseService } from './base.service';
import { environment } from '../../environments/environment';
import { IEventSearch } from '../models/searches/IEventSearch';
import { IEvent } from '../models/IEvent';
import { ToastrService } from 'ngx-toastr';

const EVENTS_URL = `${environment.apiUrl}/Events`;

@Injectable({
  providedIn: 'root',
})
export class EventService extends BaseService<IEvent, IEventSearch> {
  constructor(http: HttpClient, toastrService: ToastrService) {
    super(EVENTS_URL, http, toastrService);
  }
}
