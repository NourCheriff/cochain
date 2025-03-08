import { Base } from "../base.model";

export interface Company extends Base {
  email?: string;
  phone?: string;
  walletId?: string;
}
