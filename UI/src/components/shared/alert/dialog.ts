import {DialogController} from 'aurelia-dialog';
import { autoinject, inject } from 'aurelia-framework';
 
@autoinject
 
export class Dialog {    
    title?: string;
    message?: string;
    action?: (args?: any) => {};
 
    constructor(private dialogController : DialogController) {
        dialogController.settings.centerHorizontalOnly = true;
    }
 
    activate(model : any) {
        this.message = model.message;
        this.title = model.title;
        this.action = model.action;
     }
 
     ok() : void {
         this.action();
        this.dialogController.ok();
     }
}
