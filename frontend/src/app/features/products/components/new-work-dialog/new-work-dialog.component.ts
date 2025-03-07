import { Component, inject, ChangeDetectionStrategy} from '@angular/core';
import {
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle,
} from '@angular/material/dialog';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatInputModule} from '@angular/material/input';
import {MatSelectModule} from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';
import {provideNativeDateAdapter} from '@angular/material/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators,AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

@Component({
  selector: 'app-new-work-dialog',
    imports: [
      MatButtonModule,
      MatIconModule,
      MatSelectModule,
      MatDialogTitle,
      MatDialogContent,
      MatFormFieldModule,
      MatInputModule,
      MatDatepickerModule,
      ReactiveFormsModule
    ],
  templateUrl: './new-work-dialog.component.html',
  providers: [provideNativeDateAdapter()],
  styleUrl: './new-work-dialog.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class NewWorkDialogComponent {
  readonly dialogRef = inject(MatDialogRef<NewWorkDialogComponent>);
  /*myFilter = (d: Date | null): boolean => {
    const oggi = new Date();
    oggi.setHours(0, 0, 0, 0);
    return d ? d >= oggi : false;
  };*/
  selectedReceiver: string = '';
  selectedWorkType: string = '';
  isReceiverVisible: boolean = false;

  newWorkForm = new FormGroup({
    work: new FormControl('', Validators.required),
    receiver: new FormControl('', this.receiverValidator()),
    workDate: new FormControl(Date.now(),[Validators.required]),
    file: new FormControl('',Validators.required)
  });


  onSelectionChange(value: string) {
    this.selectedWorkType = value;
    this.isReceiverVisible = value === 'transport';
  }

  private receiverValidator(): ValidatorFn {
    return (control:AbstractControl) : ValidationErrors | null => {

      const value = control.value;
      const workValue = this.newWorkForm?.value?.work;

      // If the work value is "transport", receiver must be provided
      if (workValue === "transport" && (!value || value === '')) {
        return { receiverRequired: true };
      }

      return null;
  }
}
}

