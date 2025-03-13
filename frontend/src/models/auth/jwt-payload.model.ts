export interface Jwt {
  nameid: string;
  email: string;
  jti: string;
  nbf: number;
  exp: number;
  iss: string;
  aud: string;
}