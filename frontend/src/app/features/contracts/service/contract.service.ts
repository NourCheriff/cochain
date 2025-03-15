import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseHttpService } from 'src/app/core/services/api.service';
import { SupplyChainPartner } from 'src/models/company-entities/supply-chain-partner.model';
import { Contract } from 'src/models/documents/contract.model';
import { ProductLifeCycleCategory } from 'src/models/product/product-life-cycle-category.model';

@Injectable({
  providedIn: 'root'
})
export class ContractService {

  constructor(private apiService: BaseHttpService) { }

  addContract(conctact: Contract): Observable<Contract>{
    return this.apiService.add('api/Document/AddContractDocument', conctact)
  }

  getAllContracts(): Observable<Contract[]>{
    return this.apiService.getAll('api/Document/')
  }

  getAllProductLifeCycleCategories(): Observable<ProductLifeCycleCategory[]>{
    return this.apiService.getAll('api/ProductLifeCycle')
  }

  getAllSupplyChainPartner(): Observable<SupplyChainPartner[]>{
    return this.apiService.getAll('api/SupplyChainPartner/categories')
  }
}
