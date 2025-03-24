export interface Jwt {
  nameid: string;
  email: string;
  jti: string;
  nbf: number;
  exp: number;
  iss: string;
  aud: string;
  [ROLES_KEY]: string | string[];
}

export const ROLES_KEY = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role';
