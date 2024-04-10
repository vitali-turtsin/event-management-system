import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseService } from './base.service';
import { environment } from '../../environments/environment';
import { IPersonSearch } from '../models/searches/IPersonSearch';
import { IPerson } from '../models/IPerson';
import { ToastrService } from 'ngx-toastr';

const EVENTS_URL = `${environment.apiUrl}/People`;

@Injectable({
  providedIn: 'root',
})
export class PersonService extends BaseService<IPerson, IPersonSearch> {
  constructor(http: HttpClient, toastrService: ToastrService) {
    super(EVENTS_URL, http, toastrService);
  }
}
