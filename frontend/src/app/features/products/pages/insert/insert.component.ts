import { Component, inject } from '@angular/core';
import { ProductService } from '../../services/product.service';

@Component({
  selector: 'app-insert',
  imports: [],
  templateUrl: './insert.component.html',
  styleUrl: './insert.component.css'
})
export class InsertComponent {
  private service = inject(ProductService);
}
