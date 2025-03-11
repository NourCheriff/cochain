
import { ChangeDetectionStrategy, Component, computed, ElementRef, inject, signal, ViewChild, AfterViewInit } from '@angular/core';
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
import { ProductInfo } from 'src/models/product/product-info.model'
import { Product } from 'src/models/product/product.model'
import { ProductCategory } from 'src/models/product/product-category.model';
import { ProductIngredient } from 'src/models/product/product-ingredient.model';
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
export class EditProductDialogComponent implements AfterViewInit {

  readonly dialogRef = inject(MatDialogRef<EditProductDialogComponent>);
  hasIngredients:boolean = false;
  defaultName:string = "";
  defaultDate: Date = new Date();

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
  readonly allIngredients: string[] = ['Ingredient1', 'Ingredient2', 'Farina', 'Uova', 'Ingredient5'];

  ngAfterViewInit() {
    // PRODUCT_INFO_3 will be the loaded product
    PRODUCT_INFO_3.ingredients!.forEach(ingredient => {
      // load default ingredients in component
      const ingredientNames = PRODUCT_INFO_3.ingredients!.map(ingredient => ingredient.ingredient!.name).filter((name): name is string => name !== undefined);;
      if(ingredientNames.length > 0){
        this.ingredients.set(ingredientNames);
        this.newProductForm.value.hasIngredients = true;
        this.hasIngredients = true;
      }
      else{
        this.newProductForm.value.hasIngredients = false;
        this.hasIngredients = false;
      }
    });
    this.defaultName = PRODUCT_INFO_3.name!;
    this.defaultDate = new Date(PRODUCT_INFO_3.expirationDate);
    console.log(this.defaultDate)
  }



  constructor(private fileUploadService: FileUploadService) {}
  // get from API
  readonly products: Option[] = [
    { value: 'name1', displayValue: 'name1' },
    { value: 'name2', displayValue: 'name2' },
    { value: 'Pasta', displayValue: 'Pasta' },
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
      console.log(ingredients)
    }

    const fileData = new FormData()
    fileData.append('file',this.fileUploaded)

    for (const pair of fileData.entries()) {
      console.log(pair[0], pair[1]);
    }
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

const CATEGORY_1: ProductCategory = {
    description: 'Materie Prime'
  }
const CATEGORY_2: ProductCategory = {
  description: 'Materie Lavorata'
}

const PRODUCT_1: Product = {
  description: 'Farina grano duro 00',
  category: CATEGORY_1
}

const PRODUCT_2: Product = {
  description: 'Uova confezione da 6',
  category: CATEGORY_1
}

const PRODUCT_3: Product = {
  description: 'Pasta 500g',
  category: CATEGORY_2
}

const PRODUCT_INFO_1: ProductInfo = {
  id: "1",
  name: "Farina",
  product: PRODUCT_1,
  expirationDate:  "17-04-2025",
  ingredients: [{}],
}

const INGREDIENT_1: ProductIngredient = {
  ingredient: PRODUCT_INFO_1
}

const PRODUCT_INFO_2: ProductInfo = {
  id: "2",
  name: "Uova",
  product: PRODUCT_2,
  expirationDate:  "17-04-2025",
  ingredients: [{}],
}

const INGREDIENT_2: ProductIngredient = {
  ingredient: PRODUCT_INFO_2
}

const PRODUCT_INFO_3: ProductInfo = {
  id: "3",
  name: "Pasta",
  product: PRODUCT_3,
  expirationDate:  "04/17/2025",
  ingredients: [INGREDIENT_1, INGREDIENT_2],
}
