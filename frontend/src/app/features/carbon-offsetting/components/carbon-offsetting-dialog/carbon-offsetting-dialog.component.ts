import { Component, inject } from '@angular/core';
import { MatDialogContent, MatDialogRef } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ReactiveFormsModule, FormControl, Validators, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { SanitizerUtil } from 'src/app/core/utilities/sanitizer';
import { CarbonOffsettingAction } from 'src/models/carbon-offset/carbon-offsetting-actions.model';
import { AuthService } from 'src/app/core/services/auth.service';
import { CarbonOffsettingService } from '../../services/carbon-offsetting.service';

@Component({
  selector: 'app-carbon-offsetting-dialog',
  imports: [
    MatDialogContent,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    ReactiveFormsModule,
  ],
  templateUrl: './carbon-offsetting-dialog.component.html',
  styleUrl: './carbon-offsetting-dialog.component.css'
})
export class CarbonOffsettingDialogComponent {

  private toasterService = inject(ToastrService);
  private authService = inject(AuthService);
  private sanitizer = inject(SanitizerUtil);
  private carbonOffsettingService = inject(CarbonOffsettingService);

  readonly dialogRef = inject(MatDialogRef<CarbonOffsettingDialogComponent>);

  carbonOffsettingForm = new FormGroup<CarbonOffsettingForm>({
    action: new FormControl('', [Validators.required, Validators.pattern('^[a-zA-Z ]*$')]),
    offset: new FormControl(0, [Validators.required, Validators.pattern("^[0-9]*$"), Validators.min(0)]),
  });

  public addCarbonOffsettingAction(): void {
    if (!this.carbonOffsettingForm.valid)
      return;

    const sanitizedForm = this.sanitizer.sanitizeForm(this.carbonOffsettingForm);
    let carbonOffsettingAction: CarbonOffsettingAction = {
      name: sanitizedForm.action,
      offset: sanitizedForm.offset,
      supplyChainPartnerId: this.authService.userId!,
    }

    this.carbonOffsettingService.addCarbonOffsettingAction(carbonOffsettingAction).subscribe({
      next: (res) => {
        this.showToast('Action inserted successfully!', 'success');
        this.dialogRef.close(true);
      },
      error: (err) => {
        this.showToast('Error on action insertion', 'error');
      }
    });
  }

  private showToast(message: string, severity: string | undefined) {
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

interface CarbonOffsettingForm {
  action: FormControl<string | null>;
  offset: FormControl<number | null>;
}
