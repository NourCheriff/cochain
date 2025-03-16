import { inject, Injectable } from '@angular/core';
import { BaseHttpService } from 'src/app/core/services/api.service';

@Injectable({
  providedIn: 'root'
})
export class ScpProductsService {

  private apiServie = inject(BaseHttpService)

  getScpProducts(scpId: string){
    return this.apiServie.getById('api/Product/scp', scpId)
  }

}
