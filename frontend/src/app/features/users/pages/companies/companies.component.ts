import { OnInit, Component, ViewChild, inject } from '@angular/core';
import { MatPaginator, MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { CompanyDialogComponent } from '../../components/company-dialog/company-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { FormsModule }   from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { RouterLink } from '@angular/router';
import { CompanyService } from '../../services/company.service';
import { CertificationAuthority } from 'src/models/company-entities/certification-authority.model';
import { SupplyChainPartner } from 'src/models/company-entities/supply-chain-partner.model';
import { CompanyType } from 'src/types/company.enum';
import { DefaultPagination } from 'src/app/core/utilities/paginationResponse';

@Component({
  selector: 'app-companies',
  imports: [
    MatTableModule,
    MatPaginatorModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatSelectModule,
    FormsModule,
    MatInputModule,
    RouterLink,
  ],
  templateUrl: './companies.component.html',
  styleUrl: './companies.component.css'

})
export class CompaniesComponent implements OnInit {

  readonly dialog = inject(MatDialog);
  CompanyType = CompanyType;

  selected = CompanyType.SupplyChainPartner;
  displayedColumns: string[] = [];

  scpSource: any;
  caSource:  any;
  totalRecords = 0;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private companyService: CompanyService){}

  ngOnInit(): void {
    if (this.selected === CompanyType.SupplyChainPartner){
      this.getSupplyChainPartners();
      this.showSupplyChainPartners()
    }else{
      this.getCertificationAuthorities();
      this.showCertificationAuthorities()
    }
  }

  getCertificationAuthorities(pageSize: number = DefaultPagination.defaultPageSize, pageNumber: number = DefaultPagination.defaultPageNumber): void {
    this.companyService.getAllCertificationAuthorities(pageSize.toString(),pageNumber.toString()).subscribe({
      next: (certificationAuthorities) => {
        this.caSource = new MatTableDataSource<CertificationAuthority>(certificationAuthorities.items!);
        this.totalRecords = certificationAuthorities.totalSize;
      },
      error: (error) => console.error(error)
    })
  }

  getSupplyChainPartners(pageSize: number = DefaultPagination.defaultPageSize, pageNumber: number = DefaultPagination.defaultPageNumber): void {
    this.companyService.getAllSupplyChainPartners(pageSize.toString(),pageNumber.toString()).subscribe({
      next: (supplyChainPartners) => {
        this.scpSource = new MatTableDataSource<SupplyChainPartner>(supplyChainPartners.items!);
        this.totalRecords = supplyChainPartners.totalSize;
      },
      error: (error) => console.error(error)
    })
  }

  updateTable(): void {
    if (this.selected === CompanyType.CertificationAuthority) {
        this.getCertificationAuthorities();
        this.showCertificationAuthorities();
    } else if (this.selected === CompanyType.SupplyChainPartner) {
        this.getSupplyChainPartners();
        this.showSupplyChainPartners();
    }
  }

  addCompany(): void {
    const dialogRef = this.dialog.open(
      CompanyDialogComponent,
      { data: { companyType: this.selected } }
    );
    dialogRef.afterClosed().subscribe(result => {
      if(result && result.reloadContent)
        result.isSCP ? this.getSupplyChainPartners() : this.getCertificationAuthorities()
    });
  }

  private showSupplyChainPartners(): void {
    this.displayedColumns = ['name', 'email', 'phone', 'type', 'wallet_id', 'action'];
    this.scpSource.paginator = this.paginator;
  }

  private showCertificationAuthorities(): void {
    this.displayedColumns = ['name', 'email', 'phone', 'action'];
    this.caSource.paginator = this.paginator;
  }

  onPageChange(event: PageEvent){
    this.selected === CompanyType.SupplyChainPartner ?
      this.getSupplyChainPartners(event.pageSize,event.pageIndex)
    :
      this.getCertificationAuthorities(event.pageSize,event.pageIndex)
  }
}
