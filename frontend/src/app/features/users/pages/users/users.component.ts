
import { AfterViewInit, Component, ViewChild, inject, OnInit } from '@angular/core';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { UserDialogComponent } from '../../components/user-dialog/user-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { FormsModule }   from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-users',
  imports: [
    MatTableModule,
    MatPaginatorModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    FormsModule,
    MatInputModule
  ],
  templateUrl: './users.component.html',
  styleUrl: './users.component.css'

})
export class UsersComponent implements AfterViewInit, OnInit {

  readonly dialog = inject(MatDialog);
  constructor(private route: ActivatedRoute) {}

  companyId: string | null = null;

  ngOnInit(): void {
    this.companyId = this.route.snapshot.paramMap.get('id');//to get SCP/CA name for query
  }

  user: User = {
    "supplyChainPartner": "Alpha",
    "role": "Admin"
  };

  displayedColumns: string[] = ['firstName', 'lastName', 'email', 'phone', 'role', 'action'];

  dataSource = new MatTableDataSource<PersonElement>(PERSON_DATA);

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  addUser() {
    this.dialog.open(UserDialogComponent);
  }
}

export interface User {
  supplyChainPartner: string;
  role:               string;
}

export interface PersonElement {
  firstName: string;
  lastName:  string;
  email:     string;
  phone:     string;
  role:      string;
}

const PERSON_DATA: PersonElement[] = [
  { firstName: "Marco", lastName: "Rossi", email: "marco.rossi@example.com", phone: "3208947123", role: "Admin" },
  { firstName: "Giulia", lastName: "Bianchi", email: "giulia.bianchi@example.com", phone: "3275612398", role: "User" },
  { firstName: "Luca", lastName: "Verdi", email: "luca.verdi@example.com", phone: "3314765281", role: "User" },
  { firstName: "Anna", lastName: "Neri", email: "anna.neri@example.com", phone: "3205829147", role: "Moderator" },
  { firstName: "Francesca", lastName: "Ferrari", email: "francesca.ferrari@example.com", phone: "3387654123", role: "User" },
  { firstName: "Giovanni", lastName: "Esposito", email: "giovanni.esposito@example.com", phone: "3391426758", role: "Admin" },
  { firstName: "Paola", lastName: "Russo", email: "paola.russo@example.com", phone: "3406582941", role: "User" },
  { firstName: "Alessandro", lastName: "Gallo", email: "alessandro.gallo@example.com", phone: "3457123984", role: "User" },
  { firstName: "Andrea", lastName: "Colombo", email: "andrea.colombo@example.com", phone: "3284659123", role: "Moderator" },
  { firstName: "Simona", lastName: "Ricci", email: "simona.ricci@example.com", phone: "3298475126", role: "User" },
  { firstName: "Elena", lastName: "Marini", email: "elena.marini@example.com", phone: "3219658473", role: "Admin" },
  { firstName: "Stefano", lastName: "Greco", email: "stefano.greco@example.com", phone: "3375412698", role: "User" }
];
