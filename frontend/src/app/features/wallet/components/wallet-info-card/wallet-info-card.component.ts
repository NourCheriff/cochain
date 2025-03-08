import { Component, inject, Input } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon'
import { TransactionsDialogComponent } from '../transactions-dialog/transactions-dialog.component';

@Component({
  selector: 'app-wallet-info-card',
  imports: [MatButtonModule, MatIconModule],
  templateUrl: './wallet-info-card.component.html',
  styleUrl: './wallet-info-card.component.css'
})
export class WalletInfoCardComponent {
  @Input() walletID: string = '0x1234';
  @Input() balance: number = 0;

  readonly dialog = inject(MatDialog);

  openDialog(): void {
    const dialogRef = this.dialog.open(TransactionsDialogComponent);
  }
}
