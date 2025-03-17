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
import { ToastrService } from 'ngx-toastr';

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
  constructor(
    private route: ActivatedRoute,
    private userService: UserService,
    private toasterService: ToastrService
  ) {}

  readonly dialog = inject(MatDialog);
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  userSource: any;
  users: User[] = [];
  companyId: string | null = null;
  companyType: CompanyType | null = null;
  displayedColumns: string[] = ['firstName', 'lastName', 'email', 'phone', 'role', 'action'];

  ngOnInit(): void {
    this.companyId = this.route.snapshot.paramMap.get('id');
    this.companyType = this.route.snapshot.queryParamMap.get('type') as CompanyType | null;
    if (!this.companyId || !this.companyType)
      return;

    this.getUsers();
  }

  addUser(): void {
    let dialogData = {
      id: this.companyId,
      type: this.companyType,
    };

    this.dialog.open(UserDialogComponent, { data: dialogData });
  }

  deleteUser(id: string): void {
    this.userService.deleteUser(id).subscribe((response) => {
      if (!response) {
        this.showToast('Error in removing user', 'error');
        return;
      }
      this.showToast('User deleted successfully!', 'success');
      this.getUsers();
    });
  }

  private showToast(message: string, severity: string | undefined) {
    switch(severity) {
      case 'success':
        this.toasterService.success(message, 'Success');
        break;
      case 'error':
        this.toasterService.error(message, 'Error');
        break;
      default:
        this.toasterService.info(message, 'Info');
    }
  }

  private getUsers(): void {
    this.userService.getUsersByCompanyId(this.companyId!, this.companyType!).subscribe({
      next: (users) => {
        this.users = users;
        this.userSource = new MatTableDataSource<User>(this.users);
        this.userSource.paginator = this.paginator;
      },
      error: (error) => console.error(error)
    })
  }
}

