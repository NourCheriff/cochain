import {AfterViewInit, Component, OnInit, ViewChild, inject } from '@angular/core';
import {MatPaginator, MatPaginatorModule} from '@angular/material/paginator';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatSelectModule} from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';
import { ContractDialogComponent } from '../contract-dialog/contract-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { ContractService } from '../../service/contract.service';
import { Contract } from 'src/models/documents/contract.model';

@Component({
  selector: 'app-contracts-section',
  templateUrl: './contracts-section.component.html',
  styleUrl: './contracts-section.component.css',
  imports: [MatTableModule, MatPaginatorModule, MatButtonModule, MatIconModule, MatFormFieldModule, MatSelectModule]
})
export class ContractsSectionComponent implements OnInit {
  readonly dialog = inject(MatDialog);

  user: User = {
    "supplyChainPartner": "Prova company",// qui ci va il supply chain partner, quando l'utente effettua il login
    "role": "User"
  };

  displayedColumns: string[] = ['emitter', 'receiver', 'workType', 'attachment'];

  dataSource: any;
  contracts: Contract[] = [];

  selected = 'all_contracts';
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private contractService: ContractService){}

  ngOnInit() {
    if (this.isAdmin()) {
      this.displayedColumns.push('action');
    }
    //this.getAllContracts()
  }

  getAllContracts(){
    this.contractService.getAllContracts().subscribe({
      next: (response) => {
        this.contracts = response
        this.dataSource = new MatTableDataSource<Contract>(this.contracts);
        this.dataSource.paginator = this.paginator;
      },
      error: (error) => console.log(error)
    })
  }

  updateTable() {

    const BACKUP_DATA = this.contracts;
    let SELECTED_DATA: Contract[] = [];

    switch(this.selected){
      case 'all_contracts':
        SELECTED_DATA = BACKUP_DATA;
      break;

      case 'received_contracts':
       // SELECTED_DATA = BACKUP_DATA.filter(item => item.supplyChainPartnerReceiver?.name == this.user.supplyChainPartner);
      break;

      case 'emitted_contracts':
       // SELECTED_DATA = BACKUP_DATA.filter(item => item.userEmitter?.supplyChainPartner.name == this.user.supplyChainPartner);
      break;
    }

    this.dataSource = new MatTableDataSource<Contract>(SELECTED_DATA);
    this.dataSource.paginator = this.paginator;
  }

  isAdmin(): boolean {
    return this.user.role == "Admin";
  }

  addContract() {
    this.openDialog();
  }

  openDialog() {
    this.dialog.open(ContractDialogComponent);
  }
}

export interface User {
  supplyChainPartner: string;
  role: string;
}

