import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { Observable } from 'rxjs/internal/Observable';
import { BaseHttpService } from 'src/app/core/services/api.service';
import { PaginationResponse } from 'src/app/core/utilities/paginationResponse';
import { CertificationAuthority } from 'src/models/company-entities/certification-authority.model';
import { SupplyChainPartnerType } from 'src/models/company-entities/supply-chain-partner-type.model';
import { SupplyChainPartner } from 'src/models/company-entities/supply-chain-partner.model';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {

  constructor(private apiService: BaseHttpService) {}

  addCertificationAuthority(newCA: CertificationAuthority): Observable<CertificationAuthority> {
    return this.apiService.add('api/CertificationAuthority/addCA', newCA)
  }

  addSupplyChainPartner(newSCP: SupplyChainPartner): Observable<SupplyChainPartner> {
    return this.apiService.add('api/SupplyChainPartner/addSCP', newSCP)
  }

  getAllCertificationAuthorities(pageSize: string, pageNumber: string): Observable<PaginationResponse<CertificationAuthority>> {
    return this.apiService.getAll('api/CertificationAuthority', { params: { pageNumber, pageSize} }).pipe(
      map((response: any) => {
        const paginationResponse: PaginationResponse<SupplyChainPartner> = {
          items: response[0].items || [],
          totalSize: response[0].totalSize || 0
        };
        return paginationResponse;
      })
    );
  }

  getAllSupplyChainPartners(pageSize: string, pageNumber: string): Observable<PaginationResponse<SupplyChainPartner>> {
    return this.apiService.getAll('api/SupplyChainPartner', { params: { pageNumber, pageSize} }).pipe(
      map((response: any) => {
        const paginationResponse: PaginationResponse<SupplyChainPartner> = {
          items: response[0].items || [],
          totalSize: response[0].totalSize || 0
        };
        return paginationResponse;
      })
    );
  }

  getSupplyChainPartnerTypes(): Observable<SupplyChainPartnerType[]>{
    return this.apiService.getAll('api/SupplyChainPartner/categories')
  }
}
