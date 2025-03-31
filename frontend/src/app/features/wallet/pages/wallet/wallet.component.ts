import { Component, inject, OnInit, OnDestroy } from '@angular/core';
import { TransactionsComponent } from '../transactions/transactions.component';
import { ConnectWalletComponent } from '../connect-wallet/connect-wallet.component';
import { BlockchainService } from '../../services/blockchain.service';
import { NgZone } from '@angular/core';
import { Subscription } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-wallet',
  imports: [TransactionsComponent, ConnectWalletComponent],
  templateUrl: './wallet.component.html',
  styleUrl: './wallet.component.css'
})
export class WalletComponent implements OnInit, OnDestroy {
  isConnected: boolean = false;  // flag to display the connection page or the transactions page
  private blockchainService = inject(BlockchainService);
  private toasterService = inject(ToastrService);

  private ngZone = inject(NgZone);
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
        this.ngZone.run(() => this._showToast(message, 'error'));
      })
    );

    this.subscriptions.push(
      this.blockchainService.transferEvent.subscribe(event => {
        this.ngZone.run(() => this._showToast("Transaction successfully executed on the blockchain.", 'success'));
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

  private _showToast(message: string, severity: string = 'info') {
    switch(severity) {
      case 'success':
        this.toasterService.success(message, 'Success');
        break;
      case 'error':
        this.toasterService.error(message, 'Error');
        break;
      default:
        this.toasterService.info(message, 'Info');
    }
  }
}
