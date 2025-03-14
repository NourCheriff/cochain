import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AuthRequest } from 'src/models/auth/auth-request.model';
import { AuthResponse } from 'src/models/auth/auth-response.model';
import { BaseResponse, RequestExecution } from 'src/models/auth/base-response.model';
import { jwtDecode } from 'jwt-decode';
import { Jwt } from 'src/models/auth/jwt-payload.model';
import { Router } from '@angular/router';
import { Role } from 'src/types/roles.enum';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private readonly TOKEN_KEY = 'token';
  private readonly API_BASE_URL = `${environment.baseUrl}${environment.apiEndpoint}`;
  private _tokenSource: BehaviorSubject<string | null>;
  public readonly token$: Observable<string | null>;

  constructor(private http: HttpClient, private router: Router) {
    this._tokenSource = new BehaviorSubject<string | null>(this.getStoredToken());
    this.token$ = this._tokenSource.asObservable();
  }

  public get token(): string | null {
    return this._tokenSource.value;
  }

  public requestOtp(email: string): Observable<boolean> {
    let body: AuthRequest = { username: email, password: 'System' };
    return this.http.post<boolean>(`${this.API_BASE_URL}/Users/RequestPassword`, body);
  }

  public login(email: string, otp: string): Observable<boolean> {
    let body: AuthRequest = { username: email, password: otp };

    return this.http.post<BaseResponse<AuthResponse>>(`${this.API_BASE_URL}/Users/Login`, body).pipe(
      tap((response) => this.onResponse(response)),
      map((response) => response.status === RequestExecution.successful),
    );
  }

  public logout(): void {
    this.clearToken();
    this.router.navigateByUrl('/login');
  }

  public isAdmin(): boolean {
    if (!this.token)
      return false;

    try {
      let decodedJwt: Jwt = jwtDecode<Jwt>(this.token);
      return decodedJwt['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] === Role.SysAdmin;
    } catch {
      return false;
    }
  }

  public onResponse(response: BaseResponse<AuthResponse>): void {
    if (response.status !== RequestExecution.successful || !response.data) {
      this.clearToken();
      return;
    }

    this.saveToken(response.data.token);
  }

  public getStoredToken(): string | null {
    let token = sessionStorage.getItem(this.TOKEN_KEY);
    if(!this.isTokenValid(token))
      return null;

    return token;
  }

  private saveToken(token: string): void {
    sessionStorage.setItem(this.TOKEN_KEY, token);
    this._tokenSource.next(token);
  }

  private clearToken(): void {
    sessionStorage.removeItem(this.TOKEN_KEY);
    this._tokenSource.next(null);
  }

  private isTokenValid(token: string | null): boolean {
    if (!token)
      return false;

    try {
      let decodedJwt: Jwt = jwtDecode<Jwt>(token);
      return Date.now() < decodedJwt.exp * 1000;
    } catch {
      return false;
    }
  }

}
