import { ChangeDetectionStrategy, Component, computed, ElementRef, inject, model, OnInit, signal, ViewChild } from '@angular/core';
import { MatDialogContent, MatDialogRef, MatDialogTitle } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { provideNativeDateAdapter } from '@angular/material/core';
import { LiveAnnouncer } from '@angular/cdk/a11y';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatAutocompleteModule, MatAutocompleteSelectedEvent} from '@angular/material/autocomplete';
import { MatChipInputEvent, MatChipsModule} from '@angular/material/chips';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { Product } from 'src/models/product/product.model';
import { ProductCategory } from 'src/models/product/product-category.model';
import { ProductIngredient } from 'src/models/product/product-ingredient.model';
import { ProductInfo } from 'src/models/product/product-info.model';
import { ProductDocument } from 'src/models/documents/product-document.model';
import { ProductService } from '../../services/product.service';
import { DatePipe } from '@angular/common';
import { sha256 } from 'js-sha256';
import { AuthService } from 'src/app/core/services/auth.service';
import { DocumentType } from 'src/types/document.enum';
import { BlockchainService } from 'src/app/features/wallet/services/blockchain.service';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-product-dialog',
  imports: [
    MatInputModule,
    MatButtonModule,
    MatDialogTitle,
    MatDialogContent,
    MatSelectModule,
    MatFormFieldModule,
    MatIconModule,
    MatDatepickerModule,
    MatChipsModule,
    MatAutocompleteModule,
    MatSlideToggleModule,
    FormsModule,
    ReactiveFormsModule
  ],

  providers: [provideNativeDateAdapter()],
  templateUrl: './product-dialog.component.html',
  styleUrl: './product-dialog.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ProductDialogComponent implements OnInit {

  constructor(private productService: ProductService, private blockchainService: BlockchainService) {

  }
  private authService = inject(AuthService);
  private toasterService = inject(ToastrService);

  readonly dialogRef = inject(MatDialogRef<ProductDialogComponent>);
  readonly announcer = inject(LiveAnnouncer);
  readonly ingredients = signal<string[]>([]);
  readonly allIngredients: string[] = [];

  @ViewChild("fileInput") fileInput!: ElementRef
  fileUploaded!: File
  uploadEnabled: boolean = false;

  alreadyLoaded: boolean = false;
  hasIngredients: boolean = false;
  genericProducts: Product[] = [];
  allIngredientsRes: ProductInfo[] = [];
  productCategories: ProductCategory[] = [];

  newProductForm = new FormGroup({
    category: new FormControl('', [Validators.required]),
    product: new FormControl('', [Validators.required]),
    name: new FormControl('', [Validators.required]),
    date: new FormControl('', [Validators.required]),
    hasIngredients: new FormControl(false),
    ingredients: new FormControl('')
  });

  ngOnInit(): void {
    this.loadProductCategories();
  }

  private loadProductCategories(): void {
    this.productService.getProductCategories().subscribe({
      next: (response) => {this.productCategories = response},
      error: (error) => console.error('Errore nel recupero dei prodotti:', error)
    });
  }

  loadIngredientsOnce(){
    if(!this.alreadyLoaded){
      this.loadIngredients();
      this.alreadyLoaded = true;
    }
  }

  private loadIngredients(): void {
    if(this.allIngredientsRes != null)
      this.productService.getAllProductInfo().subscribe({
        next: (response) => {
          this.allIngredientsRes = response.items!
          this.allIngredientsRes.forEach(ingredient =>{
            this.allIngredients.push(ingredient.name!)
          })
        },
        error: (error) => console.error(error)
      });
  }

  loadGenericProducts(){
    this.productService.getAllGenericProducts(this.newProductForm.getRawValue().category!).subscribe({
      next: (response) => {
        this.genericProducts = response
      },
      error: (error) => console.error(error)
    });
  }

  createProduct(){
    const ingredientsValue = this.ingredients();

    const productIngredients: ProductIngredient[] = ingredientsValue.map(ingredientName => {
      const ingredient = this.allIngredientsRes.find(item => item.name === ingredientName);
      return ingredient ? { ingredientId: ingredient.id }: null;
    }).filter((ingredient): ingredient is ProductIngredient => ingredient !== null);

    const datepipe: DatePipe = new DatePipe('en-US')
    let formattedDate = datepipe.transform(this.newProductForm.value.date!, 'YYYY-MM-dd');

    const newProduct: ProductInfo = {
      name: this.newProductForm.value.name!,
      productId: this.newProductForm.value.product!,
      expirationDate: formattedDate!,
      ingredients: productIngredients,
    }

    if(this.blockchainService.isWalletConnected()) {
      this.productService.addProductInfo(newProduct).subscribe({
        next: (response) => {
          this.uploadFile(response.id!);
          this.dialogRef.close({ newProduct: response });
          this.blockchainService.createProduct(response.id!, response.expirationDate).then((item) => {
            const tokenId = Number(item!.logs[0].topics[3]);
            this.updateProduct(response.id!, tokenId)
            console.log("Prodott added correctly", item)
          });
        },
        error: (error) => console.error(error),
      })
    }
    else {
      this.toasterService.error("Wallet not connected", 'Error');
    }
  }


  readonly filteredIngredients = computed(() => {
    return this.ingredients().length === 0
      ? this.allIngredients
      : this.allIngredients.filter((ingredient: string) =>
          !this.ingredients().includes(ingredient)
      );
    });

  add(event: MatChipInputEvent): void {
    const value = (event.value || '').trim();
    if (value) {
      this.ingredients.update(ingredients => [...ingredients, value]);
    }
  }

  remove(ingredient: string): void {
    this.ingredients.update(ingredients => {
      const index = ingredients.indexOf(ingredient);
      if (index < 0) {
        return ingredients;
      }

      ingredients.splice(index, 1);
      this.announcer.announce(`Removed ${ingredient}`);
      return [...ingredients];
    });
  }

  selected(event: MatAutocompleteSelectedEvent): void {
    this.ingredients.update(ingredients => [...ingredients, event.option.viewValue]);
    event.option.deselect();
  }

  dateFilter = (d: Date | null): boolean => {
    const today = new Date();
    today.setHours(0, 0, 0, 0);
    return d ? d >= today : false;
  };

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

  uploadFile(productInfoId: string): void {
    const reader = new FileReader();
    reader.onload = () => {
      const base64String = reader.result?.toString().split(',')[1]!;
      const hashedBase64Contract = sha256(base64String!)

      let originDocument: ProductDocument = {
        hash: hashedBase64Contract,
        fileString: base64String,
        productInfoId: productInfoId,
        userEmitterId: this.authService.userId!,
        type: DocumentType.Origin,
      };

      this.productService.uploadOriginDocument(originDocument).subscribe({
        next: (response) => console.log('File uploaded successfully', response),
        error: (error) => console.error('File upload failed', error),
      });
    };

    reader.readAsDataURL(this.fileUploaded);
  }

  private updateProduct(productInfoId: string, tokenId: number) {
    const ingredientsValue = this.ingredients();

    const productIngredients: ProductIngredient[] = ingredientsValue.map(ingredientName => {
      const ingredient = this.allIngredientsRes.find(item => item.name === ingredientName);
      return ingredient ? { ingredientId: ingredient.id }: null;
    }).filter((ingredient): ingredient is ProductIngredient => ingredient !== null);

    const datepipe: DatePipe = new DatePipe('en-US')
    let formattedDate = datepipe.transform(this.newProductForm.value.date!, 'YYYY-MM-dd');

    const newProduct: ProductInfo = {
      id: productInfoId,
      name: this.newProductForm.value.name!,
      productId: this.newProductForm.value.product!,
      expirationDate: formattedDate!,
      ingredients: productIngredients,
      tokenId: tokenId.toString(),
    }

    this.productService.updateProductInfo(newProduct).subscribe();
  }

  reset(){
    this.fileInput.nativeElement.value = null
    this.uploadEnabled = false
  }
}


