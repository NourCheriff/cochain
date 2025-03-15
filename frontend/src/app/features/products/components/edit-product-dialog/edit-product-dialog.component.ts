
import { ChangeDetectionStrategy, Component, computed, ElementRef, inject, signal, ViewChild, AfterViewInit, Inject } from '@angular/core';
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
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ProductInfo } from 'src/models/product/product-info.model'
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
export class EditProductDialogComponent {

  readonly dialogRef = inject(MatDialogRef<EditProductDialogComponent>);

  @ViewChild("fileInput") fileInput!: ElementRef
  fileUploaded!: File
  uploadEnabled: boolean = false;

  readonly announcer = inject(LiveAnnouncer);

  // get from API
  readonly products: Option[] = [
    { value: 'Uova', displayValue: 'Uova' },
    { value: 'Farina', displayValue: 'Farina' },
    { value: 'Pasta', displayValue: 'Pasta' },
    { value: 'Pizza', displayValue: 'Pizza' },
    { value: 'Mozzarella', displayValue: 'Mozzarella' },
  ];

  readonly ingredients = signal<string[]>([]);
  // get from API
  readonly allIngredients: string[] = ['Pizza', 'Pasta', 'Farina', 'Uova', 'Mozzarella'];

  readonly filteredIngredients = computed(() => {
    return this.ingredients().length === 0
      ? this.allIngredients
      : this.allIngredients.filter((ingredient: string) => !this.ingredients().includes(ingredient));
  });

  modifiedProductForm = new FormGroup<ProductForm>({});

  constructor(@Inject(MAT_DIALOG_DATA) public data: {product: ProductInfo}, private fileUploadService: FileUploadService) {
    const INGREDIENTS = this.data.product.ingredients!.map(ingredient => ingredient.ingredient!.name).filter((name): name is string => name !== undefined);
    this.modifiedProductForm.addControl('name', new FormControl<string>(data.product.name!, Validators.required));
    this.modifiedProductForm.addControl('date', new FormControl<Date>(new Date(this.data.product.expirationDate), Validators.required));
    this.modifiedProductForm.addControl('hasIngredients', new FormControl<boolean>((INGREDIENTS.length > 0)? true : false, Validators.required));
    this.ingredients.set(INGREDIENTS);
  }

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


  modifyProduct(){
    const modifiedProductName = this.modifiedProductForm.value.name
    const modifiedDate = this.modifiedProductForm.value.date
    if(this.modifiedProductForm.value.hasIngredients){
      const modifiedIngredients: string[] = this.ingredients()
      console.log(modifiedIngredients)
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

interface ProductForm {
  name?: FormControl<string | null>;
  date?: FormControl<Date | null>;
  hasIngredients?: FormControl<boolean | null>;
}
