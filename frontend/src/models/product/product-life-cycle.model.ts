import { Base } from "../base.model";
import { SupplyChainPartner } from "../company-entities/supply-chain-partner.model";
import { ProductLifeCycleDocument } from "../documents/product-life-cycle-document.model";
import { ProductInfo } from "./product-info.model";
import { ProductLifeCycleCategory } from "./product-life-cycle-category.model";

export interface ProductLifeCycle extends Base {
  timestamp: string;
  emissions: number;
  isEmissionsProcessed: boolean;
  emissionTransactionId?: string;
  productLifeCycleCategoryId: string;
  productLifeCycleCategory?: ProductLifeCycleCategory;
  supplyChainPartnerId: string;
  supplyChainPartner?: SupplyChainPartner;
  productInfoId: string;
  productInfo?: ProductInfo;
  productLifeCycleDocuments?: ProductLifeCycleDocument[];
}
