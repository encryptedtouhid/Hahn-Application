import {Aurelia} from 'aurelia-framework';
import * as environment from '../config/environment.json';
import {PLATFORM} from 'aurelia-pal';
import { I18N, TCustomAttribute } from "aurelia-i18n";
import Backend from "i18next-xhr-backend";

export function configure(aurelia: Aurelia): void {
  aurelia.use
    .standardConfiguration()
    .feature(PLATFORM.moduleName('resources/index'))
    .plugin(PLATFORM.moduleName("aurelia-validation"))
    .plugin(PLATFORM.moduleName('aurelia-dialog'))
    .plugin(PLATFORM.moduleName("aurelia-i18n"), (instance) => {
      let aliases = ["t", "i18n"];
      TCustomAttribute.configureAliases(aliases);   
      instance.i18next.use(Backend);
      return instance.setup({
        backend: {
          // <-- configure backend settings
          loadPath: "./locales/{{lng}}/{{ns}}.json", // <-- XHR settings for where to get the files from
        },
        attributes: aliases,
        lng: "de",
        fallbackLng: "en",
        debug: false,
      });
    });
    
  aurelia.use.developmentLogging(environment.debug ? 'debug' : 'warn');

  if (environment.testing) {
    aurelia.use.plugin(PLATFORM.moduleName('aurelia-testing'));
  }

  aurelia.start().then(() => aurelia.setRoot(PLATFORM.moduleName('app')));
}
