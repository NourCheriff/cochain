
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Observable } from 'rxjs/internal/Observable';
import { BaseHttpService } from 'src/app/core/services/api.service';
import { CertificationAuthority } from 'src/models/company-entities/certification-authority.model';
import { SupplyChainPartner } from 'src/models/company-entities/supply-chain-partner.model';
import { User } from 'src/models/auth/user.model';
@Injectable({
  providedIn: 'root'
})
export class CompanyService {

  private company = new BehaviorSubject<any>(null);
  selectedCompany = this.company.asObservable();

  constructor(private apiService: BaseHttpService) {}

  passCompany(companyId: string) {
    this.company.next(companyId);
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

  getUsersByCompanyId(companyId: string): Observable<User[]>{
    return this.apiService.getAll('api/Users/company/'+ companyId)
  }
}
