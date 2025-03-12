import { ChangeDetectionStrategy, Component, computed, ElementRef, inject, model, signal, ViewChild } from '@angular/core';
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
import { FileUploadService } from 'src/app/core/services/fileUpload.service';
import { Product } from 'src/models/product/product.model';
import { ProductCategory } from 'src/models/product/product-category.model';
import { ProductInfo } from 'src/models/product/product-info.model';
import { ProductService } from '../../services/product.service';

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
export class ProductDialogComponent {
  readonly dialogRef = inject(MatDialogRef<ProductDialogComponent>);
  hasIngredients: boolean = false;

  newProductForm = new FormGroup({
    name: new FormControl('', [Validators.required]),
    date: new FormControl('', [Validators.required]),
    hasIngredients: new FormControl(false),
    ingredients: new FormControl('')
  });

  @ViewChild("fileInput") fileInput!: ElementRef
  fileUploaded!: File
  uploadEnabled: boolean = false;

  readonly announcer = inject(LiveAnnouncer);

  readonly ingredients = signal<string[]>([]);
  readonly allIngredients: string[] = ['Ingredient1', 'Ingredient2', 'Ingredient3', 'Ingredient4', 'Ingredient5'];

  constructor(private productService: ProductService) {}
  // get from API
  readonly products: Option[] = [
    { value: 'name1', displayValue: 'name1' },
    { value: 'name2', displayValue: 'name2' },
    { value: 'name3', displayValue: 'name3' },
    { value: 'name4', displayValue: 'name4' },
    { value: 'name5', displayValue: 'name5' },
  ];

  readonly filteredIngredients = computed(() => {
    return this.ingredients().length === 0
      ? this.allIngredients
      : this.allIngredients.filter((ingredient: string) => !this.ingredients().includes(ingredient));
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


  createProduct(){
    const productName = this.newProductForm.value.name
    const date = this.newProductForm.value.date
    if(this.newProductForm.value.hasIngredients){
      const ingredients: string[] = this.ingredients()
    }

    const reader = new FileReader();
    reader.onload = () => {
      const base64String = reader.result?.toString().split(',')[1]; // Rimuove il prefisso 'data:...;base64,'

      let category: ProductCategory = {
        description: 'prova'
      }

      let product: Product = {
        description:'prova',
        category: category,
      }

      let productInfo: ProductInfo = {
        name: productName!,
        product: product,
        expirationDate: date!,
      }

      const res = this.productService.addProduct(productInfo)
      console.log(res)
      // this.fileUploadService.uploadFile(doc).subscribe({
      //   next: (response) => console.log('File uploaded successfully', response),
      //   error: (error) => console.error('File upload failed', error),
      // });
    };

    reader.readAsDataURL(this.fileUploaded);
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

   reset(){
    this.fileInput.nativeElement.value = null
    this.uploadEnabled = false
  }
}

interface Option {
  value: string;
  displayValue: string;
}
