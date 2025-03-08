
import { ChangeDetectionStrategy, Component, computed, inject, model, signal } from '@angular/core';
import {  MatDialogContent, MatDialogRef, MatDialogTitle } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { provideNativeDateAdapter } from '@angular/material/core';
import { LiveAnnouncer} from '@angular/cdk/a11y';
import { COMMA, ENTER} from '@angular/cdk/keycodes';
import { FormsModule} from '@angular/forms';
import { MatAutocompleteModule, MatAutocompleteSelectedEvent} from '@angular/material/autocomplete';
import { MatChipInputEvent, MatChipsModule} from '@angular/material/chips';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';

@Component({
  selector: 'app-company-dialog',
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
    FormsModule
  ],

  providers: [provideNativeDateAdapter()],
  templateUrl: './company-dialog.component.html',
  styleUrl: './company-dialog.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CompanyDialogComponent {
  readonly dialogRef = inject(MatDialogRef<CompanyDialogComponent>);

  isChecked:boolean =  false;

  myFilter = (d: Date | null): boolean => {
    const oggi = new Date();
    oggi.setHours(0, 0, 0, 0);
    return d ? d >= oggi : false;
  };

  readonly separatorKeysCodes: number[] = [ENTER, COMMA];
  readonly currentFruit = model('');
  readonly ingredients = signal<string[]>([]); // if you want to insert default ingredients
  readonly allFruits: string[] = ['Ingredient1', 'Ingredient2', 'Ingredient3', 'Ingredient4', 'Ingredient5'];//where the ingredients will be loaded
  readonly filteredFruits = computed(() => {
    const currentFruit = this.currentFruit().toLowerCase();
    return currentFruit
      ? this.allFruits.filter(ingredient => ingredient.toLowerCase().includes(currentFruit))
      : this.allFruits.slice();
  });

  readonly announcer = inject(LiveAnnouncer);

  add(event: MatChipInputEvent): void {
    const value = (event.value || '').trim();

    // Add our ingredient
    if (value) {
      this.ingredients.update(ingredients => [...ingredients, value]);
    }

    // Clear the input value
    this.currentFruit.set('');
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
    this.currentFruit.set('');
    event.option.deselect();
  }
}
