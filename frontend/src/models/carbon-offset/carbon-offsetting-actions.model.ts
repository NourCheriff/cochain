import { Base } from "../base.model";
import { SupplyChainPartner } from "../company-entities/supply-chain-partner.model";

export interface CarbonOffsettingAction extends Base {
  offset: number;
  supplyChainPartner: SupplyChainPartner;
}
