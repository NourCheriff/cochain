import { Component, inject } from '@angular/core';
import {MatInputModule} from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { ReactiveFormsModule, FormControl, Validators, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { LoginDialogComponent } from '../dialog/dialog.component';

@Component({
  selector: 'app-login-form',
  imports: [ReactiveFormsModule, MatInputModule, MatButtonModule],
  templateUrl: './form.component.html',
  styleUrl: './form.component.css'
})
export class LoginFormComponent {
  readonly dialog = inject(MatDialog);

  loginForm = new FormGroup({
    email: new FormControl("", [Validators.required, Validators.email],),
  });

  requestOtp() {
    console.log(`Hi, ${this.loginForm.value.email}`); /** inject and call auth service */
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
  }
}
