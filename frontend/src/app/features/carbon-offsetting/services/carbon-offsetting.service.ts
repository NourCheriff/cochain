import { Injectable, inject } from '@angular/core';
import { BaseHttpService } from 'src/app/core/services/api.service';
import { Observable, map } from 'rxjs';
import { PaginationResponse } from 'src/app/core/utilities/pagination-response';
import { CarbonOffsettingAction } from 'src/models/carbon-offset/carbon-offsetting-actions.model';

@Injectable({
  providedIn: 'root'
})
export class CarbonOffsettingService {

  private apiService = inject(BaseHttpService)

  public getCarbonOffsettingActions(pageSize: string, pageNumber: string): Observable<PaginationResponse<CarbonOffsettingAction>> {
    return this.apiService.getAll('api/CarbonOffsettingAction', { params: { pageNumber, pageSize } }).pipe(
      map((response: any) => {
        const paginationResponse: PaginationResponse<CarbonOffsettingAction> = {
          items: response[0].items || [],
          totalSize: response[0].totalSize || 0
        };
        return paginationResponse;
      })
    );
  }

  public addCarbonOffsettingAction(action: CarbonOffsettingAction): Observable<CarbonOffsettingAction> {
    return this.apiService.add('api/CarbonOffsettingAction', action);
  }
}
