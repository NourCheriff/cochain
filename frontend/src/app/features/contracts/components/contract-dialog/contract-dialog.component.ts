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
import { FormControl, FormGroup, Validators, ReactiveFormsModule} from '@angular/forms';
import { FileUploadService } from 'src/app/core/services/fileUpload.service';

@Component({
  selector: 'app-contract-dialog',
  imports: [
    MatInputModule,
    MatButtonModule,
    MatDialogTitle,
    MatDialogContent,
    MatSelectModule,
    MatFormFieldModule,
    MatIconModule,
    ReactiveFormsModule
  ],
  templateUrl: './contract-dialog.component.html',
  styleUrl: './contract-dialog.component.css'
})
export class ContractDialogComponent {
  readonly dialogRef = inject(MatDialogRef<ContractDialogComponent>);

  selectedReceiver = null;
  selectedWorkType = null;

  @ViewChild("fileInput") fileInput!: ElementRef
  fileUploaded!: File
  uploadEnabled: boolean = false;

  newContractForm = new FormGroup({
    work: new FormControl('', Validators.required),
    receiver: new FormControl('', Validators.required),
  });

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

  createContract(): void {

    //handle other input field
    const receiver = this.newContractForm.value.receiver
    const workType = this.newContractForm.value.work
    const fileData = new FormData()
    fileData.append('file', this.fileUploaded);

    for (const pair of fileData.entries()) {
      console.log(pair[0], pair[1]);
    }

    /* this.fileUploadService.uploadFile(file).subscribe({
        next: (response) => {
          console.log('File uploaded successfully', response);
        },
        error: (error) => {
          console.error('File upload failed', error);
        },
      });*/

  }

  reset(){
    this.fileInput.nativeElement.value = null
    this.uploadEnabled = false
  }

}
