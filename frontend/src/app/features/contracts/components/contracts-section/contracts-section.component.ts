import { Component, OnInit, ViewChild, inject } from '@angular/core';
import {MatPaginator, MatPaginatorModule, PageEvent} from '@angular/material/paginator';
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
    "id": "3542da56-0de3-4797-a059-effff257f63d",
    "supplyChainPartner": "Prova company",// qui ci va il supply chain partner, quando l'utente effettua il login
    "role": "User"
  };

  displayedColumns: string[] = ['emitter', 'receiver', 'workType', 'attachment'];
  dataSource = new MatTableDataSource<Contract>([]);
  contracts: Contract[] = [];

  selected = 'received_contracts';
  @ViewChild(MatPaginator, { static: false }) paginator!: MatPaginator;

  constructor(private contractService: ContractService){}

  ngOnInit() {
    if (this.isAdmin()) {
      this.displayedColumns.push('action');
    }
    this.getContracts(5,1)
  }

  onPageChange(event: PageEvent){
    console.log('Cambiata la pagina:', event.pageIndex);
    console.log('Elementi per pagina:', event.pageSize);
    this.getContracts(event.pageSize, event.pageIndex)
  }

  getContracts(pageSize: number = 5, pageNumber: number = 1){
    const currentSCPId = this.user.id
    this.contractService.getContracts(currentSCPId,this.selected,pageSize.toString(),pageNumber.toString()).subscribe({
      next: (response) => {
        this.contracts = response
        this.dataSource.data = this.contracts;
        this.dataSource.paginator = this.paginator;
      },
      error: (error) => console.log(error)
    })
  }

  updateSelected(event: any){
    this.selected = event.value
    this.getContracts()
  }


  isAdmin(): boolean {
    return this.user.role === 'Admin';
  }

  addContractDialog() {
    this.dialog.open(ContractDialogComponent);
  }
}

export interface User {
  id: string,
  supplyChainPartner: string;
  role: string;
}

