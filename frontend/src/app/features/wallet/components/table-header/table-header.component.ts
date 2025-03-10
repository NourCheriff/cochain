import { Component, EventEmitter, Output } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-table-header',
  imports: [MatFormFieldModule, MatSelectModule, MatIconModule],
  templateUrl: './table-header.component.html',
  styleUrl: './table-header.component.css'
})
export class TableHeaderComponent {

  @Output() changeFilterEvent = new EventEmitter<string>();
  @Output() changeOrderEvent = new EventEmitter<string>();

  orderOptions: Options[] = [
    { value: 'date', displayValue: 'Date' },
    { value: 'id', displayValue: 'Transaction ID' },
    { value: 'amount', displayValue: 'Amount' },
  ];

  filterOptions: Options[] = [
    { value: 'all', displayValue: 'All transactions' },
    { value: 'in', displayValue: 'Incomes' },
    { value: 'out', displayValue: 'Expenses' },
  ]

  selectedFilter: string = this.filterOptions[0].value;
  selectedOrder: string = this.orderOptions[0].value;

  onFilterChange() {
    this.changeFilterEvent.emit(this.selectedFilter);
  }

  onOrderChange() {
    this.changeFilterEvent.emit(this.selectedOrder);
  }
}

interface Options {
  value: string;
  displayValue: string;
}
