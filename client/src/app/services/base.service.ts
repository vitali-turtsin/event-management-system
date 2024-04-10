import { HttpClient } from '@angular/common/http';
import { Observable, catchError } from 'rxjs';
import { toQueryString } from '../../shared/helpers/http-request-util';
import { handleHttpError } from '../../shared/helpers/http.helper';
import { ToastrService } from 'ngx-toastr';

export class BaseService<TEntity, TSearch> {
  constructor(
    protected apiUrl: string,
    protected http: HttpClient,
    protected toastrService: ToastrService
  ) {}

  getAll(search?: TSearch): Observable<TEntity[]> {
    return this.http
      .get<TEntity[]>(`${this.apiUrl}?${toQueryString(search)}`)
      .pipe(catchError(handleHttpError(this.toastrService)));
  }

  get(id: string): Observable<TEntity> {
    return this.http
      .get<TEntity>(`${this.apiUrl}/${id}`)
      .pipe(catchError(handleHttpError(this.toastrService)));
  }

  post(entity: TEntity): Observable<TEntity> {
    return this.http
      .post<TEntity>(this.apiUrl, entity)
      .pipe(catchError(handleHttpError(this.toastrService)));
  }

  put(entity: TEntity, id: string): Observable<TEntity> {
    return this.http
      .put<TEntity>(`${this.apiUrl}/${id}`, entity)
      .pipe(catchError(handleHttpError(this.toastrService)));
  }

  delete(id: string): Observable<TEntity> {
    return this.http
      .delete<TEntity>(`${this.apiUrl}/${id}`)
      .pipe(catchError(handleHttpError(this.toastrService)));
  }
}
