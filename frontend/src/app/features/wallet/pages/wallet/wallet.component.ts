import { Component, inject } from '@angular/core';
import { TransactionsComponent } from '../transactions/transactions.component';
import { ConnectWalletComponent } from '../connect-wallet/connect-wallet.component';
import { BlockchainService } from '../../services/blockchain.service';
import { NgZone } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-wallet',
  imports: [TransactionsComponent, ConnectWalletComponent],
  templateUrl: './wallet.component.html',
  styleUrl: './wallet.component.css'
})
export class WalletComponent {
  isConnected: boolean = false;  // flag to display the connection page or the transactions page
  private blockchainService = inject(BlockchainService);
  private ngZone = inject(NgZone);
  private snackBar = inject(MatSnackBar);

  constructor() {
    // listen to wallet connection events from blockchain services
    this.blockchainService.isConnected.subscribe((value: boolean) => {
      this.ngZone.run(() => this.isConnected = value);
    });

    this.blockchainService.errorEvent.subscribe((message: string) => {
      this.ngZone.run(() => this.showMessage(message));
    });

    this.blockchainService.transferEvent.subscribe(event => {
      //Chiamata backend per salvataggio transaction console.log("Trasferimento: ", event);
    });
  }

  async connectWallet(): Promise<void> {
    await this.blockchainService.connectWallet();
  }

  showMessage(message: string) {
    this.snackBar.open(message, undefined, { duration: 3000 });
  }
}
