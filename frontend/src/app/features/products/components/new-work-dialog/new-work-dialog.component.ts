import { Component, inject, ChangeDetectionStrategy} from '@angular/core';
import {
  MatDialogActions,
  MatDialogClose,
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

@Component({
  selector: 'app-new-work-dialog',
    imports: [
      MatButtonModule,
      MatIconModule,
      MatSelectModule,
      MatDialogTitle,
      MatDialogContent,
      MatDialogActions,
      MatDialogClose,
      MatFormFieldModule,
      MatInputModule,
      MatDatepickerModule
    ],
  templateUrl: './new-work-dialog.component.html',
  providers: [provideNativeDateAdapter()],
  styleUrl: './new-work-dialog.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class NewWorkDialogComponent {
  readonly dialogRef = inject(MatDialogRef<NewWorkDialogComponent>);
  myFilter = (d: Date | null): boolean => {
    const oggi = new Date();
    oggi.setHours(0, 0, 0, 0);
    return d ? d >= oggi : false;
  };
  selectedReceiver: string = '';
  selectedWorkType: string = '';
  isReceiverVisible: boolean = false;


  onSelectionChange(value: string) {
    this.selectedWorkType = value;
    this.isReceiverVisible = value === 'transport';
  }
}

