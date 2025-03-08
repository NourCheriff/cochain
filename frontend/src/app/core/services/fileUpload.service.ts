import { ElementRef, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { FormGroup } from '@angular/forms';
import { SupplyChainPartnerDocument } from 'src/models/documents/SupplyChainPartnerCertiticate';

@Injectable({
  providedIn: 'root',
})
export class FileUploadService {
  private uploadUrl = 'https://cochainapi-app-20250308155408.bravebeach-6563b686.northeurope.azurecontainerapps.io/api/Document/AddCertificationDocument'; // Sostituisci con l'URL del tuo endpoint backend

  constructor(private http: HttpClient) {}

  onFileSelected(inputFile: HTMLInputElement, fileRef: ElementRef): void {
    if (inputFile.files && inputFile.files.length) {
      console.log(inputFile.files);
      const selectedFile = inputFile.files[0];
      console.log(selectedFile.type)
      if (selectedFile.type !== 'application/pdf') {
        alert('Only PDF files are allowed.');
        fileRef.nativeElement.value = null;
        return;
      }
    }
  }

  uploadFile(doc: SupplyChainPartnerDocument): Observable<any> {
   //console.log(file.name)
    return this.http.post<any>(this.uploadUrl, doc);
  }

  resetFile(fileRef: ElementRef): void{
    fileRef.nativeElement.value = null;
  }
}
