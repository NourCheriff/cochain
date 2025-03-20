import { Component, OnInit, ViewChild, inject, signal } from '@angular/core';
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
import { ProductInfo } from 'src/models/product/product-info.model';
import { ProductService } from '../../services/product.service';
import {MatTable} from '@angular/material/table'
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
export class ProductsComponent implements OnInit {

  constructor(private productService: ProductService){}

  readonly dialog = inject(MatDialog);
  private _liveAnnouncer = inject(LiveAnnouncer);

  @ViewChild(MatTable) table!: MatTable<any>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  isChecked = false;
  newProduct!: ProductInfo;
  productInfo: ProductInfo[] = [];
  dataSource: any;
  displayedColumns: string[] = ['name', 'category', 'expiration_date', 'producer', 'sustainability_certificate', 'action'];

  user: User = {
    "supplyChainPartner": "Prova company", // here goes the scp of the logged user
    "role": "Admin"
  };


  ngOnInit(): void {
    this.getAllProductInfo()
  }

  sendProduct(product: ProductInfo) {
    this.productService.passProduct(product);
  }

  getAllProductInfo(){
    this.productService.getAllProductInfo().subscribe({
      next: (response) => {
        this.productInfo = response
        this.dataSource = new MatTableDataSource<ProductInfo>(this.productInfo);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      },
      error: (error) => console.log(error)
    })
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
    const BACKUP_DATA = this.productInfo;
    let SELECTED_DATA: ProductInfo[] = [];

    SELECTED_DATA = !this.isChecked ? BACKUP_DATA :
    BACKUP_DATA.filter(item => item.supplyChainPartner?.name === this.user.supplyChainPartner)

    this.dataSource = new MatTableDataSource<ProductInfo>(SELECTED_DATA);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  addProduct() {
    let currentDialog = this.dialog.open(ProductDialogComponent);

    currentDialog.afterClosed().subscribe(result => {
      if (result !== undefined) {
        let updatedData = [result.newProduct, ...this.dataSource.data];
        this.dataSource.data = updatedData;
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        this.table.renderRows();
      }
    });
  }
}
export interface User {
  supplyChainPartner: string;
  role: string;
}
