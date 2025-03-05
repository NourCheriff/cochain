import { Component, Input } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon'

@Component({
  selector: 'app-wallet-info-card',
  imports: [MatButtonModule, MatIconModule],
  templateUrl: './wallet-info-card.component.html',
  styleUrl: './wallet-info-card.component.css'
})
export class WalletInfoCardComponent {
  @Input() walletID: string = '0x1234';
  @Input() balance: number = 0;
}
