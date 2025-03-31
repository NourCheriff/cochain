import { Component, ViewChild, inject, OnInit } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatButtonModule } from '@angular/material/button';
import { MatChipsModule } from '@angular/material/chips';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { NewWorkDialogComponent } from '../../components/new-work-dialog/new-work-dialog.component';
import { ProductService } from '../../services/product.service';
import { ProductInfo } from 'src/models/product/product-info.model';
import { ProductLifeCycle } from 'src/models/product/product-life-cycle.model';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { EditProductDialogComponent } from '../../components/edit-product-dialog/edit-product-dialog.component';
import { MatTable } from '@angular/material/table'
import { ActivatedRoute } from '@angular/router';
import { Role } from 'src/types/roles.enum';
import { AuthService } from 'src/app/core/services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/models/auth/user.model';
import { tap, switchMap, of } from 'rxjs';

@Component({
  selector: 'app-product-details',
  imports: [
    MatTableModule,
    MatPaginatorModule,
    MatCardModule,
    MatButtonModule,
    MatDividerModule,
    MatIconModule,
    MatChipsModule,
    CommonModule,
    RouterLink,
  ],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.css',
})
export class ProductDetailsComponent implements OnInit {

  constructor(private route: ActivatedRoute, private productService: ProductService){}
  private authService = inject(AuthService)
  private toastrService =  inject(ToastrService)

  readonly dialog = inject(MatDialog);

  @ViewChild(MatTable) table!: MatTable<any>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  displayedColumns: string[] = ['workType', 'emissions', 'workDate', 'attachments'];
  productInfo: ProductInfo = {
    productId: '',
    expirationDate: ''
  };
  ingredients:ProductInfo[] = [];
  lifeCycleSource = new MatTableDataSource<ProductLifeCycle>([]);
  lifeCyclesList: ProductLifeCycle[] = [];
  currentUser!: User;

  ngOnInit(): void {
    this.authService.getUser().pipe(
      tap(response => this.currentUser = response),
      switchMap(() => this.productService.selectedProduct.pipe(
        tap(data => this.loadDetails(data))
      )),
      switchMap(() => {
        if (this.productInfo == null) {
          let productId = this.route.snapshot.paramMap.get('id')!;
          return this.productService.getProductInfoById(productId).pipe(
            tap(response => this.loadDetails(response))
          );
        }
        return of(null);
      })
    ).subscribe({
      error: (error) => console.error(error)
    });
  }

  loadDetails(data: ProductInfo){
    this.productInfo = data;

    if (this.lifeCycleSource != null) {
      this.lifeCyclesList = this.productInfo.productLifeCycles ? this.productInfo.productLifeCycles : [];
      this.lifeCycleSource.data = this.lifeCyclesList;
      this.lifeCycleSource.paginator = this.paginator;
    }

    this.loadProductIngredients();
  }

  sendProduct(product: ProductInfo) {
    this.productService.passProduct(product);
  }

  addWork(){
    let currentDialog = this.dialog.open(NewWorkDialogComponent,{data: {product: this.productInfo}});

    currentDialog.afterClosed().subscribe(result => {
      if (result !== undefined) {
        this.lifeCycleSource.data = (this.lifeCycleSource != null) ? [result.newWork, ...this.lifeCycleSource.data] : result.newWork;
        this.lifeCycleSource.paginator = this.paginator;
        this.table.renderRows();
      }
    });
  }

  modifyProduct(){
    let currentDialog = this.dialog.open(EditProductDialogComponent, {data: {product: this.productInfo, ingredients: this.ingredients}});

    currentDialog.afterClosed().subscribe(result => {
      if (result !== undefined) {
        this.productInfo = result.modifiedProduct;
        this.loadProductIngredients();
      }
    });
  }

  loadProductIngredients(){
    this.ingredients = [];

    if(this.productInfo?.ingredients && this.productInfo.ingredients.length > 0){
        this.productService.getIngredientsByProductInfoId(this.productInfo.id!).subscribe({
          next: (response) => this.ingredients = response,
          error: (error) => console.log(error)
        })
    }
  }

  deleteDocument(id: string, documentType: string) {
    this.productService.deleteDocument(id, documentType).subscribe({
      next: (response) => {
        this.toastrService.info(`Removed Origin certificate`, 'Info')
        this.productInfo.productDocuments = this.productInfo.productDocuments?.filter(doc => doc.id !== id);
      },
      error: (error) => console.error(error)
    })
  }

  isAdmin(): boolean {
    return this.authService.userRoles!.includes(Role.SysAdmin);
  }

  isMyProduct(): boolean {
    if(this.authService.userRoles!.includes(Role.SysAdmin)) {
      return true;
    }

    const requiredRoles = [Role.SCPTransformator, Role.SCPRawMaterial];

    return this.authService.userRoles!.some(role => requiredRoles.includes(role)) &&
            this.currentUser.supplyChainPartnerId === this.productInfo.supplyChainPartnerId;
  }
}
