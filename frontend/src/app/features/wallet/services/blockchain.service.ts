import { Injectable, EventEmitter } from '@angular/core';
import { ethers } from 'ethers';

declare const window: any; // window object provided by the browser API

@Injectable({
  providedIn: 'root'
})
export class BlockchainService {

  // null values means that the account is disconnected
  private provider: ethers.BrowserProvider | null = null;  // for readonly operations
  private signer: ethers.JsonRpcSigner | null = null;  // for 'write' operations
  private network: string | null = null;
  private account: string | null = null; // account address (in hex representation)
  private transaction = new ethers.Transaction();

  isConnected = new EventEmitter<boolean>();
  errorEvent = new EventEmitter<string>();

  // returns a boolean that indicates if the connection was successfull or not
  async connectWallet(): Promise<boolean> {
    if (window.ethereum == null) {
      console.error('Metamask isn\'t installed');
      this.errorEvent.emit('Metamask isn\'t installed');
      return false;
    }

    try {
      await this.setupAccount();
      this.setEventHandlers();
    } catch (error) {
      this.isConnected.emit(false);
      this.errorEvent.emit("Failed to connect wallet");
      return false;
    }
    return true
  }

  async sendCarbonCredits(receiverAddress: string, amount: number) {
    if (this.account === null) return;
    if (!ethers.isAddress(receiverAddress)) return;

    try {
      let tx = await this.signer?.sendTransaction({
        from: this.account,
        to: receiverAddress,
        value: amount,
      });
    } catch(error) {
    }
  }

  private async setupAccount(): Promise<void> {
    this.provider = new ethers.BrowserProvider(window.ethereum);
    this.signer = await this.provider.getSigner();
    this.network = (await this.provider.getNetwork()).name
    let accounts = await window.ethereum.request({ method: 'eth_requestAccounts' });
    this.account = accounts[0];
    this.isConnected.emit(true);
  }

  private setEventHandlers(): void {
    window.ethereum.on('accountsChanged', (accounts: string[]) => this.onAccountsChanged(accounts));
  }

  private onAccountsChanged(accounts: string[]): void {
    if (accounts.length === 0) {
      this.disconnectWallet();
    } else {
      this.setupAccount();
    }
  }

  private disconnectWallet(): void {
    this.provider = null;
    this.signer = null;
    this.network = null;
    this.account = null;
    this.isConnected.emit(false);
  }
}
