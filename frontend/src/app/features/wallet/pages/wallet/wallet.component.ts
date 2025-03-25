import { Component, inject, OnInit, OnDestroy } from '@angular/core';
import { TransactionsComponent } from '../transactions/transactions.component';
import { ConnectWalletComponent } from '../connect-wallet/connect-wallet.component';
import { BlockchainService } from '../../services/blockchain.service';
import { NgZone } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-wallet',
  imports: [TransactionsComponent, ConnectWalletComponent],
  templateUrl: './wallet.component.html',
  styleUrl: './wallet.component.css'
})
export class WalletComponent implements OnInit, OnDestroy {
  isConnected: boolean = false;  // flag to display the connection page or the transactions page
  private blockchainService = inject(BlockchainService);
  private ngZone = inject(NgZone);
  private snackBar = inject(MatSnackBar);
  private subscriptions: Subscription[] = []

  constructor() { }

  ngOnInit(): void {
    this.subscriptions.push(
      this.blockchainService.connectionStatus$.subscribe((value: boolean) => {
        this.ngZone.run(() => this.isConnected = value);
      })
    );

    this.subscriptions.push(
      this.blockchainService.errorEvent.subscribe((message: string) => {
        this.ngZone.run(() => this.showMessage(message));
      })
    );

    this.subscriptions.push(
      this.blockchainService.transferEvent.subscribe(event => {
        this.ngZone.run(() => this.showMessage("Transaction completed"));
      })
    )
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((subscription) => {
      subscription.unsubscribe();
    })
  }

  async connectWallet(): Promise<void> {
    await this.blockchainService.connectWallet();
  }

  showMessage(message: string) {
    this.snackBar.open(message, undefined, { duration: 3000 });
  }
}
