import { Component, Inject, inject, Input} from '@angular/core';
import {
  MAT_DIALOG_DATA,
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
import { sha256 } from 'js-sha256';
import { AuthService } from 'src/app/core/services/auth.service';
import { ProductDocument } from 'src/models/documents/product-document.model';

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
  private authService = inject(AuthService);
  readonly dialogRef = inject(MatDialogRef<FileInputComponent>);

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: { scpReceiverId: string, documentType: string, productInfoId: string },
  ){}

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
      this.uploadEnabled = true
    }else{
      alert("Upload a file!")
    }
  }

  uploadFile(): void {
    const reader = new FileReader();
    reader.onload = () => {
      const base64String = reader.result?.toString().split(',')[1]!;
      const hashedBase64Contract = sha256(base64String!)

      if(this.data.documentType === 'sustainability') {
        let certificate: SupplyChainPartnerCertificate = {
          hash: hashedBase64Contract,
          fileString: base64String,
          supplyChainPartnerReceiverId: this.data.scpReceiverId,
          userEmitterId: this.authService.userId!,
          type: this.data?.documentType!,
        };

        this.certificatesService.uploadSustainabilityCertificate(certificate).subscribe({
          next: () => {
            this.dialogRef.close(true);
          },
          error: (error) => {
            this.dialogRef.close(false);
            console.error('File upload failed', error)
          },
        });
      }else{
        console.log(this.data.productInfoId)
        let certificate: ProductDocument = {
           hash: hashedBase64Contract,
           fileString: base64String,
           userEmitterId: this.authService.userId!,
           type: this.data?.documentType!,
           productInfoId: this.data.productInfoId
        };
        this.certificatesService.uploadQualityCertificate(certificate).subscribe({
          next: () => {
            this.dialogRef.close(true);
          },
          error: (error) => {
            this.dialogRef.close(false);
            console.error('File upload failed', error)
          },
        });
      }
    };

    reader.readAsDataURL(this.fileUploaded);
  }

  reset(){
    this.uploadEnabled = false
  }

}
