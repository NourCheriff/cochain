import {AfterViewInit, Component, inject, ViewChild} from '@angular/core';
import {MatPaginator, MatPaginatorModule} from '@angular/material/paginator';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatSelectModule} from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import {RouterLink, RouterLinkActive, RouterOutlet} from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { FileInputComponent } from '../file-input/file-input.component';
@Component({
  selector: 'app-ca-certificates',
  imports: [RouterLink, RouterLinkActive,MatInputModule,MatTableModule, MatPaginatorModule, MatButtonModule, MatIconModule, MatFormFieldModule, MatSelectModule],
  templateUrl: './ca-certificates.component.html',
  styleUrl: './ca-certificates.component.css'
})
export class CaCertificatesComponent implements AfterViewInit {
  displayedColumns: string[] = ['receiver', 'scpType', 'attachments', 'actions'];
  dataSource = new MatTableDataSource<CaCertificate>(certificates);
  readonly dialog = inject(MatDialog);
  selected = 'receiver';

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  orderBy() {
    const BACKUP_DATA = certificates;

    var SELECTED_DATA: CaCertificate[] = [];

    switch (this.selected) {
      case 'receiver':
        SELECTED_DATA = [...BACKUP_DATA].sort((a, b) => a.receiver.localeCompare(b.receiver));
        break;

      case 'scp':
        SELECTED_DATA = [...BACKUP_DATA].sort((a, b) => a.scpType.localeCompare(b.scpType));
        break;

      default:
        SELECTED_DATA = [...BACKUP_DATA];
        break;
    }

    this.dataSource = new MatTableDataSource<CaCertificate>(SELECTED_DATA);
    this.dataSource.paginator = this.paginator;
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

