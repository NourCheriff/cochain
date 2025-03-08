import { Base } from "../Base";

export interface BaseDocument extends Base {
  path?: string;
  type: string;
  userEmitterId: string;
  supplyChainPartnerReceiverId: string;
  userEmitter?: string;
  supplyChainPartnerReceiver?: string;
  file?: File;
}
