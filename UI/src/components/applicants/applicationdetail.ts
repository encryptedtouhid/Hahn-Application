import { autoinject } from "aurelia-framework";
import { HttpClient } from "aurelia-fetch-client";
const importedData = require('locales/transalation.json');
@autoinject
export class ApplicationDetail {
  public heading: string = "Applicant Detail";
  public applicant: any;
  public language = "EN";
  public lngdata = null;

  constructor(private http: HttpClient) {
    http.configure((config) => {
      config.useStandardConfiguration().withBaseUrl("https://api.github.com/");
    });
    this.language = "EN";
    this.lngdata = importedData;
  }

  async activate(params, routeConfig): Promise<void> {
    const response = await this.http.fetch(
      `${params.url}`
    );
    this.applicant = await response.json();
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
}
