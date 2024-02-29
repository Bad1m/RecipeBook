import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationClient } from '../clients/authentication.client';
import { Observable } from 'rxjs';
import { LoginRequest, RegisterRequest, AuthenticationResponse } from '../models/auth-models';
import { tap } from 'rxjs';
import { throwError } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  private tokenKey = 'token';
  private refreshTokenKey = 'refreshToken';

  constructor(
    private authenticationClient: AuthenticationClient,
    private router: Router
  ) {}

  public login(request: LoginRequest): Observable<AuthenticationResponse> {
    return this.authenticationClient.login(request).pipe(
      tap((response) => {
        const token = response.accessToken;
        const refreshToken = response.refreshToken;
        const userName = request.username;
        localStorage.setItem(this.tokenKey, token);
        localStorage.setItem(this.refreshTokenKey, refreshToken);
        localStorage.setItem('userName', userName);
        this.router.navigate(['/recipes']);
      })
    );
  }

  public refreshToken(refreshToken: string): Observable<AuthenticationResponse> {
    if (refreshToken) {
      return this.authenticationClient.refreshToken(refreshToken).pipe(
        tap((response) => {
          localStorage.setItem(this.tokenKey, response.accessToken);
        })
      );
    } else {
      return throwError(() => new Error("Refresh token is null"));
    }
  }

  public getRefreshToken(): string | null {
    return localStorage.getItem(this.refreshTokenKey);
  }

  public register(request: RegisterRequest): Observable<AuthenticationResponse> {
    return this.authenticationClient.register(request).pipe(
      tap((response) => {
        this.router.navigate(['/login']);
      })
    );
  }
  public logout() {
    localStorage.removeItem(this.tokenKey);
    this.router.navigate(['/login']);
  }

  public isLoggedIn(): boolean {
    if (typeof localStorage !== 'undefined') {
      let token = localStorage.getItem(this.tokenKey);
      return token != null && token.length > 0;
    } else {
      return false;
    }
  }

  public getToken(): string | null {
    if (typeof localStorage !== 'undefined' && this.isLoggedIn()) {
      return localStorage.getItem(this.tokenKey);
    } else {
      return null; 
    }
  }
}