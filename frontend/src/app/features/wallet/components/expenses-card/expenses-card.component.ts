import { Component, Input } from '@angular/core';
import { MatIconModule } from '@angular/material/icon'

@Component({
  selector: 'app-expenses-card',
  imports: [MatIconModule],
  templateUrl: './expenses-card.component.html',
  styleUrl: './expenses-card.component.css'
})
export class ExpensesCardComponent {

  @Input() expenses: number = 0;

}
