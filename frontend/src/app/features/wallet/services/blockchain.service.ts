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

  private activityABI: any;
  private carbonCreditsABI: any;

  private activityContract: ethers.Contract | null = null;
  private carbonCreditsContract: ethers.Contract | null = null;

  isConnected = new EventEmitter<boolean>();
  errorEvent = new EventEmitter<string>();
  transactionEvent = new EventEmitter<{hash: string, status: string}>();

  constructor() {
    try {
      this.activityABI = require('frontend/artifacts/contracts/Activity.sol/Activity.json').abi;
      this.carbonCreditsABI = require('frontend/artifacts/contracts/CarbonCredits.sol/CarbonCredits.json').abi;
    } catch (error) {
      console.error('Errore nel caricamento degli ABI:', error);
    }
  }

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
      await this.initializeContracts();
      return true
    } catch (error) {
      console.error('Errore nella connessione del wallet:', error);
      this.isConnected.emit(false);
      this.errorEvent.emit("Impossibile connettere il wallet: " + (error instanceof Error ? error.message : String(error)));
      return false;
    }
  }

  private async initializeContracts(): Promise<void> {
    if (!this.signer) return;

    const activityContractAddress = require('frontend/deployments/localhost/Activity.json').address;
    const carbonCreditsContractAddress = require('frontend/deployments/localhost/CarbonCredits.json').address;

    try {
      this.activityContract = new ethers.Contract(
        activityContractAddress,
        this.activityABI,
        this.signer
      );

      this.carbonCreditsContract = new ethers.Contract(
        carbonCreditsContractAddress,
        this.carbonCreditsABI,
        this.signer
      );
    } catch (error) {
      this.errorEvent.emit("Errore nell'inizializzazione dei contratti");
    }
  }

  async sendCarbonCredits(receiverAddress: string, amount: number): Promise<string | null> {
    if (!this.signer || !this.account || !this.carbonCreditsContract) {
      this.errorEvent.emit('Wallet non connesso');
      return null;
    }

    if (!ethers.isAddress(receiverAddress)) {
      this.errorEvent.emit('Indirizzo destinatario non valido');
      return null;
    }

    try {
      const amountInWei = ethers.parseUnits(amount.toString(), 18);

      const tx = await this.carbonCreditsContract['transfer'](receiverAddress, amountInWei);
      this.transactionEvent.emit({hash: tx.hash, status: 'pending'});

      const receipt = await tx.wait(); // wait for transaction to confirmation
      this.transactionEvent.emit({hash: tx.hash, status: 'confirmed'});

      return tx.hash;
    } catch (error) {
      console.error('Errore nell\'invio dei carbon credits:', error);
      this.errorEvent.emit('Transazione fallita: ' + (error instanceof Error ? error.message : String(error)));
      return null;
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
