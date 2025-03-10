import { ProductLifeCycle } from "../product/product-life-cycle.model";
import { BaseDocument } from "./base-document.model";

export interface ProductLifeCycleDocument extends BaseDocument {
  productLifeCycle: ProductLifeCycle;
}
