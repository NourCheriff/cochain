import { Base } from '../base.model';
import { ProductCategory } from './product-category.model';

export interface Product extends Base  {
  description?: string;
  categoryId: string;
  category?: ProductCategory;
}
