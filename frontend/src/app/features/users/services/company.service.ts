import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { BaseHttpService } from 'src/app/core/services/api.service';
import { CertificationAuthority } from 'src/models/company-entities/certification-authority.model';
import { SupplyChainPartner } from 'src/models/company-entities/supply-chain-partner.model';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {

  constructor(private apiService: BaseHttpService) {}

  getAllCertificationAuthorities(): Observable<CertificationAuthority[]> {
    return this.apiService.getAll('api/CertificationAuthority')
  }

  getAllSupplyChainPartners(): Observable<SupplyChainPartner[]> {
    return this.apiService.getAll('api/SupplyChainPartner')
  }

  addSupplyChainPartner(newSCP: SupplyChainPartner) {
    return this.apiService.add('api/SupplyChainPartner/addSCP', {})
  }
}
