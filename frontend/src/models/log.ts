import { Severity } from "src/types/severity.enum";
import { Base } from "./base.model";

export interface Log extends Base{
  severity: Severity;
  entity: string;
  entityId?: string;
  action: string;
  message: string;
  timestamp: Date;
  userId?: string;
  URL?: string;
  queryString?: string;
  cookies?: string;
}
