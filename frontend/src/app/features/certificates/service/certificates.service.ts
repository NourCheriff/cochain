import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseHttpService } from 'src/app/core/services/api.service';
import { SupplyChainPartner } from 'src/models/company-entities/supply-chain-partner.model';
import { SupplyChainPartnerCertificate } from 'src/models/documents/supply-chain-partner-certificate.model';
import { ProductInfo } from 'src/models/product/product-info.model';

@Injectable({
  providedIn: 'root'
})
export class CertificatesService {

  private apiServie = inject(BaseHttpService)

  getScpProducts(scpId: string, pageSize: string, pageNumber: string): Observable<ProductInfo[]>{
    return this.apiServie.getAll('api/Product/scp', { params: { pageNumber, pageSize}, id: scpId })
  }

  getSupplyChainPartners(): Observable<SupplyChainPartner[]>{
    return this.apiServie.getAll('api/SupplyChainPartner')
  }

  uploadCertificate(certificate: SupplyChainPartnerCertificate): Observable<SupplyChainPartnerCertificate>{
    return this.apiServie.add('api/Document/AddCertificationDocument', certificate)
  }

  deleteCertificate(id: string): Observable<SupplyChainPartnerCertificate>{
    return this.apiServie.delete('api/Document/RemoveCertificate', id)
  }

}
