import { OnInit, Component, ViewChild, inject } from '@angular/core';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
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

  // cache lists for faster access on client side
  supplyChainPartners: SupplyChainPartner[] = [];
  certificationAuthorities: CertificationAuthority[] = [];

  displayedColumns: string[] = [];

  scpSource: any;
  caSource:  any;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private companyService: CompanyService){}

  ngOnInit(): void {
    if (this.selected === CompanyType.SupplyChainPartner)
      this.getSupplyChainPartners();
    else
      this.getCertificationAuthorities();
  }

  getCertificationAuthorities(): void {
    this.companyService.getAllCertificationAuthorities().subscribe({
      next: (certificationAuthorities) => {
        this.certificationAuthorities = certificationAuthorities;
        this.showCertificationAuthorities();
      },
      error: (error) => console.log(error)
    })
  }

  getSupplyChainPartners(): void {
    this.companyService.getAllSupplyChainPartners().subscribe({
      next: (supplyChainPartners) => {
        this.supplyChainPartners = supplyChainPartners;
        this.showSupplyChainPartners();
      },
      error: (error) => console.log(error)
    })
  }

  updateTable(): void {
    if (this.selected === CompanyType.CertificationAuthority && this.certificationAuthorities.length === 0) {
      this.getCertificationAuthorities();
    }
    if (this.selected === CompanyType.SupplyChainPartner && this.supplyChainPartners.length === 0) {
      this.getSupplyChainPartners();
    }

    if (this.selected === CompanyType.SupplyChainPartner)
      this.showSupplyChainPartners();
    else
      this.showCertificationAuthorities();
  }

  sendCompany(companyId: string): void {
    this.companyService.passCompany(companyId, this.selected);
  }

  addCompany(): void {
    this.dialog.open(
      CompanyDialogComponent,
      { data: {companyType: this.selected} }
    );
  }

  private showSupplyChainPartners(): void {
    this.displayedColumns = ['name', 'email', 'phone', 'type', 'wallet_id', 'action'];
    this.scpSource = new MatTableDataSource<SupplyChainPartner>(this.supplyChainPartners);
    this.scpSource.paginator = this.paginator;
  }

  private showCertificationAuthorities(): void {
    this.displayedColumns = ['name', 'email', 'phone', 'action'];
    this.caSource = new MatTableDataSource<CertificationAuthority>(this.certificationAuthorities);
    this.caSource.paginator = this.paginator;
  }
}
