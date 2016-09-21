
var app = angular
    .module('picamWebApp', ["ngRoute", "ngResource", "ngSanitize"])
    .service("notifierService", [App.Services.NotifierService])
    .service("errorsHttpInterceptor", ["$q", "notifierService", App.Services.ErrorHttpInterceptorService])
    .controller("cameraController", ["$scope", "notifierService", App.CameraController])
    .config([
        '$httpProvider',
        ($httpProvider: ng.IHttpProvider) => {
            $httpProvider.interceptors.push('errorsHttpInterceptor');
        }
    ]);

app.run(() => { });