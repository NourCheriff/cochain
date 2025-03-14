import { AfterViewInit, OnInit, Component, ViewChild, inject } from '@angular/core';
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
    RouterLink
  ],
  templateUrl: './companies.component.html',
  styleUrl: './companies.component.css'

})
export class CompaniesComponent implements  OnInit, AfterViewInit {

  user: User = {
    "supplyChainPartner": "Alpha",
    "role": "Admin"
  };

  readonly dialog = inject(MatDialog);

  selected = 'supply_chain_partner';

  scpList: SupplyChainPartner[] = [];
  caList: CertificationAuthority[] = [];

  displayedColumns: string[] = ['name', 'email', 'phone', 'type', 'wallet_id', 'action'];

  scpSource: any;
  caSource:  any;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private companyService: CompanyService){}

  ngOnInit(): void {
    this.getAllSupplyChainPartners();
  }

  ngAfterViewInit() {
    this.scpSource!.paginator = this.paginator;
    this.caSource!.paginator = this.paginator;
  }

  getAllCertificationAuthorities(){
    this.companyService.getAllCertificationAuthorities().subscribe({
      next: (response) => {
        console.log(response)
        this.caList = response
        this.caSource = new MatTableDataSource<CertificationAuthority>(this.caList);
        this.caSource.paginator = this.paginator;
      },
      error: (error) => console.log(error)
    })
  }

  getAllSupplyChainPartners(){
    this.companyService.getAllSupplyChainPartners().subscribe({
      next: (response) => {
        console.log(response)
        this.scpList = response
        this.scpSource = new MatTableDataSource<SupplyChainPartner>(this.scpList);
        this.scpSource.paginator = this.paginator;
      },
      error: (error) => console.log(error)
    })
  }

  updateTable() {
    if(this.caList.length == 0){
      this.getAllCertificationAuthorities();
    }

    if(this.selected == "supply_chain_partner"){
      this.displayedColumns = ['name', 'email', 'phone', 'type', 'wallet_id', 'action'];
      this.scpSource = new MatTableDataSource<SupplyChainPartner>(this.scpList);
      this.scpSource.paginator = this.paginator;
    }
    else{
      this.displayedColumns = ['name', 'email', 'phone', 'action'];
      this.caSource = new MatTableDataSource<CertificationAuthority>(this.caList);
      this.caSource.paginator = this.paginator;
    }
  }

  sendCompany(companyId: string) {
    this.companyService.passCompany(companyId);
  }

  addCompany() {
    this.dialog.open(
      CompanyDialogComponent,
      {data: {company: this.selected}}
    );
  }
}

export interface User {
  supplyChainPartner: string;
  role: string;
}
