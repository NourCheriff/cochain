import { AfterViewInit, Component, ViewChild, inject } from '@angular/core';
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
    MatInputModule
  ],
  templateUrl: './companies.component.html',
  styleUrl: './companies.component.css'

})
export class CompaniesComponent implements AfterViewInit {

  user: User = {
    "supplyChainPartner": "Alpha",
    "role": "Admin"
  };

  readonly dialog = inject(MatDialog);

  selected = 'supply_chain_partner';

  displayedColumns: string[] = ['name', 'email', 'phone', 'type', 'wallet_id', 'action'];

  scpSource = new MatTableDataSource<SCPElement>(SCP_DATA);
  caSource  = new MatTableDataSource<CAElement>(CA_DATA);

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  ngAfterViewInit() {
    this.scpSource.paginator = this.paginator;
    this.caSource.paginator  = this.paginator;
  }

  updateTable() {
    if(this.selected == "supply_chain_partner"){
      this.displayedColumns = ['name', 'email', 'phone', 'type', 'wallet_id', 'action'];
      this.scpSource = new MatTableDataSource<SCPElement>(SCP_DATA);
      this.scpSource.paginator = this.paginator;
    }
    else{
      this.displayedColumns = ['name', 'email', 'phone', 'action'];
      this.caSource = new MatTableDataSource<CAElement>(CA_DATA);
      this.caSource.paginator = this.paginator;
    }
  }

  addCompany() {
    this.dialog.open(
      CompanyDialogComponent,
      {data: {
        company: this.selected,
      }}
    );
  }
}

export interface User {
  supplyChainPartner: string;
  role: string;
}

export interface SCPElement {
  name:      string;
  email:     string;
  phone:     string;
  type:      string;
  wallet_id: string;
}

export interface CAElement {
  name:      string;
  email:     string;
  phone:     string;
}

const SCP_DATA: SCPElement[] = [
  { name: "Alpha", email: "alpha@example.com", phone: "3208947123", type: "Supplier", wallet_id: "bc1qar0srrr0pqqqqpqy9r0f4j9876543" },
  { name: "Beta", email: "beta@example.com", phone: "3275612398", type: "Distributor", wallet_id: "bc1qxy2kgdygjrsqtzq2n0yrf27890123" },
  { name: "Gamma", email: "gamma@example.com", phone: "3314765281", type: "Retailer", wallet_id: "bc1q5t4yqqqqqqqqqqqqqqqqqq56789012" },
  { name: "Delta", email: "delta@example.com", phone: "3205829147", type: "Consumer", wallet_id: "bc1q9u0c8e7e2g3p4gqz6yfr4h3456789" },
  { name: "Epsilon", email: "epsilon@example.com", phone: "3387654123", type: "Supplier", wallet_id: "bc1q8r6t5yh9wr2x2t5q4p2pfq2345678" },
  { name: "Zeta", email: "zeta@example.com", phone: "3391426758", type: "Distributor", wallet_id: "bc1qrp2d00cpz24lfxq7k3l7f67890123" },
  { name: "Eta", email: "eta@example.com", phone: "3406582941", type: "Retailer", wallet_id: "bc1qjz0p2h3wn5q2a4l7fgxy9e4567890" },
  { name: "Theta", email: "theta@example.com", phone: "3457123984", type: "Consumer", wallet_id: "bc1qlt7yfq8h7w9p2q4x2y3g6n5678901" },
  { name: "Iota", email: "iota@example.com", phone: "3284659123", type: "Supplier", wallet_id: "bc1q5x2y9wrq0lfxq7h3p2g8r62345678" },
  { name: "Kappa", email: "kappa@example.com", phone: "3298475126", type: "Distributor", wallet_id: "bc1qx2y9wpq4t7g3lf7h5p6r278901234" },
  { name: "Lambda", email: "lambda@example.com", phone: "3219658473", type: "Retailer", wallet_id: "bc1q7h5p2y9wrq4t3g6lfx8n012345678" },
  { name: "Mu", email: "mu@example.com", phone: "3375412698", type: "Consumer", wallet_id: "bc1qlfxq4t7h3p2y9wr8n5g6789012345" },
  { name: "Nu", email: "nu@example.com", phone: "3321478596", type: "Supplier", wallet_id: "bc1q8n5g7h3p2wrx2y4t6lfq012345678" },
  { name: "Xi", email: "xi@example.com", phone: "3307852149", type: "Distributor", wallet_id: "bc1q2y9wrx4t7g5h3p6lfn8q789012345" },
  { name: "Omicron", email: "omicron@example.com", phone: "3226984751", type: "Retailer", wallet_id: "bc1q4t7g5h3p2y9wrx6lfn8q012345678" },
  { name: "Pi", email: "pi@example.com", phone: "3198527463", type: "Consumer", wallet_id: "bc1qx6lfn8q4t7g5h3p2y9wr789012345" },
  { name: "Rho", email: "rho@example.com", phone: "3421587963", type: "Supplier", wallet_id: "bc1q7g5h3p2y9wrx4t6lfn8q012345678" },
  { name: "Sigma", email: "sigma@example.com", phone: "3269784125", type: "Distributor", wallet_id: "bc1q2wrx6lfn8q4t7g5h3p9y789012345" },
  { name: "Tau", email: "tau@example.com", phone: "3412659874", type: "Retailer", wallet_id: "bc1q9wrx6lfn8q4t7g5h3p2y012345678" },
  { name: "Upsilon", email: "upsilon@example.com", phone: "3245197862", type: "Consumer", wallet_id: "bc1q6lfn8q4t7g5h3p2y9wrx789012345" }
];

const CA_DATA: CAElement[] = [
  { name: "ALCE", email: "alce@example.com", phone: "3208947123" },
  { name: "BARNABAS", email: "barnabas@example.com", phone: "3275612398" },
  { name: "CASSIOPEA", email: "cassiopea@example.com", phone: "3314765281" },
  { name: "DRACO", email: "draco@example.com", phone: "3205829147" },
  { name: "ORIONE", email: "orione@example.com", phone: "3387654123" },
  { name: "FENICE", email: "fenice@example.com", phone: "3391426758" },
  { name: "HERA", email: "hera@example.com", phone: "3406582941" },
  { name: "ALCYONE", email: "alcyone@example.com", phone: "3457123984" },
  { name: "CRONOS", email: "cronos@example.com", phone: "3284659123" },
  { name: "POSEIDONE", email: "poseidone@example.com", phone: "3298475126" },
  { name: "ATHENA", email: "athena@example.com", phone: "3219658473" },
  { name: "ZEUS", email: "zeus@example.com", phone: "3375412698" }
];
