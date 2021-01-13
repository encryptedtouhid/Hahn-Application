import { BindingEngine } from 'aurelia-framework';
const importedData = require('locales/transalation.json');
export class index {
  public message = 'Add Applicants';
  public language = "EN";
  public lngdata = null;
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
