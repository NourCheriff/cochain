import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { Observable } from 'rxjs/internal/Observable';
import { BaseHttpService } from 'src/app/core/services/api.service';
import { ProductCategory } from 'src/models/product/product-category.model';
import { ProductInfo } from 'src/models/product/product-info.model';
import { Product } from 'src/models/product/product.model';
import { ProductLifeCycleCategory } from 'src/models/product/product-life-cycle-category.model';
import { ProductLifeCycle } from 'src/models/product/product-life-cycle.model';
import { ProductDocument } from 'src/models/documents/product-document.model';
import { ProductLifeCycleDocument } from 'src/models/documents/product-life-cycle-document.model';
import { PaginationResponse } from 'src/app/core/utilities/pagination-response';
import { User } from 'src/models/auth/user.model';


@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private productInfo = new BehaviorSubject<any>(null);
  selectedProduct = this.productInfo.asObservable();

  constructor(private apiService: BaseHttpService) {}

  passProduct(product: ProductInfo) {
    this.productInfo.next(product);
  }

  addProductInfo(productInfo: ProductInfo): Observable<ProductInfo>{
    return this.apiService.add("api/Product", productInfo)
  }

  updateProductInfo(productInfo: ProductInfo): Observable<ProductInfo>{
    return this.apiService.add("api/Product/UpdateProduct", productInfo)
  }

  addProductLifeCycleGeneric(newWork: ProductLifeCycle): Observable<ProductLifeCycle>{
    return this.apiService.add("api/ProductLifeCycle/LifeCycle/AddGeneric", newWork)
  }

  addProductLifeCycleTransport(newWork: ProductLifeCycle): Observable<ProductLifeCycle>{
    return this.apiService.add("api/ProductLifeCycle/LifeCycle/AddTransport", newWork)
  }

  getProductCategories(): Observable<ProductCategory[]> {
    return this.apiService.getAll('api/Product/categories');
  }

  getProductInfo(productId: string): Observable<ProductInfo> {
    return this.apiService.getById('api/Product', productId)
  }

  getMyProductsInfo(userId: string, pageSize: string, pageNumber: string): Observable<PaginationResponse<ProductInfo>> {
    return this.apiService.getAll(`api/Product/scp/${userId}`, { params: { pageNumber, pageSize} }).pipe(
      map((response: any) => {
        const paginationResponse: PaginationResponse<ProductInfo> = {
          items: response[0].items || [],
          totalSize: response[0].totalSize || 0
        };
        return paginationResponse;
      })
    );
  }

  getAllProductInfo(pageSize?: string, pageNumber?: string): Observable<PaginationResponse<ProductInfo>> {
    return this.apiService.getAll('api/Product/allproducts', { params: { pageNumber, pageSize} } ).pipe(
      map((response: any) => {
        const paginationResponse: PaginationResponse<ProductInfo> = {
          items: response[0].items || [],
          totalSize: response[0].totalSize || 0
        };
        return paginationResponse;
      })
    );
  }

  getAllProductLifeCycleCategories(): Observable<ProductLifeCycleCategory[]>{
    return this.apiService.getAll('api/ProductLifeCycle')
  }

  getAllGenericProducts(categoryId: string): Observable<Product[]>{
    return this.apiService.getAll('api/Product/generic', { id :categoryId })
  }

  getProductInfoById(productId: string): Observable<ProductInfo> {
    return this.apiService.getById('api/Product', productId)
  }

  getIngredientsByProductInfoId(productId: string): Observable<ProductInfo[]> {
    return this.apiService.getAll('api/Product/ingredients', { id :productId });
  }

  uploadOriginDocument(originDocument: ProductDocument): Observable<ProductDocument>{
    return this.apiService.add('api/Document/AddOriginDocument', originDocument);
  }

  deleteDocument(documentId: string, documentType: string): Observable<ProductDocument>{
    return this.apiService.delete('api/Document', documentId, documentType);
  }

  uploadLifeCycleDocument(lifeCycleDocument: ProductLifeCycleDocument): Observable<ProductLifeCycleDocument>{
    let url = `api/Document/`;

    if(lifeCycleDocument.type == "invoice"){
      url += `AddInvoicesDocument`;
    }
    else{
      url += `AddTransportDocument`;
    }

    return this.apiService.add(url, lifeCycleDocument);
  }
}
