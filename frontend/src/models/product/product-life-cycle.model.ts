import { Base } from "../base.model";
import { SupplyChainPartner } from "../company-entities/supply-chain-partner.model";
import { ProductLifeCycleDocument } from "../documents/product-life-cycle-document.model";
import { ProductInfo } from "./product-info.model";
import { ProductLifeCycleCategory } from "./product-life-cycle-category.model";

export interface ProductLifeCycle extends Base {
  timestamp: string;
  emissions: number;
  productLifeCycleCategory: ProductLifeCycleCategory;
  supplyChainPartner: SupplyChainPartner;
  productInfo: ProductInfo;
  productLifeCycleDocuments?: ProductLifeCycleDocument[];
}
