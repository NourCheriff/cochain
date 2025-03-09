import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class FileUploadService {

  private uploadUrl = 'https://cochainapi-app-20250308155408.bravebeach-6563b686.northeurope.azurecontainerapps.io/api/Document/AddCertificationDocument';
  constructor(private http: HttpClient){}

  uploadFile<T>(doc: T): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });

    return this.http.post<any>(this.uploadUrl, doc, { headers });
  }

}
