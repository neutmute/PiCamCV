/// <reference path="../typings/globals/toastr/index.d.ts" />
/*
http://stackoverflow.com/questions/21565738/using-toastr-in-the-angularjs-way
*/
module App.Services {
    export interface INotifierService {
        success(message: string): void;
        error(message, title?: string): void;
        warning(message: string, title?: string): void;
    }

    export class NotifierService implements INotifierService {

        constructor() {
            //toastr.options.timeOut = 0;
            //toastr.options.extendedTimeOut = 0;
            toastr.options.positionClass = "toast-bottom-left";
        }

        success(message: string) {
            toastr.success(message);
        }

        error(message: string, title?: string) {
            toastr.error(message, title);
        }

        warning(message: string, title?: string) {
            toastr.warning(message, title);
        }
    }
}