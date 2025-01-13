import { environment } from '../environments/environment';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ErrorHandlingService } from '../services/error.service';
import { LoginRequest, RegisterRequest, AuthenticationResponse } from '../models/auth-models';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationClient {
  private apiUrl = environment.apiUrl;

  constructor(
    private http: HttpClient,
    private errorHandlingService: ErrorHandlingService
  ) {}

  public login(request: LoginRequest): Observable<AuthenticationResponse> {
    return this.http.post<AuthenticationResponse>(
      `${this.apiUrl}/api/auth/login`,
      request
    ).pipe(
      catchError((error: HttpErrorResponse) => this.errorHandlingService.handleError(error))
    );
  }

  public register(request: RegisterRequest): Observable<AuthenticationResponse> {
    return this.http.post<AuthenticationResponse>(
      `${this.apiUrl}/api/auth/register`,
      request
    ).pipe(
      catchError((error: HttpErrorResponse) => this.errorHandlingService.handleError(error))
    );
  }

  public refreshToken(refreshToken: string): Observable<AuthenticationResponse> {
    return this.http.post<AuthenticationResponse>(
      `${this.apiUrl}/api/auth/refresh`,
      `"${refreshToken}"`,
      { headers: { 'Content-Type': 'text/json' } }
    ).pipe(
      catchError((error: HttpErrorResponse) => this.errorHandlingService.handleError(error))
    );
  }
}