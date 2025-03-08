import { Component, ElementRef, inject, ViewChild } from '@angular/core';
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
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { FileUploadService } from 'src/app/core/services/fileUpload.service';
import { SupplyChainPartnerCertiticate } from 'src/models/documents/SupplyChainPartnerCertiticate';

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

  readonly dialogRef = inject(MatDialogRef<FileInputComponent>);
  fileUploaded: File | undefined
  uploadEnabled: boolean = false;

  constructor(private fileUploadService: FileUploadService) {}

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
    let doc: SupplyChainPartnerCertiticate = {
      file: this.fileUploaded,
      supplyChainPartnerReceiverId: 'd65e685f-8bdd-470b-a6b8-c9a62e39f095',
      userEmitterId: '3542da56-0de3-4797-a059-effff257f63d',
      type: 'quality'
    }
    this.fileUploadService.uploadFile(doc).subscribe({
      next: (response) => {
          console.log('File uploaded successfully', response);
      },
      error: (error) => {
        console.error('File upload failed', error);
      },
    });
  }

  reset(){
    this.uploadEnabled = false
  }

}
