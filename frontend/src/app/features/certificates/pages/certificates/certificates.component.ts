import {AfterViewInit, Component, inject, OnInit, ViewChild} from '@angular/core';
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
import { DefaultPagination } from 'src/app/core/utilities/pagination-response';
import { AuthService } from 'src/app/core/services/auth.service';
import { Role } from 'src/types/roles.enum';
import { SupplyChainPartnerCertificate } from 'src/models/documents/supply-chain-partner-certificate.model';
import { DocumentType } from 'src/types/document.enum';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-certificates',
  imports: [CommonModule,MatSortModule,RouterLink, MatInputModule,MatTableModule, MatPaginatorModule, MatButtonModule, MatIconModule, MatFormFieldModule, MatSelectModule],
  templateUrl: './certificates.component.html',
  styleUrl: './certificates.component.css',
})
export class CertificatesComponent implements OnInit, AfterViewInit {

  readonly dialog = inject(MatDialog);

  private certificateService = inject(CertificatesService);
  private authService = inject(AuthService)
  private toastrService =  inject(ToastrService)

  totalRecords = 0;
  displayedColumns: string[] = ['receiver', 'scpType', 'actions'];
  dataSource = new MatTableDataSource<SupplyChainPartner>([]);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  ngOnInit(): void {
    this.getSupplyChainPartners()
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

   ngAfterViewInit(): void {
    if(this.isAuthorizated()){
      this.displayedColumns.splice(2,0,'attachments')
    }
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

  deleteCertificate(id: string){
    const fileName = id.split('/').pop() || id;
    this.certificateService.deleteCertificate(id,fileName).subscribe({
      next: (response) => {

        this.toastrService.info(`Removed certificate ${response.name}`, 'info')
      },
      error: (error) => { console.log(error) }
    })
  }

  attachCertificate(scpReceiverId: string) {
    const dialogRef = this.dialog.open(FileInputComponent,{
      data: {
        scpReceiverId: scpReceiverId,
        documentType: DocumentType.Sustainability
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result)
       this.getSupplyChainPartners()
    });
  }

  onPageChange(event: PageEvent){
    this.getSupplyChainPartners(event.pageSize, event.pageIndex)
  }

  isAuthorizated(): boolean {
    const roles = [Role.SysAdmin, Role.UserCA, Role.AdminCA];
    return this.authService.userRoles?.some(role => roles.includes(role)) ?? false;
  }

  getSustainabilityCertificate(receivedSupplyChainPartnerCertificates: SupplyChainPartnerCertificate[]): SupplyChainPartnerCertificate | null {
    if (!receivedSupplyChainPartnerCertificates?.length) return null;
    return receivedSupplyChainPartnerCertificates.find(doc => doc.type === DocumentType.Sustainability) || null;
  }

}
