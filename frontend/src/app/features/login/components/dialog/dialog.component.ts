import { Component, EventEmitter, inject, Output } from '@angular/core';
import {
  MAT_DIALOG_DATA,
  MatDialogActions,
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle,
} from '@angular/material/dialog';
import { FormControl, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import {MatSnackBar, MatSnackBarModule} from '@angular/material/snack-bar';

interface DialogData {
  email: string;
}

@Component({
  selector: 'app-login-dialog',
  imports: [
    MatInputModule,
    MatButtonModule,
    MatDialogTitle,
    MatDialogContent,
    MatDialogActions,
    ReactiveFormsModule,
    MatSnackBarModule,
  ],
  templateUrl: './dialog.component.html',
  styleUrl: './dialog.component.css'
})
export class LoginDialogComponent {
  @Output() resendOtpEvent = new EventEmitter();
  isLoginRequestSent = false;

  private _snackBar = inject(MatSnackBar);
  readonly dialogRef = inject(MatDialogRef<LoginDialogComponent>);
  readonly data = inject<DialogData>(MAT_DIALOG_DATA)
  otp = new FormControl("", Validators.required);

  resendOtp() {
    this._showSnackbar('OTP resent');
    this.resendOtpEvent.emit();
  }

  login() {
    this.isLoginRequestSent = true; // disable send button
    this._showSnackbar('Login request sent successfully');
    console.log(`OTP Sent: ${this.data.email}, ${this.otp.value}`); /** inject and call auth service or do it in the form component */
  }

  private _showSnackbar(message: string) {
    this._snackBar.open(message, undefined, { duration: 3000 });
  }
}
