import { ProductInfo } from "../product/product-info.model";
import { BaseDocument } from "./base-document.model";

export interface ProductDocument extends BaseDocument {
  productInfoId: string;
  productInfo?: ProductInfo;
}
