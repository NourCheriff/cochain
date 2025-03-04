import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { Transaction } from '../../models/transaction.model';

@Component({
  selector: 'app-transactions-table',
  imports: [MatTableModule, MatPaginatorModule],
  templateUrl: './transactions-table.component.html',
  styleUrl: './transactions-table.component.css'
})
export class TransactionsTableComponent implements AfterViewInit {
  displayedColumns: string[] = ['transaction-id', 'sender', 'receiver', 'amount', 'date']
  dataSource = new MatTableDataSource<Transaction>(DATA);

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  ngAfterViewInit(): void {
      this.dataSource.paginator = this.paginator;
  }
}

const DATA: Transaction[] = [
  {
    id: '1',
    sender: 'SCP A',
    receiver: 'SCP B',
    amount: 3,
    date: '12 sep 2024, 12:30 AM',
  }
]
