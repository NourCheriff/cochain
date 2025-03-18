import { Component, inject, Inject, ChangeDetectionStrategy, ViewChild, ElementRef, OnInit, AfterViewInit } from '@angular/core';
import { MatDialogContent, MatDialogRef, MatDialogTitle } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { provideNativeDateAdapter } from '@angular/material/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators,AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';
import { FileUploadService } from 'src/app/core/services/fileUpload.service';
import { ProductLifeCycleCategory } from 'src/models/product/product-life-cycle-category.model';
import { ProductService } from '../../services/product.service';
import { SupplyChainPartner } from 'src/models/company-entities/supply-chain-partner.model';
import { ProductLifeCycle } from 'src/models/product/product-life-cycle.model';
import { ProductInfo } from 'src/models/product/product-info.model';
import { DatePipe } from '@angular/common';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CompanyService } from '../../../users/services/company.service';
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
    workDate: new FormControl(new Date(Date.now()),[Validators.required])
  });

  billFileUploaded!: File;
  transportFileUploaded!: File;
  uploadEnabled: boolean = false;

  productLifeCycleCategories: ProductLifeCycleCategory[] = [];
  supplyChainPartners: SupplyChainPartner[] = [];

  constructor(@Inject(MAT_DIALOG_DATA) public data: {product: ProductInfo}, private fileUploadService: FileUploadService, private productService: ProductService, private companyService: CompanyService) {}

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
    this.companyService.getAllSupplyChainPartners().subscribe({
      next: (response) => {
        this.supplyChainPartners = response;
      },
      error: (error) => console.log(error)
    })
  }

  createWork(): void {

    const datepipe: DatePipe = new DatePipe('en-US')
    let formattedDate = datepipe.transform(this.newWorkForm.value.workDate, 'YYYY-MM-dd');

    const newProductLifeCycle: ProductLifeCycle = {
      timestamp: formattedDate!,
      emissions: 50,
      isEmissionsProcessed: false,
      productLifeCycleCategoryId: this.newWorkForm.value.work!,
      supplyChainPartnerId: 'd65e685f-8bdd-470b-a6b8-c9a62e39f095',
      productInfoId: this.data.product.id!,
    }
    if(this.isReceiverVisible){
      const receiver = this.newWorkForm.value.receiver
      this.productService.addProductLifeCycleTransport(newProductLifeCycle).subscribe({
        next: (response) => console.log(response),
        error: (error) => console.error(error),
      })
    }
    else{
      this.productService.addProductLifeCycleGeneric(newProductLifeCycle).subscribe({
        next: (response) => console.log(response),
        error: (error) => console.error(error),
      })
    }


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
    return this.selectedWorkType === '7a286d32-f89b-4e86-88bc-a6eb32fa2132' ? !!this.billFileUploaded && !!this.transportFileUploaded : !!this.billFileUploaded;
  }

  onSelectionChange(value: string) {
    this.selectedWorkType = value;
    this.isReceiverVisible = value === '7a286d32-f89b-4e86-88bc-a6eb32fa2132';
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

      // If the work value is "7a286d32-f89b-4e86-88bc-a6eb32fa2132", receiver must be provided
      if (workValue === "7a286d32-f89b-4e86-88bc-a6eb32fa2132" && (!value || value === '')) {
        return { receiverRequired: true };
      }

      return null;
    }
  }
}

