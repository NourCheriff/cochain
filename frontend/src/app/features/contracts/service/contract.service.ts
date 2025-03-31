import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { BaseHttpService } from 'src/app/core/services/api.service';
import { PaginationResponse } from 'src/app/core/utilities/pagination-response';
import { SupplyChainPartner } from 'src/models/company-entities/supply-chain-partner.model';
import { Contract } from 'src/models/documents/contract.model';
import { SupplyChainPartnerCertificate } from 'src/models/documents/supply-chain-partner-certificate.model';
import { ProductLifeCycleCategory } from 'src/models/product/product-life-cycle-category.model';

@Injectable({
  providedIn: 'root'
})
export class ContractService {

  constructor(private apiService: BaseHttpService) { }

  addContract(conctact: Contract): Observable<Contract>{
    return this.apiService.add('api/Document/AddContractDocument', conctact)
  }

  getContracts(userId: string, type: string, pageSize: string, pageNumber: string): Observable<PaginationResponse<Contract>> {

    const endpoint = type === "received_contracts"
    ? "api/Document/ReceivedContracts"
    : "api/Document/EmittedContracts";

    const params: any = { pageNumber, pageSize };

    if (type === "received_contracts") {
      params.scpId = userId;
    } else {
      params.userId = userId;
    }

    return this.apiService.getAll(endpoint, { params }).pipe(
      map((response: any) => {
        return {
          items: response[0]?.items || [],
          totalSize: response[0]?.totalSize || 0,
        } as PaginationResponse<Contract>;
      })
    );
  }

  getAllProductLifeCycleCategories(): Observable<ProductLifeCycleCategory[]>{
    return this.apiService.getAll('api/ProductLifeCycle')
  }

  getAllSupplyChainPartner(): Observable<PaginationResponse<SupplyChainPartner>> {
    return this.apiService.getAll('api/SupplyChainPartner').pipe(
      map((response: any) => {
        const paginationResponse: PaginationResponse<SupplyChainPartner> = {
          items: response[0].items || [],
          totalSize: response[0].totalSize || 0,
        }
        return paginationResponse;
      })
    );
  }

  deleteContract(id: string, type: string): Observable<Contract>{
    return this.apiService.deleteDocument('api/Document', id, type)
  }
}
