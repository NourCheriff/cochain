import { Component, inject } from '@angular/core';
import {
  MatDialogActions,
  MatDialogClose,
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle,
} from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { BlockchainService } from '../../services/blockchain.service';
import { BaseHttpService } from 'src/app/core/services/api.service';

@Component({
  selector: 'app-transactions-dialog',
  imports: [
    MatDialogActions,
    MatFormFieldModule,
    MatDialogContent,
    MatSelectModule,
    MatInputModule,
    MatButtonModule,
    MatDialogTitle,
    MatDialogClose,
    ReactiveFormsModule
  ],
  templateUrl: './transactions-dialog.component.html',
  styleUrl: './transactions-dialog.component.css'
})
export class TransactionsDialogComponent {

  readonly dialogRed = inject(MatDialogRef<TransactionsDialogComponent>);
  transactionForm = new FormGroup({
    receiver: new FormControl('', [Validators.required,]),
    amount: new FormControl(0, [Validators.required, Validators.min(0),]),
  });

  private blockchainService = inject(BlockchainService);
  private apiService = inject(BaseHttpService);

  // inject and get from a service
  options: Option[] = [
    { value: 'SCP1 Address', displayValue: 'SCP1 ADDRESS - SCP Name'},
    { value: 'SCP2 Address', displayValue: 'SCP2 ADDRESS - SCP Name'},
    { value: 'SCP3 Address', displayValue: 'SCP3 ADDRESS - SCP Name'},
    { value: 'SCP4 Address', displayValue: 'SCP4 ADDRESS - SCP Name'},
    { value: 'SCP5 Address', displayValue: 'SCP5 ADDRESS - SCP Name'},
    { value: 'SCP6 Address', displayValue: 'SCP6 ADDRESS - SCP Name'},
    { value: 'SCP7 Address', displayValue: 'SCP7 ADDRESS - SCP Name'},
    { value: 'SCP8 Address', displayValue: 'SCP8 ADDRESS - SCP Name'},
    { value: 'SCP9 Address', displayValue: 'SCP9 ADDRESS - SCP Name'},
    { value: 'SCP10 Address', displayValue: 'SCP10 ADDRESS - SCP Name'},
    { value: 'SCP11 Address', displayValue: 'SCP11 ADDRESS - SCP Name' },
    { value: '0xb95348283a9714737059b4fdf50926924bdb4655', displayValue: 'Samuele'}
  ]

  async onSubmit() {
    if (this.transactionForm.valid) {
      let receiver = this.transactionForm.value.receiver as string;
      let amount = this.transactionForm.value.amount as number;
      let receipt = await this.blockchainService.sendCarbonCredits(receiver, amount);

      if (receipt) {
        const newTransaction = {
          transactionHash: receipt.hash!.toLowerCase(),
          walletIdEmitter: receipt.from!.toLowerCase(),
          walletIdReceiver: receiver.toLowerCase(),
        }

        this.apiService.add('api/Transaction/AddTransaction', newTransaction).subscribe({

        });

      }
    }
  }

}

interface Option {
  value: string;
  displayValue: string;
}
