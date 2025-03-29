import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BaseHttpService {

  protected http = inject(HttpClient);

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

  getAll<T>(endpoint: string, options?: { params?: { [key: string]: any }, id?: string }): Observable<T[]> {
    let url = `${this.API_BASE_URL}/${endpoint}`;

    if (options?.id) {
      url += `/${encodeURIComponent(options.id)}`;
    }

    return this.http.get<T | T[]>(url, {
      headers: this.header,
      params: this.createParams(options?.params),
    }).pipe(
      map((response: T | T[]) => Array.isArray(response) ? response : [response])
    );
  }

  getById<T>(endpoint: string, id: string) : Observable<T>{
    return this.http.get<T>(`${this.API_BASE_URL}/${endpoint}/${id}`,{
      headers: this.header
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

  deleteDocument<T>(endpoint: string, id: string, fileName: string, documentType: string): Observable<T> {
    let url = `${this.API_BASE_URL}/${endpoint}`;
    let body: any = { fileName };

    if (endpoint === 'api/Document/RemoveDocuments') {
      url += `/${documentType}/${id}`;
    } else {
      url += `/${id}`;
      body.documentType = documentType;
    }

    return this.http.post<T>(url, body, {
      headers: this.header
    });
  }

  delete<T>(endpoint: string, id: string) : Observable<T>{
    return this.http.delete<T>(`${this.API_BASE_URL}/${endpoint}/${id}`, {
      headers: this.header
    });
  }


}
