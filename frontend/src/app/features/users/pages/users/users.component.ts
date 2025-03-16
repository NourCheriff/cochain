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
import { UserService } from '../../services/user.service';
import { User } from 'src/models/auth/user.model';
import { CompanyType } from 'src/types/company.enum';
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
  constructor(private route: ActivatedRoute, private userService: UserService) {}

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  userSource: any;
  users: User[] = [];
  companyId: string | null = null;
  displayedColumns: string[] = ['firstName', 'lastName', 'email', 'phone', 'role', 'action'];

  ngOnInit(): void {
    this.companyId = this.route.snapshot.paramMap.get('id');
    let companyType = this.route.snapshot.queryParamMap.get('type') as CompanyType | null;
    if (!this.companyId || !companyType)
      return;

    this.userService.getUsersByCompanyId(this.companyId, companyType).subscribe({
      next: (users) => {
        this.users = users;
        this.userSource = new MatTableDataSource<User>(this.users);
        this.userSource.paginator = this.paginator;
      },
      error: (error) => console.log(error)
    })
  }

  addUser() {
    this.dialog.open(UserDialogComponent,
      { data: { company: this.companyId } });
  }
}

