import { CertificationAuthority } from "../company-entities/certification-authority.model";
import { SupplyChainPartner } from "../company-entities/supply-chain-partner.model";

export interface User {
  firstName: string;
  lastName: string;
  userName: string;
  phone: string;
  role: string;
  certificationAuthority: CertificationAuthority;
  supplyChainPartner: SupplyChainPartner;
}
