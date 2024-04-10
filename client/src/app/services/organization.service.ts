import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseService } from './base.service';
import { environment } from '../../environments/environment';
import { IOrganizationSearch } from '../models/searches/IOrganizationSearch';
import { IOrganization } from '../models/IOrganization';
import { ToastrService } from 'ngx-toastr';

const EVENTS_URL = `${environment.apiUrl}/Organizations`;

@Injectable({
  providedIn: 'root',
})
export class OrganizationService extends BaseService<
  IOrganization,
  IOrganizationSearch
> {
  constructor(http: HttpClient, toastrService: ToastrService) {
    super(EVENTS_URL, http, toastrService);
  }
}
