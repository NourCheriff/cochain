import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { BaseHttpService } from 'src/app/core/services/api.service';
import { ProductInfo } from 'src/models/product/product-info.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private apiService: BaseHttpService) {}

  addProduct(productInfo: ProductInfo): Observable<ProductInfo>{
      return this.apiService.add("/api/Product", productInfo)
  }
}
