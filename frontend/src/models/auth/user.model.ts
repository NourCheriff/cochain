import { CertificationAuthority } from "../company-entities/certification-authority.model";
import { SupplyChainPartner } from "../company-entities/supply-chain-partner.model";

export interface User {
  id: string;
  firstName: string;
  lastName: string;
  phone?: string;
  certificationAuthority?: CertificationAuthority;
  supplyChainPartner?: SupplyChainPartner;
  userRoles?: string[];
  role?: string;
}
