import {Component, inject, OnInit, RESPONSE_INIT, ViewChild} from '@angular/core';
import {MatPaginator, MatPaginatorModule, PageEvent} from '@angular/material/paginator';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatSelectModule} from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { CommonModule } from '@angular/common'; // Add this import for NgClass
import { MatSort, MatSortModule } from '@angular/material/sort';
import { Log } from 'src/models/log';
import { DefaultPagination } from 'src/app/core/utilities/pagination-response';
import { LogsService } from '../services/log.service';
import { Severity } from 'src/types/severity.enum';

@Component({
  selector: 'app-logs',
  imports: [CommonModule, MatSortModule, MatInputModule, MatTableModule, MatPaginatorModule, MatButtonModule, MatIconModule, MatFormFieldModule, MatSelectModule],
  templateUrl: './logs.component.html',
  styleUrl: './logs.component.css'
})
export class LogsComponent implements OnInit {

    private logsService = inject(LogsService)

    severityLevels = Object.values(Severity);
    selected: Severity = Severity.Info;
    totalRecords = 0;
    displayedColumns: string[] = ['severity', 'entity', 'message', 'action', 'url', 'queryString', 'timestamp'];
    dataSource = new MatTableDataSource<Log>([]);

    @ViewChild(MatPaginator) paginator!: MatPaginator;
    @ViewChild(MatSort) sort!: MatSort;

    ngOnInit(): void {
      this.getLogs()
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    }

    getLogs(pageSize: number = DefaultPagination.defaultPageSize, pageNumber: number = DefaultPagination.defaultPageNumber, severity?: Severity){
      this.logsService.getLogs(pageSize.toString(),pageNumber.toString(),severity).subscribe({
        next: (response) => {
          this.dataSource = new MatTableDataSource<Log>(response.items!);
          this.totalRecords = response.totalSize
        },
        error: (error) => { console.log(error) }
      })
    }

    updateTable() {
      this.getLogs(DefaultPagination.defaultPageSize,DefaultPagination.defaultPageNumber,this.selected)
    }

    onPageChange(event: PageEvent){
      this.getLogs(event.pageSize, event.pageIndex)
    }

}
