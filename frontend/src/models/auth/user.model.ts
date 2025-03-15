import { CertificationAuthority } from "../company-entities/certification-authority.model";
import { SupplyChainPartner } from "../company-entities/supply-chain-partner.model";

export interface User {
  id: string;
  firstName: string;
  lastName: string;
  userName: string;
  phone?: string;
  role: string;
  userRoles?: string[];
  certificationAuthorityId?: string;
  certificationAuthority?: CertificationAuthority;
  supplyChainPartnerId?: string;
  supplyChainPartner?: SupplyChainPartner;
}
