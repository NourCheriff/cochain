import {AfterViewInit, Component, ViewChild} from '@angular/core';
import {MatPaginator, MatPaginatorModule} from '@angular/material/paginator';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatSelectModule} from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
@Component({
  selector: 'app-certificates',
  imports: [MatInputModule,MatTableModule, MatPaginatorModule, MatButtonModule, MatIconModule, MatFormFieldModule, MatSelectModule],
  templateUrl: './certificates.component.html',
  styleUrl: './certificates.component.css'
})

export class CertificatesComponent implements AfterViewInit {
  displayedColumns: string[] = ['emitter', 'receiver', 'type', 'product'];
  dataSource = new MatTableDataSource<Certificate>(certificates);

  selected = 'all_certificates';

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  updateTable() {

    const BACKUP_DATA = certificates;

    var SELECTED_DATA: Certificate[] = [];

    switch(this.selected){
      case 'all_certificates':
        SELECTED_DATA = BACKUP_DATA;
      break;

      case 'quality':
        SELECTED_DATA = BACKUP_DATA.filter(item => item.type == 'Quality');
      break;

      case 'sustainability':
        SELECTED_DATA = BACKUP_DATA.filter(item => item.type == 'Sustainability');
      break;

      case 'origin':
        SELECTED_DATA = BACKUP_DATA.filter(item => item.type == 'Origin');
      break;
    }

    this.dataSource = new MatTableDataSource<Certificate>(SELECTED_DATA);
    this.dataSource.paginator = this.paginator;
  }

}

export interface Certificate {
  emitter: string;
  receiver: string;
  type: string;
  product: string
}

const certificates: Certificate[] = [
  {
    emitter: "Company A",
    receiver: "John Doe",
    type: "Origin",
    product: "P2",
  },
  {
    emitter: "Institute B",
    receiver: "Jane Smith",
    type: "Sustainability",
    product: "P3",
  },
  {
    emitter: "University C",
    receiver: "Carlos Ruiz",
    type: "Sustainability",
    product: "P2",
  },
  {
    emitter: "Organization D",
    receiver: "Emily Davis",
    type: "Quality",
    product: "P1",
  },
];
