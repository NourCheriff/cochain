import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { Observable } from 'rxjs/internal/Observable';
import { BaseHttpService } from 'src/app/core/services/api.service';
import { SupplyChainPartner } from 'src/models/company-entities/supply-chain-partner.model';
import { ProductCategory } from 'src/models/product/product-category.model';
import { ProductInfo } from 'src/models/product/product-info.model';
import { Product } from 'src/models/product/product.model';
import { ProductLifeCycleCategory } from 'src/models/product/product-life-cycle-category.model';
import { PaginationResponse } from 'src/app/core/utilities/paginationResponse';

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

  // addProductLifeCycle(productLifeCycle: ProductLifeCycle): Observable<ProductLifeCycle>{
  //   return this.apiService.add("api/ProductLifeCycle", productLifeCycle)
  // }

  getProductCategories(): Observable<ProductCategory[]> {
    return this.apiService.getAll('api/Product/categories');
  }

  getProductInfo(productId: string): Observable<ProductInfo> {
    return this.apiService.getById('api/Product', productId)
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

  getAllSupplyChainPartner(): Observable<SupplyChainPartner[]> {
    return this.apiService.getAll('api/SupplyChainPartner')
  }

}
