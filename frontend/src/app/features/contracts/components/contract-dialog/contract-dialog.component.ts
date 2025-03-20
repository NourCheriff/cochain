import { Component, ElementRef, inject, OnInit, ViewChild } from '@angular/core';
import {
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle,
} from '@angular/material/dialog';
import { sha256 } from "js-sha256";
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {MatSelectModule} from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatIconModule} from '@angular/material/icon';
import { FormControl, FormGroup, Validators, ReactiveFormsModule} from '@angular/forms';
import { Contract } from 'src/models/documents/contract.model';
import { ContractService } from '../../service/contract.service';
import { ProductLifeCycleCategory } from 'src/models/product/product-life-cycle-category.model';
import { SupplyChainPartner } from 'src/models/company-entities/supply-chain-partner.model';
import { AuthService } from 'src/app/core/services/auth.service';

@Component({
  selector: 'app-contract-dialog',
  imports: [
    MatInputModule,
    MatButtonModule,
    MatDialogTitle,
    MatDialogContent,
    MatSelectModule,
    MatFormFieldModule,
    MatIconModule,
    ReactiveFormsModule
  ],
  templateUrl: './contract-dialog.component.html',
  styleUrl: './contract-dialog.component.css'
})
export class ContractDialogComponent implements OnInit {
  private authService = inject(AuthService)

  readonly dialogRef = inject(MatDialogRef<ContractDialogComponent>);

  @ViewChild("fileInput") fileInput!: ElementRef
  fileUploaded!: File
  uploadEnabled: boolean = false;

  newContractForm = new FormGroup({
    work: new FormControl('', Validators.required),
    receiver: new FormControl('', Validators.required),
  });

  supplyChainPartners: SupplyChainPartner[] = []
  productLifeCycleCategories: ProductLifeCycleCategory[] = [];
  selecteCategoryId = '';
  selectedReceiverId = '';

  constructor(private contractService: ContractService) {}

  ngOnInit(): void {
    this.getAllSupplyChainPartner()
  }

  getAllProductLifeCycleCategories(){
    this.contractService.getAllProductLifeCycleCategories().subscribe({
      next: (response) => {
        this.productLifeCycleCategories = response
      },
      error: (error) => console.error('Error fetching product life cycle categories:', error)
    });
  }

  getAllSupplyChainPartner(){
    this.contractService.getAllSupplyChainPartner().subscribe({
      next: (response) => {
        this.supplyChainPartners = response
        this.getAllProductLifeCycleCategories()
       },
      error: (error) => console.error('Error fetching supply chain partners:', error)
    });
  }

  onSelectFile(event : Event){
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.fileUploaded = input.files[0]
      if(this.fileUploaded.type !== "application/pdf"){
        alert("Only PDF allowed")
        return
      }
      this.uploadEnabled = true
    }else{
      alert("Upload a file!")
    }
  }

  createContract(): void {
    const reader = new FileReader();

    reader.onload = () => {
      const base64String = reader.result?.toString().split(',')[1]!;
      const hashedBase64Contract = sha256(base64String)

      const contract: Contract = {
        fileString: base64String,
        hash: hashedBase64Contract,
        supplyChainPartnerReceiverId: this.selectedReceiverId,
        userEmitterId: this.authService.userId!,
        type: 'contract',
        productLifeCycleCategoryId: this.selecteCategoryId
      };

      this.contractService.addContract(contract).subscribe({
        next: (response) => console.log('Contract uploaded successfully', response),
        error: (error) => console.error('Contract upload failed', error),
      });

    };

    reader.readAsDataURL(this.fileUploaded);
  }

  reset(){
    this.fileInput.nativeElement.value = null
    this.uploadEnabled = false
  }

}
