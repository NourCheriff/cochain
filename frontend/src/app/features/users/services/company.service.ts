import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { BaseHttpService } from 'src/app/core/services/api.service';
import { User } from 'src/models/auth/user.model';
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

  getAllCertificationAuthorities(): Observable<CertificationAuthority[]> {
    return this.apiService.getAll('api/CertificationAuthority')
  }

  getAllSupplyChainPartners(): Observable<SupplyChainPartner[]> {
    return this.apiService.getAll('api/SupplyChainPartner')
  }

  getSupplyChainPartnerTypes(): Observable<SupplyChainPartnerType[]>{
    return this.apiService.getAll('api/SupplyChainPartner/categories')
  }
}
