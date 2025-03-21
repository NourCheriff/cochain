import {Component, inject, OnInit, ViewChild} from '@angular/core';
import {MatPaginator, MatPaginatorModule, PageEvent} from '@angular/material/paginator';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatSelectModule} from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { RouterLink } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { CommonModule } from '@angular/common'; // Add this import for NgClass
import { MatSort, MatSortModule } from '@angular/material/sort';
import { FileInputComponent } from '../../components/file-input/file-input.component';
import { CertificatesService } from '../../service/certificates.service';
import { SupplyChainPartner } from 'src/models/company-entities/supply-chain-partner.model';
import { DefaultPagination } from 'src/app/core/utilities/paginationResponse';

@Component({
  selector: 'app-certificates',
  imports: [CommonModule,MatSortModule,RouterLink, MatInputModule,MatTableModule, MatPaginatorModule, MatButtonModule, MatIconModule, MatFormFieldModule, MatSelectModule],
  templateUrl: './certificates.component.html',
  styleUrl: './certificates.component.css',
})
export class CertificatesComponent implements OnInit {
  readonly dialog = inject(MatDialog);

  private certificateService = inject(CertificatesService);

  scpType: SCPType = {
    "type": "CA"
  }
  totalRecords = 0;
  displayedColumns: string[] = ['receiver', 'scpType', 'attachments', 'actions'];
  dataSource = new MatTableDataSource<SupplyChainPartner>([]);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  ngOnInit(): void {
    this.getSupplyChainPartners()
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  getSupplyChainPartners(pageSize: number = DefaultPagination.defaultPageSize, pageNumber: number = DefaultPagination.defaultPageNumber){
    this.certificateService.getSupplyChainPartners(pageSize.toString(),pageNumber.toString()).subscribe({
      next: (response) => {
        this.dataSource = new MatTableDataSource<SupplyChainPartner>(response.items!);
        this.totalRecords = response.totalSize
      },
      error: (error) => { console.log(error) }
    })
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  deleteCertificate(id: string){
    this.certificateService.deleteCertificate(id).subscribe({
      next: (response) => {
        console.log(response)
      },
      error: (error) => { console.log(error) }
    })
  }

  attachCertificate(scpReceiverId: string) {
    this.dialog.open(FileInputComponent,{
      data: {
        scpReceiverId: scpReceiverId,
        documentType: 'sustainability'
      }
    });
  }

  onPageChange(event: PageEvent){
    this.getSupplyChainPartners(event.pageSize, event.pageIndex)
  }

}

export interface SCPType {
  type: string
}
