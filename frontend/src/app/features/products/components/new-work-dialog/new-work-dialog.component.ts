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
import { ProductLifeCycleDocument } from 'src/models/documents/product-life-cycle-document.model';
import { sha256 } from 'js-sha256';
import { AuthService } from 'src/app/core/services/auth.service';
import { DocumentType } from 'src/types/document.enum';
import { BlockchainService } from 'src/app/features/wallet/services/blockchain.service';
import { ToastrService } from 'ngx-toastr';
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
  private authService = inject(AuthService);
  private blockchainService = inject(BlockchainService);
  private toasterService = inject(ToastrService);
  readonly dialogRef = inject(MatDialogRef<NewWorkDialogComponent>);

  @ViewChild('billFile') billFile!: ElementRef;
  @ViewChild('transportFile') transportFile!: ElementRef;
  @ViewChild('emissions') emissions!: ElementRef;

  selectedWorkType: string = '';
  isTransportDocument: boolean = false;

  newWorkForm = new FormGroup({
    work: new FormControl('', Validators.required),
    workDate: new FormControl(new Date(Date.now()),[Validators.required])
  });

  billFileUploaded!: File;
  transportFileUploaded!: File;
  uploadEnabled: boolean = false;

  productLifeCycleCategories: ProductLifeCycleCategory[] = [];
  emissionsValue: number = 0;

  ngOnInit(): void {
    this.getAllProductLifeCycleCategories()
  }

  ngAfterViewInit(): void {
    this.emissionsValue = this.getRandomInt(0, 100);
    this.emissions.nativeElement.textContent = `${this.emissionsValue}T CO2e `;
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
      supplyChainPartnerId: this.data.product.supplyChainPartnerId!,
      productInfoId: this.data.product.id!,
      productInfo: this.data.product
    }

    if (this.blockchainService.isWalletConnected()) {
      if (this.isTransportDocument) {
        this.productService.addProductLifeCycleTransport(newProductLifeCycle).subscribe({
          next: (response) => {
            this.uploadFile(response.id!, true);
            this.uploadFile(response.id!, false);
            this.dialogRef.close({ newWork: response });
            this.blockchainService.addActivity(Number(newProductLifeCycle.productInfo?.tokenId), response.id!, response.emissions).then((item) => {
              console.log("Activity added to the product", item)
            })
          },
          error: (error) => console.error(error),
        })
      }
      else {
        this.productService.addProductLifeCycleGeneric(newProductLifeCycle).subscribe({
          next: (response) => {
            this.uploadFile(response.id!, false);
            this.dialogRef.close({ newWork: response });
            this.blockchainService.addActivity(Number(newProductLifeCycle.productInfo?.tokenId), response.id!, response.emissions).then((item) => {
              console.log("Activity added to the product", item)
            })
          },
          error: (error) => console.error(error),
        })
      }
    }
    else {
      this.toasterService.error("Wallet not connected", 'Error');
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

  uploadFile(newWorkId: string, isTransportDocument: boolean): void {
    const reader = new FileReader();
    reader.onload = () => {
      const base64String = reader.result?.toString().split(',')[1]!;
      const hashedBase64Document= sha256(base64String!)

      let lifeCycleDocument: ProductLifeCycleDocument = {
        hash: hashedBase64Document,
        fileString: base64String,
        productLifeCycleId: newWorkId,
        supplyChainPartnerReceiverId: this.data.product.supplyChainPartnerId!,
        userEmitterId: this.authService.userId!,
        type: (isTransportDocument) ? DocumentType.Transport : DocumentType.Invoice,
      };

      this.productService.uploadLifeCycleDocument(lifeCycleDocument).subscribe({
        next: (response) => console.log('File uploaded successfully', response),
        error: (error) => console.error('File upload failed', error),
      });

      this.blockchainService.addDocument(Number(this.data.product.tokenId), hashedBase64Document).then((item) => {
        console.log("Document added to the product", item);
      });
    };

    reader.readAsDataURL((isTransportDocument) ? this.transportFileUploaded : this.billFileUploaded);
  }

  reset(fileType: 'invoice' | 'transport') {
    if(fileType === DocumentType.Invoice){
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
    const selectedCategory = this.productLifeCycleCategories.find(category => category.id === this.selectedWorkType);

    return selectedCategory!.name?.toLowerCase() === "transport" ? !!this.billFileUploaded && !!this.transportFileUploaded : !!this.billFileUploaded;
  }

  onSelectionChange(value: string) {
    const selectedCategory = this.productLifeCycleCategories.find(category => category.id === value);
    if(selectedCategory)
      this.isTransportDocument = selectedCategory.name!.toLowerCase() === "transport";
  }

  private getRandomInt(min: number, max: number): number{
    const minCeiled = Math.ceil(min);
    const maxFloored = Math.floor(max);
    return Math.floor(Math.random() * (maxFloored - minCeiled) + minCeiled);
  }
}

