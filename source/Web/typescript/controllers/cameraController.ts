module App.Controllers {
    
    export interface ICameraControllerScope extends ng.IScope {

    }

    export class CameraController {
        
        constructor(
            private $scope: ICameraControllerScope 
            , private notifierService: Services.INotifierService
        ) {
        
        }
        
    }
}