import { Component, inject } from '@angular/core';
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

  newContractForm = new FormGroup({
    work: new FormControl('', Validators.required),
    receiver: new FormControl('', Validators.required),
    file: new FormControl<File | null>(null, Validators.required)
  });

  constructor(private fileUploadService: FileUploadService) {}

  createContract(): void {

    //handle other input field
    const file = this.newContractForm.value.file

    if (file) {
      if(file.type !== "application/pdf"){
        alert("Only PDF allowed")
      }
      /* this.fileUploadService.uploadFile(file).subscribe({
          next: (response) => {
            console.log('File uploaded successfully', response);
          },
          error: (error) => {
            console.error('File upload failed', error);
          },
        });*/
    } else {
      alert('Please select a file first.');
    }
  }

}
