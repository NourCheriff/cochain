import { Injectable, EventEmitter } from '@angular/core';
import { ethers } from 'ethers';
import ActivityCompiled from 'frontend/artifacts/contracts/Activity.sol/Activity.json';
import ActivityDeployed from 'frontend/deployments/docker/Activity.json';
import CarbonCreditsCompiled from 'frontend/artifacts/contracts/CarbonCredits.sol/CarbonCredits.json';
import CarbonCreditsDeployed from 'frontend/deployments/docker/CarbonCredits.json';
import { Transaction } from '../models/transaction.model';
import { BehaviorSubject, firstValueFrom } from 'rxjs';
import { BaseHttpService } from 'src/app/core/services/api.service';
import { environment } from 'src/environments/environment';

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

  private connectionStatusSubject = new BehaviorSubject<boolean>(false);
  public connectionStatus$ = this.connectionStatusSubject.asObservable();

  errorEvent = new EventEmitter<string>();
  transactionEvent = new EventEmitter<{ hash: string, status: string }>();
  transferEvent = new EventEmitter<{ from: string, to: string, amount: number }>();
  productEvent = new EventEmitter<{ from: string, to: string, tokenId: number }>();

  constructor(private apiService: BaseHttpService) {
    this.setup();
  }

  public isWalletConnected(): boolean {
    return this.account ? true : false;
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
      await this.initializeContracts();
      return true
    } catch (error) {
      console.error('Errore nella connessione del wallet:', error);
      this.connectionStatusSubject.next(false);
      this.errorEvent.emit("Error while connecting wallet: " + (error instanceof Error ? error.message : String(error)));
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

      this.carbonCreditsContract.on("Transfer", (from: string, to: string, amount: number) => {
        this.transferEvent.emit({
          from,
          to,
          amount
        });
      });
    } catch (error) {
      this.errorEvent.emit("Error while initializing contracts");
    }
  }

  public getAccount(): string | null {
    return this.account;
  }

  public async sendCarbonCredits(receiverAddress: string, amount: number): Promise<ethers.TransactionReceipt | undefined | null> {
    if (!this.signer || !this.account || !this.carbonCreditsContract) {
      this.errorEvent.emit('Wallet not connected');
      return null;
    }

    if (!ethers.isAddress(receiverAddress)) {
      this.errorEvent.emit('Receiver address not valid');
      return null;
    }

    try {
      const tx = await this.carbonCreditsContract['transfer'](receiverAddress, amount);
      this.transactionEvent.emit({ hash: tx.hash, status: 'pending' });

      const receipt = await this.provider?.waitForTransaction(tx.hash);
      this.transactionEvent.emit({ hash: tx.hash, status: 'confirmed' });

      return receipt;
    } catch (error) {
      console.error('Error while sending carbon credits:', error);
      this.errorEvent.emit('Failed transaction: ' + (error instanceof Error ? error.message : String(error)));
      return null;
    }
  }

  public async createProduct(productId: string, expirationDate: string): Promise<ethers.TransactionReceipt | undefined | null> {
    if (!this.signer || !this.account || !this.activityContract) {
      this.errorEvent.emit('Wallet not connected');
      return null;
    }

    try {
      const expirationDateFormatted = Math.floor(new Date(expirationDate).getTime() / 1000);
      const tx = await this.activityContract['createProduct'](productId, expirationDateFormatted);
      this.transactionEvent.emit({ hash: tx.hash, status: 'pending' });

      const receipt = await tx.wait();
      this.transactionEvent.emit({ hash: tx.hash, status: 'confirmed' });

      //const tokenId = receipt.logs[0].args[2];

      this.activityContract.on("Transfer", (from: string, to: string, tokenId: number) => {
        this.productEvent.emit({
          from,
          to,
          tokenId
        });
      });

      return receipt;
    }catch (error) {
      console.error('Error while adding product:', error);
      this.errorEvent.emit('Failed transaction: ' + (error instanceof Error ? error.message : String(error)));
      return null;
    }
  }

  public async addActivity(tokenId: number, activityId: string, emissions: number): Promise<ethers.TransactionReceipt | undefined | null> {
    if (!this.signer || !this.account || !this.activityContract){
      this.errorEvent.emit('Wallet not connected');
      return null;
    }

    try {
      const tx = await this.activityContract['addActivity'](tokenId, activityId, emissions);
      this.transactionEvent.emit({ hash: tx.hash, status: 'pending' });

      const receipt = await this.provider?.waitForTransaction(tx.hash);
      this.transactionEvent.emit({ hash: tx.hash, status: 'confirmed' });

      return receipt
    } catch (error) {
      console.error('Error while adding activity:', error);
      this.errorEvent.emit('Failed transaction: ' + (error instanceof Error ? error.message : String(error)));
      return null;
    }
  }

  public async addDocument(tokenId: number, documentHash: string): Promise<ethers.TransactionReceipt | undefined | null> {
    if (!this.signer || !this.account || !this.activityContract){
      this.errorEvent.emit('Wallet not connected');
      return null;
    }

    try {
      if (!documentHash.startsWith("0x")) {
        documentHash = "0x" + documentHash;
      }
      const tx = await this.activityContract['addDocument'](tokenId, documentHash);
      this.transactionEvent.emit({ hash: tx.hash, status: 'pending' });

      const receipt = await this.provider?.waitForTransaction(tx.hash);
      this.transactionEvent.emit({ hash: tx.hash, status: 'confirmed' });

      return receipt
    } catch (error) {
      console.error('Error while adding document:', error);
      this.errorEvent.emit('Failed transaction: ' + (error instanceof Error ? error.message : String(error)));
      return null;
      }
  }

  public async getActivity(tokenId: number, activityId: string): Promise<{ timestamp: number; id: string; scp: string; emissions: number } | null> {
    if (!this.provider || !this.account || !this.activityContract) {
        this.errorEvent.emit('Wallet not connected');
        return null;
    }

    try {
      const [timestamp, id, scp, emissions] = await this.activityContract['getActivity'](tokenId, activityId);
      const activity = {"timestamp": timestamp, "id": id, "scp": scp, "emissions": emissions}
      return activity;
    } catch (error) {
      console.error("Error while retrieving the activity:", error);
      this.errorEvent.emit("Error while retrieving the activity: " + (error instanceof Error ? error.message : String(error)));
      return null;
    }
  }

  public async getDocument(tokenId: number, documentHash: string): Promise<{ timestamp: number; documentHash: string } | null> {
    if (!this.provider || !this.account || !this.activityContract) {
        this.errorEvent.emit('Wallet not connected');
        return null;
    }

    try {
      const documentHashBytes32 = ethers.encodeBytes32String(documentHash);
      const [timestamp, hash] = await this.activityContract['getDocument'](tokenId, documentHashBytes32);
      const document = { "timestamp": timestamp, "documentHash": hash };
      return document;
    } catch (error) {
      console.error('Errore nel recuperare il documento: ', error);
      this.errorEvent.emit('Error while retrieving the document: ' + (error instanceof Error ? error.message : String(error)));
      return null;
    }
  }

  public async getActivities(tokenId: number): Promise<{ timestamp: number; activityId: string; scp: string; emissions: number }[] | null> {
    if (!this.provider || !this.account || !this.activityContract) {
      this.errorEvent.emit('Wallet not connected');
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
      console.error("Error while retrieving the activity: ", error);
      this.errorEvent.emit("Error while retrieving activities: " + (error instanceof Error ? error.message : String(error)));
      return null;
    }
  }

  public async getDocuments(tokenId: number): Promise<{ timestamp: number; documentHash: string }[] | null> {
    if (!this.provider || !this.account || !this.activityContract) {
      this.errorEvent.emit('Wallet not connected');
      return null;
    }

    try {
      const [timestamps, documentHashes] = await this.activityContract['getDocuments'](tokenId);

      return timestamps.map((timestamp: string, index: number) => ({
          timestamp: timestamp,
          documentHash: documentHashes[index],
      }));
    } catch (error) {
      console.error("Error while retrieving documents: ", error);
      this.errorEvent.emit("Error while retrieving documents: " + (error instanceof Error ? error.message : String(error)));
      return null;
    }
  }

  public async getWalletId(): Promise<string | null> {
    let accounts = await window.ethereum.request({ method: 'eth_accounts' });
    if (accounts.length)
      return accounts[0];

    this.errorEvent.emit('Error while retrieving account\'s wallet ID');
    return null;
  }

  public async getBalance(): Promise<string | null> {
    let accounts = await window.ethereum.request({ method: 'eth_accounts' });
    if (!this.provider || accounts.length === 0) {
      this.errorEvent.emit('Error while retrieving account\'s balance');
      return null;
    }

    if (!this.carbonCreditsContract) {
      await this.initializeContracts();
    }

    this.account = accounts[0];
    let balance = await this.carbonCreditsContract!!['balanceOf'](this.account);
    return balance.toString();
  }

  public async getTransactions(): Promise<Transaction[] | null> {
    const besuProvider = new ethers.JsonRpcProvider(environment.rpcUrl);

    try {
      if (!this.account || !this.provider) {
        console.warn("Account o provider non disponibili.");
        return null;
      }

      const res = await firstValueFrom(
        this.apiService.getAll("api/Transaction/transactions", { id: this.account })
      );

      const transactions: Transaction[] = (await Promise.all(
        res.map(async (tx: any) => {
          try {
            const txHash = await besuProvider.getTransaction(tx.transactionHash);
            if (!txHash) return null;

            const block = await besuProvider.getBlock(txHash.blockNumber!);
            if (!block) return null;

            return {
              id: txHash.hash,
              sender: tx.supplyChainPartnerEmitterName ?? '',
              receiver: tx.supplyChainPartnerReceiverName ?? '',
              amount: Number(txHash.value),
              date: new Date(block.timestamp * 1000).toISOString(),
            } as Transaction;
          } catch (error) {
            console.error("Error while parsing transactions:", error);
            return null;
          }
        })
      )).filter(Boolean) as Transaction[];

      return transactions.length ? transactions : null;

    } catch (error) {
      console.error("Error while loading transactions:", error);
      return [];
    }
  }

  /*public async certificateDocuments(activity: ProductLifeCycle): Promise<Hash>{
    if (!this.signer || !this.account) {
      const hash = this.activityContract!!['getEvent'](activity);
    }
  }*/

  private async setupAccount(): Promise<void> {
    let addEthereumChainResponse = await window.ethereum.request({
      "method": "wallet_addEthereumChain",
      "params": [
        {
          chainId: "0x539",
          chainName: "COCHAIN",
          rpcUrls: [
            environment.rpcUrl
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
      this.errorEvent.emit('Error while connecting to the network');
    }

    let accounts = await window.ethereum.request({ method: 'eth_requestAccounts' });
    this.account = accounts[0];
    this.connectionStatusSubject.next(true);
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
    this.connectionStatusSubject.next(false);
  }

  private async checkConnection(): Promise<void> {
    const accounts = await  window.ethereum.request({ method: 'eth_accounts' });
    this.connectionStatusSubject.next(accounts.length !== 0)
    if (accounts.length !== 0)
      this.account = accounts[0];
  }

  private async setProviderAndSigner(): Promise<void> {
    this.provider = new ethers.BrowserProvider(window.ethereum);
    this.signer = await this.provider.getSigner();
  }

  private async setup(): Promise<void> {
    await this.setProviderAndSigner();
    await this.initializeContracts();
    await this.checkConnection();
    this.setEventHandlers();
  }
}
