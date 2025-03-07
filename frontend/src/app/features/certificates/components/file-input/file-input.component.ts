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
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
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
      ReactiveFormsModule
  ],
  templateUrl: './file-input.component.html',
  styleUrl: './file-input.component.css'
})
export class FileInputComponent {
  readonly dialogRef = inject(MatDialogRef<FileInputComponent>);

  fileForm = new FormGroup({
      file: new FormControl('',Validators.required)
    });

}
