import { Component, inject} from '@angular/core';
import {
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle,
} from '@angular/material/dialog';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {MatSelectModule} from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatIconModule} from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { SupplyChainPartnerCertificate } from 'src/models/documents/supply-chain-partner-certificate.model';
import { CertificatesService } from '../../service/certificates.service';

@Component({
  selector: 'app-file-input',
  imports: [
      MatInputModule,
      MatButtonModule,
      MatDialogTitle,
      MatDialogContent,
      MatSelectModule,
      MatFormFieldModule,
      MatIconModule,
      CommonModule,
      ReactiveFormsModule,
  ],
  templateUrl: './file-input.component.html',
  styleUrl: './file-input.component.css'
})

export class FileInputComponent {

  private certificatesService = inject(CertificatesService);

  readonly dialogRef = inject(MatDialogRef<FileInputComponent>);

  fileUploaded!: File;
  uploadEnabled: boolean = false;

  onSelectFile(event : Event){
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.fileUploaded = input.files[0]
      if(this.fileUploaded.type !== "application/pdf"){
        alert("Only PDF allowed")
        return
      }
      console.log(this.fileUploaded)
      this.uploadEnabled = true
    }else{
      alert("Upload a file!")
    }
  }

  uploadFile(): void {

    const reader = new FileReader();
    reader.onload = () => {
      const base64String = reader.result?.toString().split(',')[1]; // Rimuove il prefisso 'data:...;base64,'

      let certificate: SupplyChainPartnerCertificate = {
        hash: base64String,
        supplyChainPartnerReceiverId: 'd65e685f-8bdd-470b-a6b8-c9a62e39f095',
        userEmitterId: '3542da56-0de3-4797-a059-effff257f63d',
        type: 'quality',
      };

      this.certificatesService.uploadCertificate(certificate).subscribe({
        next: (response) => console.log('File uploaded successfully', response),
        error: (error) => console.error('File upload failed', error),
      });
    };

    reader.readAsDataURL(this.fileUploaded);
  }

  reset(){
    this.uploadEnabled = false
  }

}
