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
import { ContractService } from 'src/app/features/contracts/service/contract.service';
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
export class TransactionsDialogComponent implements OnInit{

  constructor(private contractService: ContractService) {}

  readonly dialogRed = inject(MatDialogRef<TransactionsDialogComponent>);
  transactionForm = new FormGroup({
    receiver: new FormControl('', [Validators.required,]),
    amount: new FormControl(0, [Validators.required, Validators.min(0),]),
  });

  private blockchainService = inject(BlockchainService);
  private apiService = inject(BaseHttpService);
  supplyChainPartners: SupplyChainPartner[] = [];

  ngOnInit(): void {
    this.getAllSupplyChainPartners();
  }

  getAllSupplyChainPartners(){
    this.contractService.getAllSupplyChainPartner().subscribe({
      next: (response) => { this.supplyChainPartners = response.items || [] },
      error: (error) => console.error('Error fetching supply chain partners.', error)
    });
  }

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
