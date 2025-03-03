import {AfterViewInit, Component, ViewChild} from '@angular/core';
import {MatPaginator, MatPaginatorModule} from '@angular/material/paginator';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatSelectModule} from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';

@Component({
  selector: 'app-contracts-section',
  templateUrl: './contracts-section.component.html',
  styleUrl: './contracts-section.component.css',
  imports: [MatTableModule, MatPaginatorModule, MatButtonModule, MatIconModule, MatFormFieldModule, MatSelectModule]
})
export class ContractsSectionComponent implements AfterViewInit {
  displayedColumns: string[] = ['emitter', 'receiver', 'workType', 'actions'];
  dataSource = new MatTableDataSource<PeriodicElement>(ELEMENT_DATA);

  user = "Alpha" // qui ci va il supply chain partner, quando l'utente effettua il login

  selected = 'all_contracts';

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  updateTable() {

    const BACKUP_DATA = ELEMENT_DATA;

    var SELECTED_DATA: PeriodicElement[] = [];

    switch(this.selected){
      case 'all_contracts':
        SELECTED_DATA = BACKUP_DATA;
      break;

      case 'recieved_contracts':
        SELECTED_DATA = BACKUP_DATA.filter(item => item.receiver == this.user);
      break;

      case 'emitted_contracts':
        SELECTED_DATA = BACKUP_DATA.filter(item => item.emitter == this.user);
      break;
    }

    this.dataSource = new MatTableDataSource<PeriodicElement>(SELECTED_DATA);
    this.dataSource.paginator = this.paginator;
    console.log(`Hi, dolphin`); /** inject and call auth service */
  }

}

export interface PeriodicElement {
  emitter: string;
  receiver: string;
  workType: string;
}

//il seguente array andrà popolato con le attività del supply chain partner che ha effettuato il login
const ELEMENT_DATA: PeriodicElement[] = [
  { "emitter": "Alpha", "receiver": "Beta", "workType": "Transport" },
  { "emitter": "Delta", "receiver": "Alpha", "workType": "Packaging" },
  { "emitter": "Gamma", "receiver": "Alpha", "workType": "Seed" },
  { "emitter": "Alpha", "receiver": "Epsilon", "workType": "Storage" },
  { "emitter": "Zeta", "receiver": "Alpha", "workType": "Transport" },
  { "emitter": "Alpha", "receiver": "Eta", "workType": "Packaging" },
  { "emitter": "Theta", "receiver": "Alpha", "workType": "Seed" },
  { "emitter": "Iota", "receiver": "Alpha", "workType": "Storage" },
  { "emitter": "Alpha", "receiver": "Kappa", "workType": "Transport" },
  { "emitter": "Lambda", "receiver": "Alpha", "workType": "Packaging" },
  { "emitter": "Mu", "receiver": "Alpha", "workType": "Seed" },
  { "emitter": "Alpha", "receiver": "Nu", "workType": "Storage" },
  { "emitter": "Xi", "receiver": "Alpha", "workType": "Transport" },
  { "emitter": "Omicron", "receiver": "Alpha", "workType": "Packaging" },
  { "emitter": "Alpha", "receiver": "Pi", "workType": "Seed" },
  { "emitter": "Rho", "receiver": "Alpha", "workType": "Storage" },
  { "emitter": "Sigma", "receiver": "Alpha", "workType": "Transport" },
  { "emitter": "Alpha", "receiver": "Tau", "workType": "Packaging" },
  { "emitter": "Upsilon", "receiver": "Alpha", "workType": "Seed" },
  { "emitter": "Phi", "receiver": "Alpha", "workType": "Storage" },
  { "emitter": "Chi", "receiver": "Alpha", "workType": "Transport" },
  { "emitter": "Alpha", "receiver": "Psi", "workType": "Packaging" },
  { "emitter": "Omega", "receiver": "Alpha", "workType": "Seed" },
  { "emitter": "Alpha", "receiver": "Alpha", "workType": "Storage" },
  { "emitter": "Beta", "receiver": "Alpha", "workType": "Transport" },
  { "emitter": "Gamma", "receiver": "Alpha", "workType": "Packaging" },
  { "emitter": "Alpha", "receiver": "Delta", "workType": "Seed" },
  { "emitter": "Zeta", "receiver": "Alpha", "workType": "Storage" },
  { "emitter": "Alpha", "receiver": "Lambda", "workType": "Transport" },
  { "emitter": "Pi", "receiver": "Alpha", "workType": "Packaging" },
  { "emitter": "Nu", "receiver": "Alpha", "workType": "Seed" }
];


