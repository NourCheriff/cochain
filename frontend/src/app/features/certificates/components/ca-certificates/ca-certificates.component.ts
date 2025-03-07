import {AfterViewInit, Component, inject, ViewChild} from '@angular/core';
import {MatPaginator, MatPaginatorModule} from '@angular/material/paginator';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatSelectModule} from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import {RouterLink, RouterLinkActive} from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { FileInputComponent } from '../file-input/file-input.component';
import {MatSort, MatSortModule} from '@angular/material/sort';
@Component({
  selector: 'app-ca-certificates',
  imports: [MatSortModule,RouterLink, RouterLinkActive,MatInputModule,MatTableModule, MatPaginatorModule, MatButtonModule, MatIconModule, MatFormFieldModule, MatSelectModule],
  templateUrl: './ca-certificates.component.html',
  styleUrl: './ca-certificates.component.css',
})
export class CaCertificatesComponent implements AfterViewInit {
  readonly dialog = inject(MatDialog);
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

export interface CaCertificate {
  receiver: string;
  scpType: string;
}

const certificates: CaCertificate[] = [
  {
    receiver: "SCP1",
    scpType: "Azienda agricola",
  },
  {
    receiver: "SCP2",
    scpType: "Azienda ittica",
  },
  {
    receiver: "SCP3",
    scpType: "Azienda di stoccaggio",
  },
   {
    receiver: "SCP1",
    scpType: "Azienda agricola",
  },
  {
    receiver: "SCP2",
    scpType: "Azienda ittica",
  },
  {
    receiver: "SCP3",
    scpType: "Azienda di stoccaggio",
  },
   {
    receiver: "SCP1",
    scpType: "Azienda agricola",
  },
  {
    receiver: "SCP2",
    scpType: "Azienda ittica",
  },
  {
    receiver: "SCP3",
    scpType: "Azienda di stoccaggio",
  },
   {
    receiver: "SCP1",
    scpType: "Azienda agricola",
  },
  {
    receiver: "SCP2",
    scpType: "Azienda ittica",
  },
  {
    receiver: "SCP3",
    scpType: "Azienda di stoccaggio",
  },
];

