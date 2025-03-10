import { ProductLifeCycleCategory } from "../product/product-life-cycle-category.model";
import { BaseDocument } from "./base-document.model";

export interface Contract extends BaseDocument {
  productLifeCycleCategory: ProductLifeCycleCategory;
}
