import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseService } from './base.service';
import { environment } from '../../environments/environment';
import { IPaymentMethodSearch } from '../models/searches/IPaymentMethodSearch';
import { IPaymentMethod } from '../models/IPaymentMethod';
import { ToastrService } from 'ngx-toastr';

const EVENTS_URL = `${environment.apiUrl}/PaymentMethods`;

@Injectable({
  providedIn: 'root',
})
export class PaymentMethodService extends BaseService<
  IPaymentMethod,
  IPaymentMethodSearch
> {
  constructor(http: HttpClient, toastrService: ToastrService) {
    super(EVENTS_URL, http, toastrService);
  }
}
