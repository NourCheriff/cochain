import { Base } from "../base.model";
import { SupplyChainPartner } from "../company-entities/supply-chain-partner.model";
import { ProductDocument } from "../documents/product-document.model";
import { ProductIngredient } from "./product-ingredient.model";
import { ProductLifeCycle } from "./product-life-cycle.model";
import { Product } from "./product.model";

export interface ProductInfo extends Base {
  productId: string;
  product?: Product;
  expirationDate: string;
  supplyChainPartnerId?: string;
  supplyChainPartner?: SupplyChainPartner;
  ingredients?: ProductIngredient[];
  productLifeCycles?: ProductLifeCycle[];
  productDocuments?: ProductDocument[];
}
