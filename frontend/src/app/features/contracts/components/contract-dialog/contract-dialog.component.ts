import { Component, ElementRef, inject, OnInit, ViewChild } from '@angular/core';
import {
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle,
} from '@angular/material/dialog';
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
  readonly dialogRef = inject(MatDialogRef<ContractDialogComponent>);

  selectedReceiver = null;
  selectedWorkType = null;

  @ViewChild("fileInput") fileInput!: ElementRef
  fileUploaded!: File
  uploadEnabled: boolean = false;

  newContractForm = new FormGroup({
    work: new FormControl('', Validators.required),
    receiver: new FormControl('', Validators.required),
  });

  supplyChainPartners: SupplyChainPartner[] = []
  productLifeCycleCategories: ProductLifeCycleCategory[] = [];

  constructor(private contractService: ContractService) {}

  ngOnInit(): void {
    this.getAllProductLifeCycleCategories()
    this.getAllSupplyChainPartner()
  }

  getAllProductLifeCycleCategories(){
    this.contractService.getAllProductLifeCycleCategories().subscribe({
      next: (response) => { this.productLifeCycleCategories = response },
      error: (error) => console.error('Errore nel recupero dei prodotti:', error)
    });
  }

  getAllSupplyChainPartner(){
    this.contractService.getAllSupplyChainPartner().subscribe({
      next: (response) => { this.supplyChainPartners = response },
      error: (error) => console.error('Errore nel recupero dei prodotti:', error)
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
      console.log(this.fileUploaded)
      this.uploadEnabled = true
    }else{
      alert("Upload a file!")
    }
  }

  createContract(): void {
    //handle other input field
    const receiver = this.newContractForm.value.receiver
    const workType = this.newContractForm.value.work

    const reader = new FileReader();
    reader.onload = () => {
      const base64String = reader.result?.toString().split(',')[1]; // Rimuove il prefisso 'data:...;base64,'

      // const contract: Contract = {
      //   productLifeCycleCategory: this.newContractForm.value.work,
      //   fileString: base64String,
      //   supplyChainPartnerReceiverId: 'd65e685f-8bdd-470b-a6b8-c9a62e39f095',
      //   userEmitterId: '3542da56-0de3-4797-a059-effff257f63d',
      //   type: 'contract',
      // };

      // this.contractService.addContract(contract).subscribe({
      //   next: (response) => console.log('Contract uploaded successfully', response),
      //   error: (error) => console.error('Contract upload failed', error),
      // });

    };

    reader.readAsDataURL(this.fileUploaded);
  }

  reset(){
    this.fileInput.nativeElement.value = null
    this.uploadEnabled = false
  }

}
