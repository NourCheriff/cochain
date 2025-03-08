import { Component, inject, ChangeDetectionStrategy, ViewChild, ElementRef} from '@angular/core';
import {
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle,
} from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatInputModule} from '@angular/material/input';
import {MatSelectModule} from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';
import {provideNativeDateAdapter} from '@angular/material/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators,AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';
import { FileUploadService } from 'src/app/core/services/fileUpload.service';

@Component({
  selector: 'app-new-work-dialog',
    imports: [
      MatButtonModule,
      MatIconModule,
      MatSelectModule,
      MatDialogTitle,
      MatDialogContent,
      MatFormFieldModule,
      MatInputModule,
      MatDatepickerModule,
      ReactiveFormsModule,
      CommonModule
    ],
  templateUrl: './new-work-dialog.component.html',
  providers: [provideNativeDateAdapter()],
  styleUrl: './new-work-dialog.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class NewWorkDialogComponent {

  readonly dialogRef = inject(MatDialogRef<NewWorkDialogComponent>);

  @ViewChild('fileInput') fileInput!: ElementRef;
  selectedReceiver: string = '';
  selectedWorkType: string = '';
  isReceiverVisible: boolean = false;

  newWorkForm = new FormGroup({
    work: new FormControl('', Validators.required),
    receiver: new FormControl('', this.receiverValidator()),
    workDate: new FormControl(Date.now(),[Validators.required]),
    file: new FormControl<File | null>(null, Validators.required)
  });

  constructor(private fileUploadService: FileUploadService) {}

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    this.fileUploadService.onFileSelected(input,this.fileInput)
  }

  createWork(): void {

    //handle other input field
    //handle multi file
    const file = this.newWorkForm.get('file')?.value;
    if (file) {
      // this.fileUploadService.uploadFile(file).subscribe({
      //   next: (response) => {
      //     console.log('File uploaded successfully', response);
      //   },
      //   error: (error) => {
      //     console.error('File upload failed', error);
      //   },
      // });
    } else {
      alert('Please select a file first.');

    }
  }

  resetFile(): void {
   this.fileUploadService.resetFile(this.fileInput);
  }

  onSelectionChange(value: string) {
    this.selectedWorkType = value;
    this.isReceiverVisible = value === 'transport';
  }

  private receiverValidator(): ValidatorFn {
    return (control:AbstractControl) : ValidationErrors | null => {

      const value = control.value;
      const workValue = this.newWorkForm?.value?.work;

      // If the work value is "transport", receiver must be provided
      if (workValue === "transport" && (!value || value === '')) {
        return { receiverRequired: true };
      }

      return null;
    }
  }
}

