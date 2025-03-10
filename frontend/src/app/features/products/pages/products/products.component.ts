import { AfterViewInit, Component, ViewChild, inject } from '@angular/core';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ProductDialogComponent } from '../../components/product-dialog/product-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { FormsModule }   from '@angular/forms';
import { MatSort, Sort, MatSortModule } from '@angular/material/sort';
import { LiveAnnouncer } from '@angular/cdk/a11y';
import { RouterLink } from '@angular/router';
import { MatInputModule } from '@angular/material/input';
@Component({
  selector: 'app-products',
  imports: [MatTableModule,
            MatPaginatorModule,
            MatButtonModule,
            MatIconModule,
            MatFormFieldModule,
            MatSelectModule,
            MatSlideToggleModule,
            FormsModule,
            MatSortModule,
            MatInputModule,
            RouterLink,
          ],
  templateUrl: './products.component.html',
  styleUrl: './products.component.css'

})
export class ProductsComponent implements AfterViewInit {

  user: User = {
    "supplyChainPartner": "Alpha", // qui ci va il supply chain partner, quando l'utente effettua il login
    "role": "Admin"
  };

  readonly dialog = inject(MatDialog);

  isChecked = false;

  private _liveAnnouncer = inject(LiveAnnouncer);

  displayedColumns: string[] = ['name', 'category', 'expiration_date', 'producer', 'sustainability_certificate', 'action'];

  dataSource = new MatTableDataSource<ProductElement>(PRODUCT_DATA);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  announceSortChange(sortState: Sort) {
    if (sortState.direction) {
      this._liveAnnouncer.announce(`Sorted ${sortState.direction}ending`);
    } else {
      this._liveAnnouncer.announce('Sorting cleared');
    }
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
}

  updateTable() {

    const BACKUP_DATA = PRODUCT_DATA;

    var SELECTED_DATA: ProductElement[] = [];

    switch(this.isChecked){
      case true:
        SELECTED_DATA = BACKUP_DATA.filter(item => item.producer == this.user.supplyChainPartner);
      break;

      case false:
        SELECTED_DATA = BACKUP_DATA;
      break;
    }

    this.dataSource = new MatTableDataSource<ProductElement>(SELECTED_DATA);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  addProduct() {
    this.dialog.open(ProductDialogComponent);
  }
}

export interface User {
  supplyChainPartner: string;
  role: string;
}

export interface ProductElement {
  id: string;
  name: string;
  category: string;
  expiration_date: string;
  producer: string;
  sustainability_certificate: string;
  action: string;
}

const PRODUCT_DATA: ProductElement[] = [
  { id: '1', name: "ProductName", category: "Product category", expiration_date: "17-04-2025", producer: "Alpha", sustainability_certificate: "link", action: "link_dettagli" },
  { id: '2', name: "ProductName", category: "Product category", expiration_date: "15-04-2025", producer: "Beta", sustainability_certificate: "", action: "link_dettagli" },
  { id: '3', name: "ProductName", category: "Product category", expiration_date: "07-04-2025", producer: "Supply Chain Partner", sustainability_certificate: "link", action: "link_dettagli" },
  { id: '4', name: "ProductName", category: "Product category", expiration_date: "07-04-2025", producer: "Alpha", sustainability_certificate: "link", action: "link_dettagli" },
  { id: '5', name: "ProductName", category: "Product category", expiration_date: "03-04-2025", producer: "Supply Chain Partner", sustainability_certificate: "", action: "link_dettagli" },
  { id: '6', name: "Farina", category: "Product category", expiration_date: "01-04-2025", producer: "Supply Chain Partner", sustainability_certificate: "", action: "link_dettagli" }
];

