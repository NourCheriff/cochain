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

  readonly dialog = inject(MatDialog);

  @ViewChild(MatTable) table!: MatTable<any>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  displayedColumns: string[] = ['workType', 'emissions', 'workDate', 'attachments'];
  userRole: string = "SCP";
  productInfo!: ProductInfo;
  ingredients:ProductInfo[] = [];
  lifeCycleSource: any;
  lifeCyclesList: ProductLifeCycle[] = [];

  ngOnInit(): void {
    this.productService.selectedProduct.subscribe((data) => this.loadDetails(data));

    if(this.productInfo == null){
      let productId = this.route.snapshot.paramMap.get('id')!;
      this.productService.getProductInfoById(productId).subscribe({
        next: (response) => this.loadDetails(response),
        error: (error) => console.log(error)
      })
    }
  }

  loadDetails(data: ProductInfo){
    this.productInfo = data;

    this.lifeCyclesList = this.productInfo.productLifeCycles!;
    this.lifeCycleSource = new MatTableDataSource<ProductLifeCycle>(this.lifeCyclesList);
    this.lifeCycleSource.paginator = this.paginator;

    this.loadProductIngredients();
  }

  sendProduct(product: ProductInfo) {
    this.ingredients = [];
    this.productService.passProduct(product);
  }

  addWork(){
    let currentDialog = this.dialog.open(NewWorkDialogComponent,{data: {product: this.productInfo}});

    currentDialog.afterClosed().subscribe(result => {
      if (result !== undefined) {
        let updatedData = [result.newWork, ...this.lifeCycleSource.data];
        this.lifeCycleSource.data = updatedData;
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
    const ingredientIds: string[] = this.productInfo.ingredients!.map(ingredient => ingredient.ingredientId);

    if(this.productInfo?.ingredients && this.productInfo.ingredients.length > 0){
        this.productService.getProductsInfoByIds(ingredientIds).subscribe({
          next: (response) => this.ingredients = response,
          error: (error) => console.log(error)
        })
    }
  }

  isAdmin(): boolean{
    return this.userRole === "Admin";
  }
}
