import { Component, inject } from '@angular/core';
import { MatDialogContent, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ReactiveFormsModule, FormControl, Validators, FormGroup, FormsModule } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { User } from 'src/models/auth/user.model';
import { Role } from 'src/types/roles.enum';
import { CompanyType } from 'src/types/company.enum';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-user-dialog',
  imports: [
    MatInputModule,
    MatButtonModule,
    MatDialogContent,
    MatSelectModule,
    MatFormFieldModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  templateUrl: './user-dialog.component.html',
  styleUrl: './user-dialog.component.css'
})
export class UserDialogComponent {

  constructor(private userService: UserService) {}

  private toasterService = inject(ToastrService);

  readonly dialogRef = inject(MatDialogRef<UserDialogComponent>);
  data = inject<CompanyData>(MAT_DIALOG_DATA);
  Role = Role;
  selectedRole = Role.User;

  userForm  = new FormGroup<NewUserForm>({
    firstName: new FormControl("", [Validators.required],),
    lastName: new FormControl("", [Validators.required],),
    userName: new FormControl("", [Validators.required, Validators.email],),
    phone: new FormControl("", [Validators.required, Validators.pattern('[- +()0-9]+')],),
    role: new FormControl(this.selectedRole, [Validators.required],),
  });

  public insertUser(): void {
    if (!this.userForm.valid || !this.data.id || !this.data.type)
      return;

    let newUser: User = {
      firstName: this.userForm.value.firstName!,
      lastName: this.userForm.value.lastName!,
      userName: this.userForm.value.userName!,
      phone: this.userForm.value.phone!,
      role: this.userForm.value.role!,
      ...(this.data.type === CompanyType.SupplyChainPartner ? { supplyChainPartnerId: this.data.id! } : { certificationAuthorityId: this.data.id! }),
    }

    this.userService.addUser(newUser).subscribe({
      next: (response) => {
        console.log(response);
        this.showToast('User inserted successfully!', 'success');
        this.dialogRef.close();
      },
      error: (error) => {
        console.error(error);
        this.showToast('Error on user insertion', 'error');
      }
    })
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
}

interface NewUserForm {
  firstName: FormControl<string | null>;
  lastName: FormControl<string | null>;
  userName: FormControl<string | null>;
  phone: FormControl<string | null>;
  role: FormControl<Role | null>;
}

interface CompanyData {
  id: string | null,
  type: CompanyType | null,
}
