import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
//import { AuthService } from './auth.service';
import { environment } from 'src/environments/environment';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class BaseHttpService {

  protected http = inject(HttpClient)
  //protected authService = inject(AuthService)

  private readonly API_BASE_URL = environment.baseUrl;
  private readonly header = new HttpHeaders({
      'Content-Type': 'application/json'
  });

  private createParams(params?: { [key: string]: any }): HttpParams {
    let httpParams = new HttpParams();
    if (params) {
      Object.keys(params).forEach((key) => {
        if (params[key] !== undefined && params[key] !== null) {
          httpParams = httpParams.set(key, params[key]);
        }
      });
    }
    return httpParams;
  }

  getAll<T>(endpoint: string, params?: { [key: string]: any } ) : Observable<T[]>{
    return this.http.get<T[]>(`${this.API_BASE_URL}/${endpoint}`,{
      headers: this.header,
      params: this.createParams(params)
    });
  }

  //DA RIMUOVERE
  getByIdWithParams<T>(endpoint: string, params?: { [key: string]: any } ) : Observable<T>{
    return this.http.get<T>(`${this.API_BASE_URL}/${endpoint}`,{
      headers: this.header,
      params: this.createParams(params)
    });
  }

  getById<T>(endpoint: string, id: string) : Observable<T>{
    return this.http.get<T>(`${this.API_BASE_URL}/${endpoint}/${id}`,{
      headers: this.header,
    });
  }

  add<T>(endpoint: string, data: T): Observable<T>{
    return this.http.post<T>(`${this.API_BASE_URL}/${endpoint}`, data, {
      headers: this.header
    });
  }

  update<T>(endpoint: string, newData: T, id: string): Observable<T>{
    return this.http.post<T>(`${this.API_BASE_URL}/${endpoint}/${id}`, newData, {
      headers: this.header
    });
  }

  delete<T>(endpoint: string, id: string): Observable<T>{
    return this.http.post<T>(`${this.API_BASE_URL}/${endpoint}/${id}`,{
      headers: this.header
    });
  }

  

}
