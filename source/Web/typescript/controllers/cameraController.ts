/// <reference path="../typings/globals/jquery/index.d.ts" />

module App {
    
    export interface ICameraControllerScope extends ng.IScope {

    }

    export class CameraController {

        private _browserHub: IBrowserHubProxy;
        moveUnits :number;

        constructor(
            private $scope: ICameraControllerScope, private notifierService: Services.INotifierService
        ) {
            this._browserHub = (<any>$.connection).browserHub;

            this._browserHub.client.imageReady = () => console.log("ImageReady!");
            this.moveUnits= 10;

            $.connection.hub.start().done(() => {
                console.log("joined");
            });
        }

        up(): void {
            this.movePanTilt(Direction.Tilt, this.moveUnits);
        }

        down(): void {
            this.movePanTilt(Direction.Tilt, -this.moveUnits);
        }

        left(): void {
            this.movePanTilt(Direction.Pan, this.moveUnits);
        }

        right(): void {
            this.movePanTilt(Direction.Pan, -this.moveUnits);
        }

        private movePanTilt(plane: Direction, units : number): JQueryPromise<void> {
            return this._browserHub.server.movePanTilt(plane, units);
        }
    }
}