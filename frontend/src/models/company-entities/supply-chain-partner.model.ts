import { CarbonOffsettingAction } from "../carbon-offset/carbon-offsetting-actions.model";
import { Contract } from "../documents/contract.model";
import { ProductDocument } from "../documents/product-document.model";
import { ProductLifeCycleDocument } from "../documents/product-life-cycle-document.model";
import { SupplyChainPartnerCertificate } from "../documents/supply-chain-partner-certificate.model";
import { Company } from "./company.model";
import { SupplyChainPartnerType } from "./supply-chain-partner-type.model";

export interface SupplyChainPartner extends Company {
  credits: number;
  supplyChainPartnerTypeId: string;
  supplyChainPartnerType?: SupplyChainPartnerType;
  receivedContracts?: Contract[];
  receivedProductDocuments?: ProductDocument[];
  receivedProductLifeCycleDocuments?: ProductLifeCycleDocument[];
  receivedSupplyChainPartnerCertificates?: SupplyChainPartnerCertificate[];
  carbonOffsettingActions?: CarbonOffsettingAction[];
}
