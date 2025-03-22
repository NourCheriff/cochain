
import { ChangeDetectionStrategy, Component, computed, ElementRef, inject, signal, ViewChild, AfterViewInit, Inject, OnInit } from '@angular/core';
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
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Product } from 'src/models/product/product.model';
import { ProductCategory } from 'src/models/product/product-category.model';
import { ProductIngredient } from 'src/models/product/product-ingredient.model';
import { ProductInfo } from 'src/models/product/product-info.model';
import { ProductDocument } from 'src/models/documents/product-document.model';
import { ProductService } from '../../services/product.service';
import { DatePipe } from '@angular/common';
import { sha256 } from 'js-sha256';
import { DocumentType } from 'src/types/document.enum';
import { AuthService } from 'src/app/core/services/auth.service';
@Component({
  selector: 'app-edit-product-dialog',
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
  templateUrl: './edit-product-dialog.component.html',
  styleUrl: './edit-product-dialog.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class EditProductDialogComponent implements OnInit{

  private authService = inject(AuthService)

  constructor(@Inject(MAT_DIALOG_DATA) public data: {product: ProductInfo, ingredients: ProductInfo[]}, private productService: ProductService) {
    const INGREDIENTS = this.data.ingredients.map(ingredient => ingredient.name).filter((name): name is string => name !== undefined);
    this.modifiedProductForm.addControl('name', new FormControl<string>(this.data.product.name!, Validators.required));
    this.modifiedProductForm.addControl('product', new FormControl<string>(this.data.product.product?.id!, Validators.required));
    this.modifiedProductForm.addControl('date', new FormControl<Date>(new Date(this.data.product.expirationDate), Validators.required));
    this.modifiedProductForm.addControl('category', new FormControl<string>(this.data.product.product?.category?.id!, Validators.required));
    this.modifiedProductForm.addControl('hasIngredients', new FormControl<boolean>((INGREDIENTS.length > 0)? true : false, Validators.required));
    if(INGREDIENTS.length > 0){
      this.loadIngredients(INGREDIENTS);
      this.modifiedProductForm.addControl('ingredients', new FormControl<string>(""));
    }
  }

  readonly allIngredients: string[] = [];
  readonly ingredients = signal<string[]>([]);
  readonly announcer = inject(LiveAnnouncer);
  readonly dialogRef = inject(MatDialogRef<EditProductDialogComponent>);

  @ViewChild("fileInput") fileInput!: ElementRef
  fileUploaded!: File
  uploadEnabled: boolean = false;

  modifiedProductForm = new FormGroup<ProductForm>({});
  genericProducts: Product[] = [];
  productCategories: ProductCategory[] = [];
  allIngredientsRes: ProductInfo[] = [];
  alreadyLoaded: boolean = false;
  hashedOriginDocument: string = "";

  ngOnInit(): void {
    this.loadProductCategories();
  }

  private loadProductCategories(): void {
    this.productService.getProductCategories().subscribe({
      next: (response) => {
        this.productCategories = response;
        this.loadGenericProducts();
      },
      error: (error) => console.error('Error during retrieval of products.', error)
    });
  }


  loadGenericProducts(){
    this.productService.getAllGenericProducts(this.modifiedProductForm.getRawValue().category!).subscribe({
      next: (response) => {
        this.genericProducts = response;
      },
      error: (error) => console.error(error)
    });
  }

  loadIngredientsOnce(){
    if(!this.alreadyLoaded){
      this.loadIngredients(null);
      this.alreadyLoaded = true;
    }
  }

  private loadIngredients(ingredients: string[] | null): void {
    if(this.allIngredientsRes != null)
      this.productService.getAllProductInfo().subscribe({
        next: (response) => {

          this.allIngredientsRes = response.items!;
          this.allIngredientsRes.forEach(ingredient =>{

            if (ingredient.id != this.data.product.id) {
              this.allIngredients.push(ingredient.name!);
            }
          })

          if(ingredients != null){
            this.ingredients.set(ingredients);
          }
        },
        error: (error) => console.error(error)
      });
  }

  modifyProduct(){
    const ingredientsValue = this.ingredients();

    const productIngredients: ProductIngredient[] = ingredientsValue.map(ingredientName => {
      const ingredient = this.allIngredientsRes.find(item => item.name === ingredientName);
      return ingredient ? { ingredientId: ingredient.id }: null;
    }).filter((ingredient): ingredient is ProductIngredient => ingredient !== null);

    const datepipe: DatePipe = new DatePipe('en-US')
    let formattedDate = datepipe.transform(this.modifiedProductForm.value.date!, 'YYYY-MM-dd');

    const modifiedProduct: ProductInfo = {
      id: this.data.product.id,
      name: this.modifiedProductForm.value.name!,
      productId: this.modifiedProductForm.value.product!,
      product: {
        id: this.genericProducts.find(x => x.id === this.modifiedProductForm.value.product!)?.id,
        name: this.genericProducts.find(x => x.id === this.modifiedProductForm.value.product!)?.name,
        description: this.genericProducts.find(x => x.id === this.modifiedProductForm.value.product!)?.description,
        categoryId : this.modifiedProductForm.value.category!,
        category : this.productCategories.find(x => x.id === this.modifiedProductForm.value.category!),
      },
      supplyChainPartnerId: this.data.product.supplyChainPartnerId,
      expirationDate: formattedDate!,
      ingredients: productIngredients,
    }

    this.productService.updateProductInfo(modifiedProduct).subscribe({
      next: (response) => {
        let originDocument = response.productDocuments!.find(x => x.type === DocumentType.Origin);

        this.productService.deleteDocument(originDocument!.id!, DocumentType.Origin).subscribe({
          next: (response) => {},
          error: (error) => console.error(error),
        })

        this.uploadFile(response.id!);
        this.dialogRef.close({ modifiedProduct: modifiedProduct });},
      error: (error) => console.error(error),
    })
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
      const hashedBase64Document= sha256(base64String!)

      let originDocument: ProductDocument = {
        hash: hashedBase64Document,
        fileString: base64String,
        productInfoId: productInfoId,
        userEmitterId: this.authService.userId!,
        supplyChainPartnerReceiverId: this.data.product.supplyChainPartnerId,
        type: DocumentType.Origin,
      };

      this.productService.uploadOriginDocument(originDocument).subscribe({
        next: (response) => console.log('File uploaded successfully', response),
        error: (error) => console.error('File upload failed', error),
      });
    };

    reader.readAsDataURL(this.fileUploaded);
  }

  reset(){
    this.fileInput.nativeElement.value = null
    this.uploadEnabled = false
  }
}

interface ProductForm {
  name?: FormControl<string | null>;
  product?: FormControl<string | null>;
  category?: FormControl<string | null>;
  date?: FormControl<Date | null>;
  hasIngredients?: FormControl<boolean | null>;
  ingredients?: FormControl<string | null>;
}
