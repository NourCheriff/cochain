
import { Component, inject } from '@angular/core';
import { MatDialogContent, MatDialogRef } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ReactiveFormsModule, FormControl, Validators, FormGroup, FormsModule } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { User } from 'src/models/auth/user.model';
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

  readonly dialogRef = inject(MatDialogRef<UserDialogComponent>);

  constructor(private userService: UserService) {}

  newUser!: User;
  selected = "user";

  userForm  = new FormGroup<MyForm>({
    firstName: new FormControl("", [Validators.required],),
    lastName: new FormControl("", [Validators.required],),
    userName: new FormControl("", [Validators.required, Validators.email],),
    phone: new FormControl("", [Validators.required],),
  });

  insertUser(){
    this.newUser.firstName = this.userForm.get('firstName')?.value!;
    this.newUser.lastName = this.userForm.get('lastName')?.value!;
    this.newUser.userName = this.userForm.get('userName')?.value!;
    this.newUser.phone = this.userForm.get('phone')?.value!;

    this.userService.addUser(this.newUser).subscribe({
      next: (response) => {
        console.log(response)
      },
      error: (error) => console.log(error)
    })
  }
}

interface MyForm {
  firstName: FormControl<string | null>;
  lastName: FormControl<string | null>;
  userName: FormControl<string | null>;
  phone: FormControl<string | null>;
}
