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
  isLoginRequestSent = false;

  readonly dialogRef = inject(MatDialogRef<LoginDialogComponent>);
  readonly data = inject<DialogData>(MAT_DIALOG_DATA)
  otp = new FormControl("", [Validators.required, Validators.pattern("^[0-9]{6}$")]);

  resendOtp() {
    this._showToast('OTP resent');
    this.resendOtpEvent.emit();
  }

  login() {
    this.isLoginRequestSent = true;
    this._showToast('Login request sent');
    this.authService.login(this.data.email, this.otp.value!).subscribe((success) => {
      if (!success) {
        this._showToast('Invalid Credentials', 'error');
        this.dialogRef.close(false);
        return;
      }

      this._showToast('Logged in successfully!', 'success');
      this.dialogRef.close(true);
      this.router.navigate(['']);
    });
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
