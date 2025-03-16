import { Base } from "../base.model";

export interface BaseDocument extends Base {
  path?: string;
  type: string;
  userEmitterId: string;
  supplyChainPartnerReceiverId: string;
  userEmitter?: string;
  supplyChainPartnerReceiver?: string;
  hash: string;
  //fileString: string;
}
