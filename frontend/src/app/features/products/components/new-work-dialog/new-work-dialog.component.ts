import { Component, inject, ChangeDetectionStrategy, ViewChild, ElementRef, OnInit, AfterViewInit} from '@angular/core';
import {
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle,
} from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatInputModule} from '@angular/material/input';
import {MatSelectModule} from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';
import {provideNativeDateAdapter} from '@angular/material/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators,AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';
import { ProductLifeCycleCategory } from 'src/models/product/product-life-cycle-category.model';
import { ProductService } from '../../services/product.service';
import { SupplyChainPartner } from 'src/models/company-entities/supply-chain-partner.model';
import { ProductLifeCycle } from 'src/models/product/product-life-cycle.model';

@Component({
  selector: 'app-new-work-dialog',
    imports: [
      MatButtonModule,
      MatIconModule,
      MatSelectModule,
      MatDialogTitle,
      MatDialogContent,
      MatFormFieldModule,
      MatInputModule,
      MatDatepickerModule,
      ReactiveFormsModule,
      CommonModule
    ],
  templateUrl: './new-work-dialog.component.html',
  providers: [provideNativeDateAdapter()],
  styleUrl: './new-work-dialog.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class NewWorkDialogComponent implements OnInit, AfterViewInit {

  readonly dialogRef = inject(MatDialogRef<NewWorkDialogComponent>);

  @ViewChild('billFile') billFile!: ElementRef;
  @ViewChild('transportFile') transportFile!: ElementRef;
  @ViewChild('emissions') emissions!: ElementRef;

  selectedReceiver: string = '';
  selectedWorkType: string = '';
  isReceiverVisible: boolean = false;

  newWorkForm = new FormGroup({
    work: new FormControl('', Validators.required),
    receiver: new FormControl('', this.receiverValidator()),
    workDate: new FormControl(Date.now(),[Validators.required])
  });

  billFileUploaded!: File;
  transportFileUploaded!: File;
  uploadEnabled: boolean = false;

  productLifeCycleCategories: ProductLifeCycleCategory[] = [];
  supplyChainPartners: SupplyChainPartner[] = [];

  constructor(private productService: ProductService) {}

  ngAfterViewInit(): void {
     this.emissions.nativeElement.textContent = `${this.getRandomInt(0, 100)}T CO2e `;
  }

  ngOnInit(): void {
    this.getAllProductLifeCycleCategories()
  }

  getAllProductLifeCycleCategories(){
    this.productService.getAllProductLifeCycleCategories().subscribe({
      next: (response) => {
        this.productLifeCycleCategories = response
      },
      error: (error) => console.error(error)
    })
  }


  getAllSupplyChainPartner(){
    this.productService.getAllSupplyChainPartner().subscribe({
      next: (response) => {
        this.supplyChainPartners = response
      },
      error: (error) => console.error(error)
    })
  }

  createWork(): void {
    if(this.isReceiverVisible){
     const receiver = this.newWorkForm.value.receiver
    }

    const fileData = new FormData()
    fileData.append('bill', this.billFileUploaded)
    if(this.isReceiverVisible){
      fileData.append('transport', this.transportFileUploaded)
    }

    // const productLifeCycle: ProductLifeCycle = {
    //   emissions: this.emissions.nativeElement.value,
    //   timestamp: Date.now().toString(),
    //   productLifeCycleCategory: this.newWorkForm.value.work,
    //   productInfo: productId,
    //   supplyChainPartner: receiver || currentUser
    // }

    // this.fileUploadService.uploadFile(file).subscribe({
    //   next: (response) => {
    //     console.log('File uploaded successfully', response);
    //   },
    //   error: (error) => {
    //     console.error('File upload failed', error);
    //   },
    // });

  }

  onSelectFile(event : Event){
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      const file = input.files[0]
      if(file.type !== "application/pdf"){
        alert("Only PDF allowed")
        return
      }
      input.id === "billUp" ? this.billFileUploaded = file : this.transportFileUploaded = file;
    }else{
      alert("Upload a file!")
    }
  }

  reset(fileType: 'bill' | 'transport') {
    if(fileType === 'bill'){
      this.billFileUploaded = undefined!
      this.billFile.nativeElement.value = null;
    }else{
      this.transportFileUploaded = undefined!
      this.transportFile.nativeElement.value = null;
    }
  }

  isFormValid(): boolean {
    if (!this.newWorkForm.valid) {
      return false;
    }
    return this.selectedWorkType === 'Transport' ? !!this.billFileUploaded && !!this.transportFileUploaded : !!this.billFileUploaded;
  }

  onSelectionChange(value: string) {
    this.selectedWorkType = value;
    this.isReceiverVisible = value === 'Transport';
    if (this.isReceiverVisible) {
      this.getAllSupplyChainPartner();
    }
  }

  private getRandomInt(min: number, max: number): number{
    const minCeiled = Math.ceil(min);
    const maxFloored = Math.floor(max);
    return Math.floor(Math.random() * (maxFloored - minCeiled) + minCeiled);
  }

  private receiverValidator(): ValidatorFn {
    return (control:AbstractControl) : ValidationErrors | null => {

      const value = control.value;
      const workValue = this.newWorkForm?.value?.work;

      // If the work value is "transport", receiver must be provided
      if (workValue === "Transport" && (!value || value === '')) {
        return { receiverRequired: true };
      }

      return null;
    }
  }
}

