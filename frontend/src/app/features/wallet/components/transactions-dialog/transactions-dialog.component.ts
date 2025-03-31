import { Component, inject, OnInit } from '@angular/core';
import { MatDialogActions, MatDialogClose, MatDialogContent, MatDialogRef, MatDialogTitle } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { BlockchainService } from '../../services/blockchain.service';
import { BaseHttpService } from 'src/app/core/services/api.service';
import { SupplyChainPartner } from 'src/models/company-entities/supply-chain-partner.model';
import { CompanyService } from 'src/app/features/users/services/company.service';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { SanitizerUtil } from 'src/app/core/utilities/sanitizer';

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
    ReactiveFormsModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './transactions-dialog.component.html',
  styleUrl: './transactions-dialog.component.css'
})
export class TransactionsDialogComponent implements OnInit{

  constructor(private companyService: CompanyService, private sanitizer: SanitizerUtil) {}

  readonly dialogRef = inject(MatDialogRef<TransactionsDialogComponent>);
  transactionForm = new FormGroup({
    receiver: new FormControl('', [Validators.required,]),
    amount: new FormControl(0, [Validators.required, Validators.min(0),]),
  });

  private blockchainService = inject(BlockchainService);
  private apiService = inject(BaseHttpService);
  supplyChainPartners: SupplyChainPartner[] = [];
  isLoading = false;

  ngOnInit(): void {
    this.getAllSupplyChainPartners();
  }

  getAllSupplyChainPartners() {
    this.companyService.getAllSupplyChainPartners().subscribe({
      next: (response) => this.supplyChainPartners = response.items || [] ,
      error: (error) => console.error('Error fetching supply chain partners.', error)
    });
  }

  async onSubmit(): Promise<void> {
    if (!this.transactionForm.valid)
        return;

    this.isLoading = true;

    const sanitizedForm = this.sanitizer.sanitizeForm(this.transactionForm);

    const receiver = sanitizedForm.receiver as string;
    const amount = sanitizedForm.amount as number;
    const receipt = await this.blockchainService.sendCarbonCredits(receiver, amount);

    if (!receipt) {
      this.isLoading= false;
      return;
    }

    const newTransaction = {
      transactionHash: receipt.hash!.toLowerCase(),
      walletIdEmitter: receipt.from!.toLowerCase(),
      walletIdReceiver: receiver.toLowerCase(),
    }

    this.apiService.add('api/Transaction/AddTransaction', newTransaction).subscribe((item) => {
      if (item) {
        console.log("Transaction added: ", item)
      }
    });

    this.isLoading = false;
    this.dialogRef.close();
  }
}
