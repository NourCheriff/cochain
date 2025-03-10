
import { Component, inject } from '@angular/core';
import { MatDialogContent, MatDialogRef } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ReactiveFormsModule, FormControl, Validators, FormGroup, FormsModule } from '@angular/forms';
@Component({
  selector: 'app-user-dialog',
  imports: [
    MatInputModule,
    MatButtonModule,
    MatDialogContent,
    MatSelectModule,
    MatFormFieldModule,
    FormsModule,
    ReactiveFormsModule
  ],
  templateUrl: './user-dialog.component.html',
  styleUrl: './user-dialog.component.css'
})
export class UserDialogComponent {

  selected = "user";

  readonly dialogRef = inject(MatDialogRef<UserDialogComponent>);
  userForm  = new FormGroup<MyForm>({
    firstNameUser: new FormControl("", [Validators.required],),
    lastNameUser:  new FormControl("", [Validators.required],),
    emailUser:     new FormControl("", [Validators.required, Validators.email],),
    phoneUser:     new FormControl("", [Validators.required],),
  });

  insertUser(){
    //where to insert the query for adding a company and the SCP/CA admin
  }
}

interface MyForm {
  firstNameUser: FormControl<string | null>;
  lastNameUser:  FormControl<string | null>;
  emailUser:     FormControl<string | null>;
  phoneUser:     FormControl<string | null>;
}
