import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Observable } from 'rxjs/internal/Observable';
import { BaseHttpService } from 'src/app/core/services/api.service';
import { CertificationAuthority } from 'src/models/company-entities/certification-authority.model';
import { SupplyChainPartner } from 'src/models/company-entities/supply-chain-partner.model';
import { CompanyType } from 'src/types/company.enum';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {

  constructor(private apiService: BaseHttpService) {}

  private companyId = new BehaviorSubject<string | null>(null);
  selectedCompanyId = this.companyId.asObservable();

  private companyType = new BehaviorSubject<CompanyType | null>(null);
  selectedCompanyType = this.companyType.asObservable();

  passCompany(companyId: string, companyType: CompanyType) {
    this.companyId.next(companyId);
    this.companyType.next(companyType);
  }

  getCurrentCompanyId(): string | null {
    return this.companyId.getValue();
  }

  getCurrentCompanyType(): CompanyType | null {
    return this.companyType.getValue();
  }

  getAllCertificationAuthorities(): Observable<CertificationAuthority[]>{
    return this.apiService.getAll('api/CertificationAuthority')
  }

  getAllSupplyChainPartners(): Observable<SupplyChainPartner[]>{
    return this.apiService.getAll('api/SupplyChainPartner')
  }

  addSupplyChainPartner(newSCP: SupplyChainPartner){
    return this.apiService.add('api/SupplyChainPartner/addSCP', {})
  }
}
