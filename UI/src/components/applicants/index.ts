import { ApplicantDto } from "../../dtos/ApplicantDto";
import { bindable, inject, NewInstance, observable } from "aurelia-framework";
import { HttpClient } from "aurelia-fetch-client";
import { autoinject } from "aurelia-dependency-injection";
import { Router } from "aurelia-router";
const importedData = require('locales/transalation.json');
import {
  ValidationControllerFactory,
  ValidationController,
  ValidationRules,
  validateTrigger,
} from "aurelia-validation";

@autoinject
export class Application {
  public heading: string = "Hahn Application";
  applicant: ApplicantDto = null;
  controller: ValidationController;
  validator = null;
  http: HttpClient;

  name: string = "Khaled";
  familyName: string = "Md Tuhidul Hossain";
  address: string = "60/3, East Chandona, Gazipur - 1701";
  countryOfOrigin: string = "Bangladesh";
  emailAddress: string = "mth.tuhin@gmail.com";
  age: number = 29;
  hired: boolean = true;

  cansave: boolean;
  router: Router;

  public language = "EN";
  public lngdata = null;
  

  constructor(
    controller: ValidationControllerFactory,
    http: HttpClient,
    router: Router
  ) {
    this.controller = controller.createForCurrentScope();
    this.controller.validateTrigger = validateTrigger.manual;
    this.router = router;
    this.http = http;
    ValidationRules.ensure((p: this) => p.name)
      .required()
      .minLength(5)
      .ensure((p: this) => p.familyName)
      .required()
      .minLength(5)
      .ensure((p: this) => p.address)
      .required()
      .minLength(10)
      .ensure((p: this) => p.countryOfOrigin)
      .required()
      .ensure((p: this) => p.emailAddress)
      .required()
      .email()
      .ensure((p: this) => p.age)
      .required()
      .min(20)
      .max(60)
      .on(this);

      this.language = "EN";
      this.lngdata = importedData;
  }

  onClick(button) {
    if (this.language === "DE") {
      this.language = "EN";
    }
    else {
      this.language = "DE"
    }   
    this.lngdata = importedData;
    console.log('JSON loaded via import', this.lngdata);
  }
  public save() {
    this.applicant = {
      address: this.address,
      age: parseInt(this.age.toString()),
      countryOfOrigin: this.countryOfOrigin,
      emailAddress: this.emailAddress,
      familyName: this.familyName,
      hired: this.hired,
      name: this.name,
    };

    console.log("appli", this.applicant);
    this.controller.validate().then(async (v) => {
      if (v.valid) {
        try {
          var saved = await this.http.post(
            "https://localhost:5001/api/applicant",
            JSON.stringify(this.applicant)
          );
          var response = await saved.json();
          if (response != null) {
            this.router.navigateToRoute("applicationdetail", {
              id: response.id,
            });
          }
        } catch (err) {
          console.log(err);
        }
      } else {
        console.log(v);
      }
    });
  }

  public reset() {
    if (confirm("Are you sure you want to reset this form?"))
      this.applicant = new ApplicantDto();
    this.name = null;
    this.familyName = null;
    this.address = null;
    this.emailAddress = null;
    this.countryOfOrigin = null;
    this.hired = this.hired;
    this.age = null;
  }
}
