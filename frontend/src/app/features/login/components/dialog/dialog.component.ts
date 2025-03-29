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
import { AuthService } from 'src/app/core/services/auth.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login-dialog',
  imports: [
    MatInputModule,
    MatButtonModule,
    MatDialogTitle,
    MatDialogContent,
    MatDialogActions,
    ReactiveFormsModule,
  ],
  templateUrl: './dialog.component.html',
  styleUrl: './dialog.component.css'
})
export class LoginDialogComponent {
  private authService = inject(AuthService);
  private router = inject(Router);
  private toasterService = inject(ToastrService);

  @Output() resendOtpEvent = new EventEmitter();
  isLoginRequestSent: boolean = false;
  failedAttempts: number = 0;
  maxAttempts: number = 3;
  lockoutTime: number = 30 * 1000; // 30 seconds
  isLocked: boolean = false;
  remainingTime = 0;
  timerInterval: any;

  readonly dialogRef = inject(MatDialogRef<LoginDialogComponent>);
  readonly data = inject<DialogData>(MAT_DIALOG_DATA)
  otp = new FormControl("", [Validators.required, Validators.pattern("^[0-9]{6}$")]);

  resendOtp() {
    this._showToast('OTP resent');
    this.resendOtpEvent.emit();
  }

  login() {

    if (this.isLocked) {
      this._showToast(`Too many failed attempts. Try again in ${this.remainingTime}s.`, 'error');
      return;
    }

    if (this.failedAttempts >= this.maxAttempts) {
      this.startTimer();
      return;
    }

    this.isLoginRequestSent = true;
    this._showToast('Login request sent');

    this.authService.login(this.data.email, this.otp.value!).subscribe((success) => {
      this.isLoginRequestSent = false;

      if (!success) {
        this.failedAttempts++;

        if (this.failedAttempts >= this.maxAttempts) {
          this.startTimer();
        } else {
          this._showToast(`Invalid OTP (${this.failedAttempts}/${this.maxAttempts})`, 'error');
        }
        return;
      }

      this.failedAttempts = 0;
      this.isLocked = false;
      this._showToast('Logged in successfully!', 'success');
      this.dialogRef.close(true);
      this.router.navigate(['']);
    });
  }

startTimer() {
  this.isLocked = true;
  this.remainingTime = this.lockoutTime/1000;
  this._showToast(`Too many failed attempts. Please wait ${this.remainingTime} seconds.`, 'error');

  this.timerInterval = setInterval(() => {
    this.remainingTime--;
    if (this.remainingTime <= 0) {
      clearInterval(this.timerInterval);
      this.failedAttempts = 0;
      this.isLocked = false;
      this._showToast('You can try logging in again.', 'info');
    }
  }, 1000);
}

  private _showToast(message: string, severity: string = 'info') {
    switch(severity) {
      case 'success':
        this.toasterService.success(message, 'Success');
        break;
      case 'error':
        this.toasterService.error(message, 'Error');
        break;
      default:
        this.toasterService.info(message, 'Info');
    }
  }
}

interface DialogData {
  email: string;
}
