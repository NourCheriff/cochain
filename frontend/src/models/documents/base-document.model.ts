import { User } from "../auth/user.model";
import { Base } from "../base.model";
import { SupplyChainPartner } from "../company-entities/supply-chain-partner.model";

export interface BaseDocument extends Base {
  path?: string;
  hash?: string;
  type?: string;
  userEmitterId?: string;
  supplyChainPartnerReceiverId: string;
  userEmitter?: User;
  supplyChainPartnerReceiver?: SupplyChainPartner;
  fileString?: string;
}
