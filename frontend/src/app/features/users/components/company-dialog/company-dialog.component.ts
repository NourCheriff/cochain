import { Component, inject, Inject } from '@angular/core';
import { MatDialogContent, MatDialogRef } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDividerModule } from '@angular/material/divider';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ReactiveFormsModule, FormControl, Validators, FormGroup, FormsModule } from '@angular/forms';
import { CompanyType } from 'src/types/company.enum';
import { CompanyService } from '../../services/company.service';
import { SupplyChainPartnerType } from 'src/models/company-entities/supply-chain-partner-type.model';
import { SupplyChainPartner } from 'src/models/company-entities/supply-chain-partner.model';
import { User } from 'src/models/auth/user.model';
import { Role } from 'src/types/roles.enum';
import { CertificationAuthority } from 'src/models/company-entities/certification-authority.model';
import { UserService } from '../../services/user.service';
import { Company } from 'src/models/company-entities/company.model';
import { ToastrService } from 'ngx-toastr';
import { SanitizerUtil } from 'src/app/core/utilities/sanitizer';

@Component({
  selector: 'app-company-dialog',
  imports: [
    MatInputModule,
    MatButtonModule,
    MatDialogContent,
    MatSelectModule,
    MatFormFieldModule,
    FormsModule,
    MatDividerModule,
    ReactiveFormsModule
  ],
  templateUrl: './company-dialog.component.html',
  styleUrl: './company-dialog.component.css',
})
export class CompanyDialogComponent {
  readonly dialogRef = inject(MatDialogRef<CompanyDialogComponent>);

  private companyService = inject(CompanyService)
  private userService = inject(UserService)
  private toasterService = inject(ToastrService)
  private sanitizer = inject(SanitizerUtil)

  companyForm  = new FormGroup<CompanyForm>({
    nameCompany:   new FormControl("", [Validators.required]),
    emailCompany:  new FormControl("", [Validators.required, Validators.email],),
    phoneCompany:  new FormControl("", [Validators.required,Validators.pattern('[- +()0-9]+')]),

    firstNameUser: new FormControl("", [Validators.required]),
    lastNameUser:  new FormControl("", [Validators.required]),
    emailUser:     new FormControl("", [Validators.required, Validators.email],),
    phoneUser:     new FormControl("", [Validators.required,Validators.pattern('[- +()0-9]+')]),
  });

  CompanyType = CompanyType;
  selectedTypeId = ''
  scpTypes: SupplyChainPartnerType[] = [];
  reloadContentAfterDialogClosed: boolean = false;

  constructor(@Inject(MAT_DIALOG_DATA) public data: {companyType: CompanyType}) {
    if(this.isSupplyChainPartner()){
      this.getSupplyChainPartnerTypes()
      this.companyForm.addControl('walletCompany', new FormControl<string | null>('', Validators.required));
    }
  }

  isSupplyChainPartner(){
    return this.data.companyType === CompanyType.SupplyChainPartner;
  }

  getSupplyChainPartnerTypes(){
    this.companyService.getSupplyChainPartnerTypes().subscribe({
      next: (response) => {
        this.scpTypes = response
      },
      error: (error) => console.error(error)
    })
  }

  insertCompanyAndUser() {
    const sanitizedForm = this.sanitizer.sanitizeForm(this.companyForm)

    const companyData: Company = {
      name: sanitizedForm.nameCompany,
      phone: sanitizedForm.phoneCompany,
      email: sanitizedForm.emailCompany
    };

    const userData: User = {
      firstName: sanitizedForm.firstNameUser,
      lastName: sanitizedForm.lastNameUser,
      userName: sanitizedForm.emailUser,
      phone: sanitizedForm.phoneUser,
      role: Role.Admin
    };

    if (this.isSupplyChainPartner()) {
      companyData.walletId = this.sanitizer.sanitizeValue(this.companyForm.value.walletCompany);
      this.addSupplyChainPartner(companyData, userData);
    } else {
      this.addCertificationAuthority(companyData, userData);
    }
  }

  private addSupplyChainPartner(companyData: Company, userData: User) {
    const supplyChainPartner: SupplyChainPartner = {
      ...companyData,
      supplyChainPartnerTypeId: this.sanitizer.sanitizeValue(this.selectedTypeId),
      credits: 0
    };

    this.companyService.addSupplyChainPartner(supplyChainPartner).subscribe({
      next: (response) => {
        this.addUser({ ...userData, supplyChainPartnerId: response.id });
      },
      error: (error) => {
        this.showToast('Error adding supply chain partner', 'error');
        console.error("Error adding supply chain partner:", error)
        this.dialogRef.close()
      }
    });
  }

  private addCertificationAuthority(companyData: any, userData: any) {
    const certificationAuthority: CertificationAuthority = { ...companyData };

    this.companyService.addCertificationAuthority(certificationAuthority).subscribe({
      next: (response) => {
        this.addUser({ ...userData, certificationAuthorityId: response.id });
      },
      error: (error) => {
        this.showToast('Error adding certification authority', 'error');
        console.error("Error adding certification authority:", error)
        this.dialogRef.close()
      }
    });
  }

  private addUser(user: User) {
    this.userService.addUser(user).subscribe({
      next: () => {
        this.showToast('Company and User added successfully', 'success');
        this.reloadContentAfterDialogClosed = true;
        this.dialogRef.close({
          reloadContent: this.reloadContentAfterDialogClosed,
          isSCP: this.isSupplyChainPartner()
        });
      },
      error: (error) => {
        this.showToast('Error adding user', 'error');
        console.error("Error adding user:", error)
        this.reloadContentAfterDialogClosed = false;
        this.dialogRef.close(
          {reloadContent: this.reloadContentAfterDialogClosed }
        )
      }
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
}

interface CompanyForm {
  nameCompany:    FormControl<string | null>;
  emailCompany:   FormControl<string | null>;
  phoneCompany:   FormControl<string | null>;
  firstNameUser:  FormControl<string | null>;
  lastNameUser:   FormControl<string | null>;
  emailUser:      FormControl<string | null>;
  phoneUser:      FormControl<string | null>;
  walletCompany?: FormControl<string | null>;
}
