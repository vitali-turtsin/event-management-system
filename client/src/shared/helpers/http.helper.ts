import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';

export function handleHttpError(toastrService: ToastrService) {
  return (errorResponse: any) => {
    if (!errorResponse.error.errors) {
      toastrService.error(errorResponse.error.message);
    }

    const errors: string[] = Object.keys(errorResponse.error.errors).map(
      (errorKey: string) =>
        (errorResponse.error.errors[errorKey] as string[])[0]
    );

    errors.forEach((error) => toastrService.error(error));

    return new Observable<any>((subscriber) => {
      subscriber.next(errorResponse);
      subscriber.complete();
    });
  };
}
