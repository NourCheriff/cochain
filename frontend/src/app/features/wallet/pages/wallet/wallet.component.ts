import { Component } from '@angular/core';
import { TransactionsTableComponent } from "../../components/transactions-table/transactions-table.component";
import { ExpensesCardComponent } from "../../components/expenses-card/expenses-card.component";
import { IncomesCardComponent } from "../../components/incomes-card/incomes-card.component";
import { WalletInfoCardComponent } from "../../components/wallet-info-card/wallet-info-card.component";

@Component({
  selector: 'app-wallet',
  imports: [TransactionsTableComponent, ExpensesCardComponent, IncomesCardComponent, WalletInfoCardComponent],
  templateUrl: './wallet.component.html',
  styleUrl: './wallet.component.css'
})
export class WalletComponent {

}
