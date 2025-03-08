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
import { SupplyChainPartnerDocument } from 'src/models/documents/SupplyChainPartnerCertiticate';

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

  constructor(private fileUploadService: FileUploadService) {}

  @ViewChild('fileInput') fileInput!: ElementRef;

  fileForm = new FormGroup({
      file: new FormControl<File | null>(null, Validators.required)
  });

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    //this.fileUploadService.onFileSelected(input,this.fileInput)
    this.uploadFile(input!.files![0]!)
  }

  uploadFile(fileUpload:File): void {
   // const file = this.fileForm.get('file')?.value;
    console.log(fileUpload);
    if (fileUpload) {
      let doc: SupplyChainPartnerDocument = {
        file: fileUpload,
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
    } else {
        alert('Please select a file first.');

    }
  }

  resetFile(): void {
   this.fileUploadService.resetFile(this.fileInput)
  }

}
