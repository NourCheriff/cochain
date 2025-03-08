import { ElementRef, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { FormGroup } from '@angular/forms';
import { SupplyChainPartnerCertiticate } from 'src/models/documents/SupplyChainPartnerCertiticate';

@Injectable({
  providedIn: 'root',
})
export class FileUploadService {
  private uploadUrl = 'https://cochainapi-app-20250308155408.bravebeach-6563b686.northeurope.azurecontainerapps.io/api/Document/AddCertificationDocument'; // Sostituisci con l'URL del tuo endpoint backend

  constructor(private http: HttpClient) {}

  uploadFile(doc: SupplyChainPartnerCertiticate): Observable<any> {
    return this.http.post<any>(this.uploadUrl, doc);
  }
}
