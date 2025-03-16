import {AfterViewInit, Component, inject, ViewChild} from '@angular/core';
import {MatPaginator, MatPaginatorModule} from '@angular/material/paginator';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatSelectModule} from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { RouterLink } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { CommonModule } from '@angular/common'; // Add this import for NgClass
import {MatSort, MatSortModule} from '@angular/material/sort';
import { FileInputComponent } from '../../components/file-input/file-input.component';

@Component({
  selector: 'app-certificates',
  imports: [CommonModule,MatSortModule,RouterLink, MatInputModule,MatTableModule, MatPaginatorModule, MatButtonModule, MatIconModule, MatFormFieldModule, MatSelectModule],
  templateUrl: './certificates.component.html',
  styleUrl: './certificates.component.css',
})
export class CertificatesComponent implements AfterViewInit {
  readonly dialog = inject(MatDialog);

  scpType: SCPType = {
    "type": "CA"
  }

  displayedColumns: string[] = ['receiver', 'scpType', 'attachments', 'actions'];
  dataSource = new MatTableDataSource<CaCertificate>(certificates);
  selected = 'receiver';

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  attachCertificate() {
    this.dialog.open(FileInputComponent);
  }

}

export interface SCPType {
  type: string
}

export interface CaCertificate {
  id: string;
  receiver: string;
  scpType: string;
}

const certificates: CaCertificate[] = [
  {
    id: '1',
    receiver: "SCP1",
    scpType: "Azienda agricola",
  },
  {
    id: '2',
    receiver: "SCP2",
    scpType: "Azienda ittica",
  },
  {
    id: '3',
    receiver: "SCP3",
    scpType: "Azienda di stoccaggio",
  },
  {
    id: '4',
    receiver: "SCP1",
    scpType: "Azienda agricola",
  },
  {
    id: '5',
    receiver: "SCP2",
    scpType: "Azienda ittica",
  },
  {
    id: '6',
    receiver: "SCP3",
    scpType: "Azienda di stoccaggio",
  },
  {
    id: '7',
    receiver: "SCP1",
    scpType: "Azienda agricola",
  },
  {
    id: '8',
    receiver: "SCP2",
    scpType: "Azienda ittica",
  },
  {
    id: '9',
    receiver: "SCP3",
    scpType: "Azienda di stoccaggio",
  },
];

