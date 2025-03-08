import { ChangeDetectionStrategy, Component, computed, inject, model, signal } from '@angular/core';
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
    ingredients: new FormControl(''),
    file: new FormControl(''),
  });

  readonly announcer = inject(LiveAnnouncer);

  readonly ingredients = signal<string[]>([]);
  readonly allIngredients: string[] = ['Ingredient1', 'Ingredient2', 'Ingredient3', 'Ingredient4', 'Ingredient5'];

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

    // Add our ingredient
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
}

interface Option {
  value: string;
  displayValue: string;
}
