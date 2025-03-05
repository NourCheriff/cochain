import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import {MatPaginator, MatPaginatorModule} from '@angular/material/paginator';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatSelectModule} from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-scp-products',
  imports: [MatInputModule,MatTableModule, MatPaginatorModule, MatButtonModule, MatIconModule, MatFormFieldModule, MatSelectModule],
  templateUrl: './scp-products.component.html',
  styleUrl: './scp-products.component.css'
})
export class ScpProductsComponent implements AfterViewInit {
  displayedColumns: string[] = ['name', 'category', 'expirationDate', 'attachments'];
  dataSource = new MatTableDataSource<SCPProducts>(scpProducts);
  certificateId: number | null = null;

  selected = 'name';

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  orderBy() {
    const BACKUP_DATA = scpProducts;

    var SELECTED_DATA: SCPProducts[] = [];

    switch (this.selected) {
      case 'name':
        SELECTED_DATA = [...BACKUP_DATA].sort((a, b) => a.name.localeCompare(b.name));
        break;

      case 'category':
        SELECTED_DATA = [...BACKUP_DATA].sort((a, b) => a.category.localeCompare(b.category));
        break;

      case 'expirationDate':
        SELECTED_DATA = [...BACKUP_DATA].sort((a, b) => {
        // Funzione per convertire una data nel formato 'dd-mm-yyyy' in un oggetto Date
        const convertToDate = (dateString: string): Date => {
            const [day, month, year] = dateString.split('-').map(num => parseInt(num, 10));
            return new Date(year, month - 1, day); // Anno, mese (0-based), giorno
        };

        const dateA = convertToDate(a.expirationDate);
        const dateB = convertToDate(b.expirationDate);

        return dateA.getTime() - dateB.getTime(); // Ordina dalla data più vicina a quella più lontana
    });
    break;


    }

    this.dataSource = new MatTableDataSource<SCPProducts>(SELECTED_DATA);
    this.dataSource.paginator = this.paginator;
  }

}

export interface SCPProducts {
  name: string;
  category: string;
  expirationDate: string;
}


const scpProducts: SCPProducts[] = [
  {
    'name':'ProductA',
    'category': 'CategoryA',
    'expirationDate':'17-04-2025'
  },
  {
    'name':'ProductB',
    'category': 'CategoryC',
    'expirationDate':'17-04-2025'
  },
  {
    'name':'ProductB',
    'category': 'CategoryA',
    'expirationDate':'15-04-2025'
  },
  {
    'name':'ProductC',
    'category': 'CategoryC',
    'expirationDate':'17-03-2025'
  },
  {
    'name':'ProductD',
    'category': 'CategoryD',
    'expirationDate':'01-08-2025'
  },
  {
    'name':'ProductA',
    'category': 'CategoryB',
    'expirationDate':'03-04-2025'
  },

]
