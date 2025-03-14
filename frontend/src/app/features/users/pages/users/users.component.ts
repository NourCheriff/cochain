
import { Component, ViewChild, inject, OnInit } from '@angular/core';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule, MatTable } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { UserDialogComponent } from '../../components/user-dialog/user-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { FormsModule }   from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { ActivatedRoute } from '@angular/router';
import { CompanyService } from '../../services/company.service';
import { User } from 'src/models/auth/user.model';
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
export class UsersComponent implements OnInit {


  readonly dialog = inject(MatDialog);
  constructor(private route: ActivatedRoute, private companyService: CompanyService) {}

  @ViewChild(MatTable) myTable!: MatTable<any>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  userSource: any;
  usersList: User[] = [];
  companyId: string | null = null;
  companyId2: string | null = null;
  displayedColumns: string[] = ['firstName', 'lastName', 'email', 'phone', 'role', 'action'];

  ngOnInit(): void {
    this.companyId2 = this.route.snapshot.paramMap.get('id');// to get SCP/CA name for query

    this.companyId = this.companyService.getCurrentCompanyId();
    this.companyService.getUsersByCompanyId(this.companyId!).subscribe({
      next: (response) => {
        this.usersList = response;
        this.userSource = new MatTableDataSource<User>(this.usersList);
        this.userSource.paginator = this.paginator;
      },
      error: (error) => console.log(error)
    })
  }
  addUser() {
    this.dialog.open(UserDialogComponent);
  }
}

