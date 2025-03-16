import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { BaseHttpService } from 'src/app/core/services/api.service';
import { User } from 'src/models/auth/user.model';
import { CompanyType } from 'src/types/company.enum';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private apiService: BaseHttpService) {}

  getUsersByCompanyId(companyId: string, companyType: CompanyType): Observable<User[]> {
    let params = { companyType: companyType };
    return this.apiService.getAll(`api/Users/company/${companyId}`, params);
  }

  addUser(newUser: User){
    const body = {
      "id": null,
      "firstName": newUser.firstName,
      "lastName": newUser.lastName,
      "userName": newUser.userName,
      "phone": newUser.phone,
      ...(newUser.certificationAuthorityId ? { certificationAuthorityId: newUser.certificationAuthorityId } : {}),
      ...(newUser.supplyChainPartnerId ? { supplyChainPartnerId: newUser.supplyChainPartnerId } : {}),
      "role": "Admin",
    }

    return this.apiService.add('api/Users/UpdateUser', body)
  }
}
