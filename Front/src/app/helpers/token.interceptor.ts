import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';
import { AuthenticationService } from '../services/authentication.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(private authenticationService: AuthenticationService) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    if (this.authenticationService.isLoggedIn()) {
      const token = this.authenticationService.getToken();
      const newRequest = this.addTokenToRequest(request, token);

      return next.handle(newRequest).pipe(
        catchError((error) => {
          if (error.status === 401) {
            return this.refreshTokenAndRetry(request, next);
          } else {
            return throwError(() => new Error(error));
          }
        })
      );
    }

    return next.handle(request);
  }

  private addTokenToRequest(
    request: HttpRequest<any>,
    token: string | null
  ): HttpRequest<any> {
    if (token) {
      return request.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`,
        },
      });
    }

    return request;
  }

  private refreshTokenAndRetry(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {

    const refreshToken = this.authenticationService.getRefreshToken();

    if (refreshToken) {
      return this.authenticationService.refreshToken(refreshToken).pipe(
        switchMap((response) => {
          this.authenticationService.refreshToken(response.accessToken);

          const newRequest = this.addTokenToRequest(request, response.accessToken);

          return next.handle(newRequest);
        }),
        catchError((error) => {
          return throwError(() => new Error(error));
        })
      );
    } else {
      this.authenticationService.logout();
      return throwError(() => new Error('No refresh token available'));
    }
  }
}