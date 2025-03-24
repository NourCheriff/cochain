import { Component, inject, ViewChild, OnInit } from '@angular/core';
import { MatPaginator, MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { CarbonOffsettingAction } from 'src/models/carbon-offset/carbon-offsetting-actions.model';
import { CarbonOffsettingService } from '../../services/carbon-offsetting.service';
import { DefaultPagination } from 'src/app/core/utilities/pagination-response';

@Component({
  selector: 'app-carbon-offsetting',
  imports: [
    MatPaginatorModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatSelectModule,
    MatFormFieldModule,
  ],
  templateUrl: './carbon-offsetting.component.html',
  styleUrl: './carbon-offsetting.component.css'
})
export class CarbonOffsettingComponent {

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  displayedColumns: string[] = ['action', 'offset'];
  dataSource = new MatTableDataSource<CarbonOffsettingAction>([]);
  totalRecords = 0;

  private carbonOffsettingService = inject(CarbonOffsettingService);

  ngOnInit(): void {
    this.getCarbonOffsettingActions()
    this.dataSource.paginator = this.paginator;
  }

  getCarbonOffsettingActions(pageSize: number = DefaultPagination.defaultPageSize, pageNumber: number = DefaultPagination.defaultPageNumber){
    this.carbonOffsettingService.getCarbonOffsettingActions(pageSize.toString(),pageNumber.toString()).subscribe({
      next: (response) => {
        this.dataSource = new MatTableDataSource<CarbonOffsettingAction>(response.items!);
        this.totalRecords = response.totalSize
      },
      error: (error) => { console.log(error) }
    })
  }

  onPageChange(event: PageEvent){
    this.getCarbonOffsettingActions(event.pageSize, event.pageIndex)
  }

}
