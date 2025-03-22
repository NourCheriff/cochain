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
import { ProductLifeCycleCategory } from 'src/models/product/product-life-cycle-category.model';
import { ProductService } from '../../services/product.service';
import { ProductLifeCycle } from 'src/models/product/product-life-cycle.model';
import { ProductInfo } from 'src/models/product/product-info.model';
import { DatePipe } from '@angular/common';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
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

  constructor(@Inject(MAT_DIALOG_DATA) public data: {product: ProductInfo}, private productService: ProductService) {}

  readonly dialogRef = inject(MatDialogRef<NewWorkDialogComponent>);

  @ViewChild('billFile') billFile!: ElementRef;
  @ViewChild('transportFile') transportFile!: ElementRef;
  @ViewChild('emissions') emissions!: ElementRef;

  selectedReceiver: string = '';
  selectedWorkType: string = '';
  isTransportDocument: boolean = false;

  newWorkForm = new FormGroup({
    work: new FormControl('', Validators.required),
    receiver: new FormControl('', this.receiverValidator()),
    workDate: new FormControl(new Date(Date.now()),[Validators.required])
  });

  billFileUploaded!: File;
  transportFileUploaded!: File;
  uploadEnabled: boolean = false;

  productLifeCycleCategories: ProductLifeCycleCategory[] = [];
  emissionsValue: any;

  ngAfterViewInit(): void {
    this.emissionsValue = this.getRandomInt(0, 100);
    this.emissions.nativeElement.textContent = `${this.emissionsValue}T CO2e `;
  }

  ngOnInit(): void {
    this.getAllProductLifeCycleCategories()
  }

  getAllProductLifeCycleCategories(){
    this.productService.getAllProductLifeCycleCategories().subscribe({
      next: (response) => {
        this.productLifeCycleCategories = response;
      },
      error: (error) => console.error(error)
    })
  }

  createWork(): void {
    const datepipe: DatePipe = new DatePipe('en-US')
    let formattedDate = datepipe.transform(this.newWorkForm.value.workDate, 'YYYY-MM-dd') + "T00:00:00Z";

    const newProductLifeCycle: ProductLifeCycle = {
      timestamp: formattedDate!,
      emissions: this.emissionsValue,
      isEmissionsProcessed: false,
      productLifeCycleCategoryId: this.newWorkForm.value.work!,
      supplyChainPartnerId: 'd65e685f-8bdd-470b-a6b8-c9a62e39f095',
      productInfoId: this.data.product.id!,
    }

    if(this.isTransportDocument){
      const receiver = this.newWorkForm.value.receiver
      this.productService.addProductLifeCycleTransport(newProductLifeCycle).subscribe({
        next: (response) => this.dialogRef.close({ newWork: response }),
        error: (error) => console.error(error),
      })
    }
    else{
      this.productService.addProductLifeCycleGeneric(newProductLifeCycle).subscribe({
        next: (response) => this.dialogRef.close({ newWork: response }),
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
    }
    else{
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
    this.isTransportDocument = value === '7a286d32-f89b-4e86-88bc-a6eb32fa2132';
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

