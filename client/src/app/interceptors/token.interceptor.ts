import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor(
    private router: Router,
  ) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const authRequest = request.clone({
        headers: request.headers.append('Authorization', `Bearer ${sessionStorage.getItem('token')}`)
    });
    return next.handle(authRequest).pipe(
      tap(
        event => { },
        error => {
          if (error instanceof HttpErrorResponse) {
            if (error.status === 401) {
              sessionStorage.removeItem('token');
              this.router.navigate(['/authorization']);
            }
          }
        }
      )
    );
  }
}
