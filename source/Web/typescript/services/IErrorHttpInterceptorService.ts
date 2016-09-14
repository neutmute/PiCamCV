module App.Services {
    export interface IErrorHttpInterceptorService {
        responseError: Function;
    } 

    //http://stackoverflow.com/questions/29851891/angular-on-top-of-asp-net-mvc-displaying-errors
    export class ErrorHttpInterceptorService implements IErrorHttpInterceptorService {

        constructor(
            private $q: ng.IQService
            , private notifierService: INotifierService) {
        }

        // Success intercept and render
        public response = (response): ng.IPromise<any> => {
            switch (response.status) {
                case 202:
                    // magic number to let the response know there is a custom message available
                    this.notifierService.success(response.data);
                    break;
            }

            return response;
        }

        public responseError = (responseFailure): ng.IPromise<any> => {
            switch (responseFailure.status) {
                case 409:
                    // magic number to let the response know there is a custom message available
                    this.notifierService.warning(responseFailure.data);
                    break;
                default:
                    this.notifierService.error("An error occurred on the server. Check logs.", responseFailure.status + " " + responseFailure.statusText);
                    break;
            }
            
            return this.$q.reject(responseFailure);
        }
    }
}
