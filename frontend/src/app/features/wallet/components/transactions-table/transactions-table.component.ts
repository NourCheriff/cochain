import { AfterViewInit, Component, inject, ViewChild } from '@angular/core';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { Transaction } from '../../models/transaction.model';
import { TableHeaderComponent } from "../table-header/table-header.component";
import { MatSort, MatSortModule } from '@angular/material/sort';
import { BlockchainService } from '../../services/blockchain.service';
import { CommonModule } from '@angular/common';
import { AuthService } from 'src/app/core/services/auth.service';

@Component({
  selector: 'app-transactions-table',
  imports: [MatTableModule, MatPaginatorModule, TableHeaderComponent, MatSortModule, CommonModule],
  templateUrl: './transactions-table.component.html',
  styleUrl: './transactions-table.component.css'
})
export class TransactionsTableComponent implements AfterViewInit {

  private blockchainService = inject(BlockchainService);
  private authService = inject(AuthService);
  private currentSupplyChainPartner: string | undefined;

  displayedColumns: string[] = ['id', 'sender', 'receiver', 'amount', 'date']
  dataSource = new MatTableDataSource<Transaction>([]);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  ngAfterViewInit(): void {
    this.loadTransactions();
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.authService.getUser().subscribe((user) => this.currentSupplyChainPartner = user.supplyChainPartner?.name);
  }

  async loadTransactions() {
    try {
      const transactions = await this.blockchainService.getTransactions();
      if (transactions) {
        this.dataSource.data = transactions;
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      }
    } catch (error) {
      console.error("Error while loading transactions", error);
    }
  }

  filterData(filter: string) {
    this.dataSource.filterPredicate = (data: Transaction, filter: string): boolean => {
      switch(filter) {
        case 'in':
          return data.receiver.toLowerCase() === this.currentSupplyChainPartner?.toLowerCase();
        case 'out':
          return data.sender.toLowerCase() === this.currentSupplyChainPartner?.toLowerCase();
        default:
          return true;
      }
    };

    this.dataSource.filter = filter.trim().toLowerCase();
  }
}
