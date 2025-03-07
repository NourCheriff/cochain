import { Component, EventEmitter, Output } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
  selector: 'app-connect-wallet',
  imports: [MatButtonModule, MatProgressSpinnerModule],
  templateUrl: './connect-wallet.component.html',
  styleUrl: './connect-wallet.component.css'
})
export class ConnectWalletComponent {
  @Output() connectWalletEvent = new EventEmitter();
  isLoading: boolean = false; // flag to display the connect button or the spinner

  connectWallet(): void {
    this.connectWalletEvent.emit();
    this.isLoading = true;
  }
}
