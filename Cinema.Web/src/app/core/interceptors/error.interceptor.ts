import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LocalStoreService } from '../services/local-store.service';


@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(public router: Router, private toastr: ToastrService, private localStoreService: LocalStoreService) {
    }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        this.handleHttpErrorEvent(error);
        return throwError(() => error);
      })
    );
  }

  private handleHttpErrorEvent(error: HttpErrorResponse): void {
    switch (error.status) {
      case 401:
        this.toastr.error('Dostęp nieautoryzowany');
        this.localStoreService.removeToken();
        this.router.navigate(['identity']);
        break;
      case 403:
        this.toastr.warning('Brak uprawnień do wykonania rządanej operacji');
        break;
      case 400:
      case 409:
        if (error.error && error.error.message) {
          let msg = error.error.message;

          if (!msg && error.error.length > 0) {
            msg = error.error[0].message;
          }
          if (msg) {
            this.toastr.warning(msg);
          }
        }
        break;
      case 404:
        this.toastr.error('Nie znaleziono');
        break;
      default:
        this.toastr.error('Nieoczekiwany błąd');
        break;
    }
  }
}
