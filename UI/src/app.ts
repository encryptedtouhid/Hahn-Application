import {RouterConfiguration, Router} from 'aurelia-router';
import { Aurelia, PLATFORM } from 'aurelia-framework';

export class App {
  router: Router;


  configureRouter(config: RouterConfiguration, router: Router): void {
    this.router = router;
    config.title = 'Aurelia';
    config.map([
      { 
        route: ['', 'home'],
        name: 'home',
        moduleId:  PLATFORM.moduleName('components/home/index'),
        nav: true, title: 'Home' 
      },
      { 
        route: 'applicants',
        name: 'applicants',
        moduleId:  PLATFORM.moduleName('components/applicants/index'),
        nav: true, 
        title: 'Applicants' 
      },
      // { route: 'users/:id/detail', name: 'userDetail', moduleId: 'users/detail' },
      // { route: 'files/*path',      name: 'files',      moduleId: 'files/index', nav: 0,    title: 'Files', href:'#files' }
    ]);
  }
}
