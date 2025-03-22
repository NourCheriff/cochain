import { Component, OnInit, ViewChild, inject, signal } from '@angular/core';
import { MatPaginator, MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule, MatTable } from '@angular/material/table';
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
import { AuthService } from 'src/app/core/services/auth.service';
import { DefaultPagination } from 'src/app/core/utilities/pagination-response';

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

  private authService = inject(AuthService)

  readonly dialog = inject(MatDialog);
  private _liveAnnouncer = inject(LiveAnnouncer);


  @ViewChild(MatTable) table!: MatTable<any>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  isChecked = false;
  newProduct!: ProductInfo;
  productInfo: ProductInfo[] = [];
  myProductInfo: ProductInfo[] = [];
  displayedColumns: string[] = ['name', 'category', 'expiration_date', 'producer', 'action'];
  dataSource = new MatTableDataSource<ProductInfo>([]);
  totalRecords = 0;

  ngOnInit(): void {
    this.getAllProductInfo()
    this.dataSource.paginator = this.paginator;
  }

  sendProduct(product: ProductInfo) {
    this.productService.passProduct(product);
  }

  getAllProductInfo(pageSize: number = DefaultPagination.defaultPageSize, pageNumber: number = DefaultPagination.defaultPageNumber){
    this.productService.getAllProductInfo(pageSize.toString(),pageNumber.toString()).subscribe({
      next: (response) => {
        this.productInfo = response.items!;
        this.dataSource = new MatTableDataSource<ProductInfo>(this.productInfo);
        this.totalRecords = response.totalSize;
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


  updateTable(pageSize: number = DefaultPagination.defaultPageSize, pageNumber: number = DefaultPagination.defaultPageNumber) {
    let data: ProductInfo[] = []
    if(this.isChecked){
        // this.productService.getMyProductsInfo(, pageSize.toString(), pageNumber.toString()).subscribe({
        //   next: (response) => {
        //     console.log(response.items)
        //     this.myProductInfo = response.items!;
        //     data = this.myProductInfo;
        //     this.totalRecords = response.totalSize;
        //   },
        //   error: (error) => console.log(error)
        // })
    }else{
      data = this.productInfo
    }

    this.dataSource = new MatTableDataSource<ProductInfo>(data);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  onPageChange(event: PageEvent){
    this.isChecked ? this.updateTable(event.pageSize, event.pageIndex) :
    this.getAllProductInfo(event.pageSize, event.pageIndex)
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
