import { Injectable, EventEmitter } from '@angular/core';
import { ethers } from 'ethers';
import ActivityCompiled from 'frontend/artifacts/contracts/Activity.sol/Activity.json';
import ActivityDeployed from 'frontend/deployments/localhost/Activity.json';
import CarbonCreditsCompiled from 'frontend/artifacts/contracts/CarbonCredits.sol/CarbonCredits.json';
import CarbonCreditsDeployed from 'frontend/deployments/localhost/CarbonCredits.json';
import { Transaction } from '../models/transaction.model';
import { lastValueFrom } from 'rxjs';
import { Hash } from 'js-sha256';
import { ProductLifeCycle } from 'src/models/product/product-life-cycle.model';
import { BaseHttpService } from 'src/app/core/services/api.service';

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

  private activityAddress: any;
  private carbonCreditsAddress: any;

  private activityContract: ethers.Contract | null = null;
  private carbonCreditsContract: ethers.Contract | null = null;

  isConnected = new EventEmitter<boolean>();
  errorEvent = new EventEmitter<string>();
  transactionEvent = new EventEmitter<{ hash: string, status: string }>();
  transferEvent = new EventEmitter<{ from: string, to: string, amount: number }>();

  constructor(private apiService: BaseHttpService) {
    this.initializeContracts();
  }

  // returns a boolean that indicates if the connection was successfull or not
  public async connectWallet(): Promise<boolean> {
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

    this.activityABI = ActivityCompiled.abi;
    this.carbonCreditsABI = CarbonCreditsCompiled.abi;

    this.activityAddress = ActivityDeployed.address;
    this.carbonCreditsAddress = CarbonCreditsDeployed.address;

    try {
      this.activityContract = new ethers.Contract(
        this.activityAddress,
        this.activityABI,
        this.signer
      );

      this.carbonCreditsContract = new ethers.Contract(
        this.carbonCreditsAddress,
        this.carbonCreditsABI,
        this.signer
      );

      this.carbonCreditsContract.on("Transfer", (from: string, to: string, amount: ethers.BigNumberish) => {
        this.transferEvent.emit({
          from,
          to,
          amount: Number()
        });
      });
    } catch (error) {
      this.errorEvent.emit("Errore nell'inizializzazione dei contratti");
    }
  }

  public async sendCarbonCredits(receiverAddress: string, amount: number): Promise<ethers.TransactionReceipt | undefined | null> {
    if (!this.signer || !this.account || !this.carbonCreditsContract) {
      this.errorEvent.emit('Wallet non connesso');
      return null;
    }

    if (!ethers.isAddress(receiverAddress)) {
      this.errorEvent.emit('Indirizzo destinatario non valido');
      return null;
    }

    try {
      const tx = await this.carbonCreditsContract['transfer'](receiverAddress, amount);
      this.transactionEvent.emit({ hash: tx.hash, status: 'pending' });

      const receipt = await this.provider?.waitForTransaction(tx.hash);
      this.transactionEvent.emit({ hash: tx.hash, status: 'confirmed' });

      return receipt;
    } catch (error) {
      console.error('Errore nell\'invio dei carbon credits:', error);
      this.errorEvent.emit('Transazione fallita: ' + (error instanceof Error ? error.message : String(error)));
      return null;
    }
  }

  public async addActivity(tokenId: number, activityId: string, emissions: number): Promise<ethers.TransactionReceipt | undefined | null> {
    if (!this.signer || !this.account || !this.activityContract){
      this.errorEvent.emit('Wallet non connesso');
      return null;
    }

    try {
      const tx = await this.activityContract['addActivity'](tokenId, activityId, emissions);
      this.transactionEvent.emit({ hash: tx.hash, status: 'pending' });

      const receipt = await this.provider?.waitForTransaction(tx.hash);
      this.transactionEvent.emit({ hash: tx.hash, status: 'confirmed' });

      return receipt
    } catch (error) {
      console.error('Errore nell\'aggiungere l\'attività:', error);
      this.errorEvent.emit('Transazione fallita: ' + (error instanceof Error ? error.message : String(error)));
      return null;
      }
  }

  public async addDocument(tokenId: number, documentHash: string): Promise<ethers.TransactionReceipt | undefined | null> {
    if (!this.signer || !this.account || !this.activityContract){
      this.errorEvent.emit('Wallet non connesso');
      return null;
    }

    try {
      const tx = await this.activityContract['addDocument'](tokenId, documentHash);
      this.transactionEvent.emit({ hash: tx.hash, status: 'pending' });

      const receipt = await this.provider?.waitForTransaction(tx.hash);
      this.transactionEvent.emit({ hash: tx.hash, status: 'confirmed' });

      return receipt
    } catch (error) {
      console.error('Errore nell\'aggiungere il documento:', error);
      this.errorEvent.emit('Transazione fallita: ' + (error instanceof Error ? error.message : String(error)));
      return null;
      }
  }

  public async getActivity(tokenId: number, activityId: string): Promise<{ timestamp: number; id: string; scp: string; emissions: number } | null> {
    if (!this.provider || !this.account || !this.activityContract) {
        this.errorEvent.emit('Wallet non connesso');
        return null;
    }

    try {
      const [timestamp, id, scp, emissions] = await this.activityContract['getActivity'](tokenId, activityId);
      const activity = {"timestamp": timestamp, "id": id, "scp": scp, "emissions": emissions}
      return activity;
    } catch (error) {
      console.error("Errore nel recuperare l'attività: ", error);
      this.errorEvent.emit("Errore nel recuperare l'attività: " + (error instanceof Error ? error.message : String(error)));
      return null;
    }
  }

  public async getDocument(tokenId: number, documentHash: string): Promise<{ timestamp: number; documentHash: string } | null> {
    if (!this.provider || !this.account || !this.activityContract) {
        this.errorEvent.emit('Wallet non connesso');
        return null;
    }

    try {
      const documentHashBytes32 = ethers.encodeBytes32String(documentHash);
      const [timestamp, hash] = await this.activityContract['getDocument'](tokenId, documentHashBytes32);
      const document = { "timestamp": timestamp, "documentHash": hash };
      return document;
    } catch (error) {
      console.error('Errore nel recuperare il documento: ', error);
      this.errorEvent.emit('Errore nel recuperare il documento: ' + (error instanceof Error ? error.message : String(error)));
      return null;
    }
  }

  public async getActivities(tokenId: number): Promise<{ timestamp: number; activityId: string; scp: string; emissions: number }[] | null> {
    if (!this.provider || !this.account || !this.activityContract) {
      this.errorEvent.emit('Wallet non connesso');
      return null;
    }

    try {
      const [timestamps, activityIds, scps, emissions] = await this.activityContract['getActivities'](tokenId);

      return timestamps.map((timestamp: string, index: number) => ({
          timestamp: timestamp,
          activityId: activityIds[index],
          scp: scps[index],
          emissions: emissions[index].toNumber(),
      }));
    } catch (error) {
      console.error("Errore nel recuperare le attività: ", error);
      this.errorEvent.emit("Errore nel recuperare le attività: " + (error instanceof Error ? error.message : String(error)));
      return null;
    }
  }

  public async getDocuments(tokenId: number): Promise<{ timestamp: number; documentHash: string }[] | null> {
    if (!this.provider || !this.account || !this.activityContract) {
      this.errorEvent.emit('Wallet non connesso');
      return null;
    }

    try {
      const [timestamps, documentHashes] = await this.activityContract['getDocuments'](tokenId);

      return timestamps.map((timestamp: string, index: number) => ({
          timestamp: timestamp,
          documentHash: documentHashes[index],
      }));
    } catch (error) {
      console.error("Errore nel recuperare i documenti: ", error);
      this.errorEvent.emit("Errore nel recuperare i documenti: " + (error instanceof Error ? error.message : String(error)));
      return null;
    }
  }


  public async removeDocument(tokenId: number, documentHashToRemove: string): Promise<ethers.TransactionReceipt | undefined | null> {
    if (!this.provider || !this.account || !this.activityContract){
      this.errorEvent.emit('Wallet non connesso');
      return null;
    }

    try {
      const tx = await this.activityContract['removeDocument'](tokenId, documentHashToRemove);
      this.transactionEvent.emit({ hash: tx.hash, status: 'pending' });

      const receipt = await this.provider?.waitForTransaction(tx.hash);
      this.transactionEvent.emit({ hash: tx.hash, status: 'confirmed' });

      return receipt
    } catch (error) {
      console.error('Errore nell\'eliminazione del documento: ', error);
      this.errorEvent.emit('Transazione fallita: ' + (error instanceof Error ? error.message : String(error)));
      return null;
      }
  }

  public get walletId(): string | null {
    if (this.provider && this.account)
      return this.account;

    this.errorEvent.emit('Impossibile recuperare il wallet ID dell\'account');
    return null;
  }

  public async getBalance(): Promise<string | null> {
    if (!this.provider || !this.account) {
      this.errorEvent.emit('Impossibile recuperare il wallet ID dell\'account');
      return null;
    }

    if (!this.carbonCreditsContract) {
      await this.initializeContracts();
    }

    let balance = await this.carbonCreditsContract!!['balanceOf'](this.account);
    return balance.toString();
  }

  public async getTransactions(): Promise<Transaction[]> {
    const params = { walletId: this.account || '' };
    const etherScan = new ethers.EtherscanProvider();

    try {
      if (this.account && this.provider) {
        const res: any[] = await lastValueFrom(this.apiService.getAll("api/Transaction/transactions", { id: this.account }));

        const transactions: Transaction[] = await Promise.all(
          res.map(async (transaction) => {
            const tx = await etherScan.getTransaction(transaction.hash);
            if (!tx) return null;

            const block = await etherScan.getBlock(tx.blockNumber!);
            transaction = {
              id: tx.hash,
              sender: tx.from,
              receiver: tx.to ?? '',
              amount: tx.value,
              date: new Date(block!!.timestamp * 1000).toISOString(),
            }
            return transaction;
          })
        );

        return transactions.filter((tx) => tx !== null);
      }
      else {
        return [];
      }
    } catch (error) {
      console.error('Errore nel recupero delle transazioni:', error);
      return [];
    }
  }

  /*public async certificateDocuments(activity: ProductLifeCycle): Promise<Hash>{
    if (!this.signer || !this.account) {
      const hash = this.activityContract!!['getEvent'](activity);
    }
  }*/

  private async setupAccount(): Promise<void> {
    this.provider = new ethers.BrowserProvider(window.ethereum);
    this.signer = await this.provider.getSigner();

    let addEthereumChainResponse = await window.ethereum.request({
      "method": "wallet_addEthereumChain",
      "params": [
        {
          chainId: "0x539",
          chainName: "COCHAIN",
          rpcUrls: [
            "http://127.0.0.1:8545"
          ],
          nativeCurrency: {
            name: "ETH",
            symbol: "ETH",
            decimals: 18
          },
        }
      ]
    });
    if (!addEthereumChainResponse) {
      await window.ethereum.request({
        "method": "wallet_switchEthereumChain",
        "params": [
          {
            chainId: "0x539"
          },
        ]
      });
    }
    else {
      this.errorEvent.emit('Errore nel collegamento con la network');
    }

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
    this.activityABI = null;
    this.carbonCreditsABI = null;
    this.activityAddress = null;
    this.carbonCreditsAddress = null;
    this.activityContract = null;
    this.carbonCreditsContract = null;
    this.isConnected.emit(false);
  }
}
