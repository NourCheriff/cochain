import { Component, EventEmitter, Output } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-connect-wallet',
  imports: [MatButtonModule],
  templateUrl: './connect-wallet.component.html',
  styleUrl: './connect-wallet.component.css'
})
export class ConnectWalletComponent {
  @Output() connectWalletEvent = new EventEmitter();

  connectWallet(): void {
    this.connectWalletEvent.emit();
  }

}
