import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AuthRequest } from 'src/models/auth/auth-request.model';
import { AuthResponse } from 'src/models/auth/auth-response.model';
import { BaseResponse } from 'src/models/auth/base-response.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  // add jwt checks before getting it from getStoredToken() to ensure validity

  private readonly TOKEN_KEY = 'token';
  private readonly API_BASE_URL = `${environment.baseUrl}${environment.apiEndpoint}`;
  private _tokenSource: BehaviorSubject<string | null>;
  public readonly token$: Observable<string | null>;

  constructor(private http: HttpClient) {
    this._tokenSource = new BehaviorSubject<string | null>(this.getStoredToken());
    this.token$ = this._tokenSource.asObservable();
  }

  public get token(): string | null {
    return this._tokenSource.value;
  }

  public requestOtp(email: string): void {
    let body: AuthRequest = { username: email, password: 'System' };
    this.http.post<boolean>(`${this.API_BASE_URL}/Users/RequestPassword`, body).subscribe();
  }

  public login(email: string, otp: string): void {
    let body: AuthRequest = { username: email, password: otp };

    this.http.post<BaseResponse<AuthResponse>>(`${this.API_BASE_URL}/Users/Login`, body).subscribe({
      next: (response) => this.onResponse(response),
    });
  }

  public logout(): void {
    this.clearToken();
  }

  public onResponse(response: BaseResponse<AuthResponse>): void {
    if (!response.data) {
      this.clearToken();
      return;
    }

    this.saveToken(response.data.token)
  }

  public getStoredToken(): string | null {
    return sessionStorage.getItem(this.TOKEN_KEY);
  }

  private saveToken(token: string): void {
    sessionStorage.setItem(this.TOKEN_KEY, token);
    this._tokenSource.next(token);
  }

  private clearToken(): void {
    sessionStorage.removeItem(this.TOKEN_KEY);
    this._tokenSource.next(null);
  }

}
