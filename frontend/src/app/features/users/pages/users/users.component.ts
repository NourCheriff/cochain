import { Component, ViewChild, inject, OnInit } from '@angular/core';
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
import { UserService } from '../../services/user.service';
import { User } from 'src/models/auth/user.model';
import { CompanyType } from 'src/types/company.enum';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/core/services/auth.service';
import { Role } from 'src/types/roles.enum';
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

  private authService = inject(AuthService)

  readonly dialog = inject(MatDialog);
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  userSource: MatTableDataSource<User> = new MatTableDataSource<User>([]);
  companyId: string | null = null;
  companyType: CompanyType | null = null;
  displayedColumns: string[] = [];

  ngOnInit(): void {
    this.companyId = this.route.snapshot.paramMap.get('id');
    this.companyType = this.route.snapshot.queryParamMap.get('type') as CompanyType | null;
    if (!this.companyId || !this.companyType)
      return;

    this.getUsers();

    if(this.isAdmin()){
      this.displayedColumns = ['firstName', 'lastName', 'email', 'phone', 'role', 'action'];
    }
    else{
      this.displayedColumns = ['firstName', 'lastName', 'email', 'phone', 'role'];
    }
  }

  addUser(): void {
    let dialogData = {
      id: this.companyId,
      type: this.companyType,
    };

    const dialogRef = this.dialog.open(UserDialogComponent, { data: dialogData });
    dialogRef.afterClosed().subscribe(result => {
      if(result)
        this.getUsers()
    });
  }

  deleteUser(id: string): void {
    this.userService.deleteUser(id).subscribe({
      next: (response) => {
        if (!response) {
          this.showToast('Error in removing user', 'error');
          return;
        }
        this.showToast('User deleted successfully!', 'success');
        this.getUsers();
      },
      error: (error) => {
        console.error(error);
        this.showToast('Error in removing user', 'error');
      }
    }
  );
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
        this.userSource.data = users;
      this.userSource.paginator = this.paginator;
      },
      error: (error) => console.error(error)
    })
  }

  isAdmin(): boolean {
    return this.authService.userRoles!.includes(Role.SysAdmin);
  }
}

