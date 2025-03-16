import { inject, Injectable } from '@angular/core';
import { BaseHttpService } from 'src/app/core/services/api.service';
import { SupplyChainPartnerCertificate } from 'src/models/documents/supply-chain-partner-certificate.model';

@Injectable({
  providedIn: 'root'
})
export class CertificatesService {

  private apiServie = inject(BaseHttpService)

  getScpProducts(scpId: string){
    return this.apiServie.getById('api/Product/scp', scpId)
  }

  uploadCertificate(certificate: SupplyChainPartnerCertificate){
    return this.apiServie.add('api/Document/AddCertificationDocument', certificate)
  }

}
