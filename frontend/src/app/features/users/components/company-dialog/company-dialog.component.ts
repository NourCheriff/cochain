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
  companyForm  = new FormGroup<CompanyForm>({
    nameCompany:   new FormControl("", [Validators.required],),
    emailCompany:  new FormControl("", [Validators.required, Validators.email],),
    phoneCompany:  new FormControl("", [Validators.required],),

    firstNameUser: new FormControl("", [Validators.required],),
    lastNameUser:  new FormControl("", [Validators.required],),
    emailUser:     new FormControl("", [Validators.required, Validators.email],),
    phoneUser:     new FormControl("", [Validators.required],),
  });

  constructor(@Inject(MAT_DIALOG_DATA) public data: {companyType: CompanyType}) {
    if(this.isSupplyChainPartner()){
      this.companyForm.addControl('walletCompany', new FormControl<string | null>('', Validators.required));
    }
  }

  CompanyType = CompanyType;
  selected = "retailer";

  isSupplyChainPartner(){
    return this.data.companyType === CompanyType.SupplyChainPartner;
  }

  insertCompanyAndUser(){
    //where to insert the query for adding a company and the SCP/CA admin
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
