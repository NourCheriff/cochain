import { Component, inject, AfterViewInit } from '@angular/core';
import { TransactionsTableComponent } from "../../components/transactions-table/transactions-table.component";
import { ExpensesCardComponent } from "../../components/expenses-card/expenses-card.component";
import { IncomesCardComponent } from "../../components/incomes-card/incomes-card.component";
import { WalletInfoCardComponent } from "../../components/wallet-info-card/wallet-info-card.component";
import { BlockchainService } from '../../services/blockchain.service';

@Component({
  selector: 'app-transactions',
  imports: [TransactionsTableComponent, ExpensesCardComponent, IncomesCardComponent, WalletInfoCardComponent],
  templateUrl: './transactions.component.html',
  styleUrl: './transactions.component.css'
})
export class TransactionsComponent implements AfterViewInit {

  private blockChainService = inject(BlockchainService);
  walletId: string | null = null;
  balance: string | null = null;

  constructor() { }

  ngAfterViewInit(): void {
    this.blockChainService.getWalletId().then((walletId) => this.walletId = walletId);
    this.blockChainService.getBalance().then((balance) => this.balance = balance);
  }
}
