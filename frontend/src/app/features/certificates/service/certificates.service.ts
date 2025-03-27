import { inject, Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { BaseHttpService } from 'src/app/core/services/api.service';
import { PaginationResponse } from 'src/app/core/utilities/pagination-response';
import { SupplyChainPartner } from 'src/models/company-entities/supply-chain-partner.model';
import { SupplyChainPartnerCertificate } from 'src/models/documents/supply-chain-partner-certificate.model';
import { ProductInfo } from 'src/models/product/product-info.model';

@Injectable({
  providedIn: 'root'
})
export class CertificatesService {

  private apiService = inject(BaseHttpService)

  getScpProducts(scpId: string, pageSize: string, pageNumber: string): Observable<PaginationResponse<ProductInfo>>{
    return this.apiService.getAll('api/Product/scp', { params: { pageNumber, pageSize}, id: scpId }).pipe(
      map((response: any) => {
        const paginationResponse: PaginationResponse<ProductInfo> = {
          items: response[0].items || [],
          totalSize: response[0].totalSize || 0
        };
        return paginationResponse;
      })
    );
  }

  getSupplyChainPartners(pageSize: string, pageNumber: string): Observable<PaginationResponse<SupplyChainPartner>> {
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

  uploadCertificate(certificate: SupplyChainPartnerCertificate): Observable<SupplyChainPartnerCertificate>{
    return this.apiService.add('api/Document/AddCertificationDocument', certificate)
  }

  deleteCertificate(id: string): Observable<SupplyChainPartnerCertificate>{
    return this.apiService.delete('api/Document/RemoveCertificate', id)
  }

}
