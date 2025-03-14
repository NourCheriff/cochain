import { Component, inject } from '@angular/core';
import {MatInputModule} from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { ReactiveFormsModule, FormControl, Validators, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { LoginDialogComponent } from '../dialog/dialog.component';
import { AuthService } from 'src/app/core/services/auth.service';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
  selector: 'app-login-form',
  imports: [ReactiveFormsModule, MatInputModule, MatButtonModule, MatProgressSpinnerModule],
  templateUrl: './form.component.html',
  styleUrl: './form.component.css'
})
export class LoginFormComponent {
  private authService = inject(AuthService);
  readonly dialog = inject(MatDialog);
  isLoading = false;

  loginForm = new FormGroup({
    email: new FormControl("", [Validators.required, Validators.email],),
  });

  requestOtp() {
    if (!this.loginForm.valid) return;

    this.isLoading = true;
    let email = this.loginForm.value.email!;
    this.authService.requestOtp(email).subscribe();
    if (!this.dialog.openDialogs || !this.dialog.openDialogs.length)
      this._openDialog();
  }

  private _openDialog() {
    const config = {
      restoreFocus: false,
      disableClose: true,
      data: { email: this.loginForm.value.email },
    };
    const ref = this.dialog.open(LoginDialogComponent, config);
    ref.componentInstance.resendOtpEvent.subscribe(this.requestOtp.bind(this));

    ref.afterClosed().subscribe((success) => this.isLoading = success);
  }
}
