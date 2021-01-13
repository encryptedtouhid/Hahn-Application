import { ValidationRules } from "aurelia-validation";
export class ApplicantDto {
  name: string;
  familyName: string;
  address: string;
  countryOfOrigin: string;
  emailAddress: string;
  age: number;
  hired: boolean;
}
