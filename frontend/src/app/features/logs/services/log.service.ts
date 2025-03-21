import { inject, Injectable } from "@angular/core";
import { map, Observable } from "rxjs";
import { BaseHttpService } from "src/app/core/services/api.service";
import { PaginationResponse } from "src/app/core/utilities/paginationResponse";
import { Log } from "src/models/log";
import { Severity } from "src/types/severity.enum";

@Injectable({
  providedIn: 'root'
})
export class LogsService {

  private apiService = inject(BaseHttpService)

  getLogs(pageSize: string, pageNumber: string, severity?: Severity): Observable<PaginationResponse<Log>>{
    return this.apiService.getAll('api/Log', { params: { pageNumber, pageSize, severity} }).pipe(
      map((response: any) => {
        const paginationResponse: PaginationResponse<Log> = {
          items: response[0].items || [],
          totalSize: response[0].totalSize || 0
        };
        return paginationResponse;
      })
    );
  }
}
