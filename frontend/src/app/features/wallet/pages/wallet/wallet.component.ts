import { Component, inject } from '@angular/core';
import { TransactionsComponent } from '../transactions/transactions.component';
import { ConnectWalletComponent } from '../connect-wallet/connect-wallet.component';
import { BlockchainService } from '../../services/blockchain.service';

@Component({
  selector: 'app-wallet',
  imports: [TransactionsComponent, ConnectWalletComponent],
  templateUrl: './wallet.component.html',
  styleUrl: './wallet.component.css'
})
export class WalletComponent {
  isConnected: boolean = false;
  private blockchainService = inject(BlockchainService);

  connectWallet(): void {
    // TODO: Change logic & isConnected handling (rely on connectWallet result)
    this.blockchainService.connectWallet();
    this.isConnected  = true;
  }
}
