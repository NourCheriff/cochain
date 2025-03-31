import { inject, Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { BaseHttpService } from 'src/app/core/services/api.service';
import { PaginationResponse } from 'src/app/core/utilities/pagination-response';
import { SupplyChainPartner } from 'src/models/company-entities/supply-chain-partner.model';
import { ProductDocument } from 'src/models/documents/product-document.model';
import { SupplyChainPartnerCertificate } from 'src/models/documents/supply-chain-partner-certificate.model';
import { ProductInfo } from 'src/models/product/product-info.model';
import { DocumentType } from 'src/types/document.enum';

@Injectable({
  providedIn: 'root'
})
export class CertificatesService {

  private apiService = inject(BaseHttpService)

  getScpProducts(scpId: string, pageSize: string, pageNumber: string): Observable<PaginationResponse<ProductInfo>>{
    return this.apiService.getAll(`api/Product/scp/${scpId}`, { params: { pageNumber, pageSize} }).pipe(
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

  uploadSustainabilityCertificate(certificate: SupplyChainPartnerCertificate): Observable<SupplyChainPartnerCertificate>{
    return this.apiService.add('api/Document/AddCertificationDocument', certificate);
  }

  uploadQualityCertificate(certificate: ProductDocument): Observable<ProductDocument>{
    return this.apiService.add('api/Document/AddQualityDocument', certificate);
  }

  deleteSustainabilityCertificate(id: string, fileName: string): Observable<SupplyChainPartnerCertificate>{
    return this.apiService.deleteDocument('api/Document/RemoveCertificate', id, fileName);
  }

  deleteQualityCertificate(id: string, fileName: string): Observable<ProductDocument>{
    return this.apiService.deleteDocument('api/Document', id, fileName, DocumentType.Quality);
  }
}
