import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { Transaction } from '../../models/transaction.model';
import { TableHeaderComponent } from "../table-header/table-header.component";
import { MatSort, MatSortModule } from '@angular/material/sort';

@Component({
  selector: 'app-transactions-table',
  imports: [MatTableModule, MatPaginatorModule, TableHeaderComponent, MatSortModule],
  templateUrl: './transactions-table.component.html',
  styleUrl: './transactions-table.component.css'
})
export class TransactionsTableComponent implements AfterViewInit {

  displayedColumns: string[] = ['id', 'sender', 'receiver', 'amount', 'date']
  dataSource = new MatTableDataSource<Transaction>(DATA);
  user = 'SCP A'

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  ngAfterViewInit(): void {
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
  }

  filterData(filter: string) {
    this.dataSource.filterPredicate = (data: Transaction, filter: string): boolean => {
      switch(filter) {
        case 'in':
          return data.receiver === this.user;
        case 'out':
          return data.sender === this.user;
        default:
          return true;
      }
    };

    this.dataSource.filter = filter.trim().toLowerCase();
  }
}

const DATA: Transaction[] = [
  {
    id: '1',
    sender: 'SCP A',
    receiver: 'SCP B',
    amount: 3,
    date: '12 sep 2024, 12:30 AM',
  },
  {
    id: '2',
    sender: 'SCP A',
    receiver: 'SCP B',
    amount: 1,
    date: '12 sep 2024, 12:30 AM',
  },
  {
    id: '3',
    sender: 'SCP B',
    receiver: 'SCP A',
    amount: 5,
    date: '12 sep 2024, 12:30 AM',
  },
  {
    id: '4',
    sender: 'SCP B',
    receiver: 'SCP A',
    amount: 4,
    date: '12 sep 2024, 12:30 AM',
  }
]
