import { Component, Input } from '@angular/core';
import {MatIconModule} from '@angular/material/icon'

@Component({
  selector: 'app-incomes-card',
  imports: [MatIconModule],
  templateUrl: './incomes-card.component.html',
  styleUrl: './incomes-card.component.css'
})
export class IncomesCardComponent {
  @Input() incomes: number = 0;
}
