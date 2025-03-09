import { User } from "../auth/user.model";
import { Base } from "../base.model";
import { SupplyChainPartner } from "../company-entities/supply-chain-partner.model";

/*
export interface BaseDocument extends Base {
  path?: string;
  type?: string;
  userEmitter: User;
  supplyChainPartnerReceiver: SupplyChainPartner;
}
*/

export interface BaseDocument extends Base {
  path?: string;
  type: string;
  userEmitterId: string;
  supplyChainPartnerReceiverId: string;
  userEmitter?: string;
  supplyChainPartnerReceiver?: string;
  fileString?: string;
}
