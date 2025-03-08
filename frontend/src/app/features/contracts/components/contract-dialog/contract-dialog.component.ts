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
    file: new FormControl('',Validators.required)
  });

}
