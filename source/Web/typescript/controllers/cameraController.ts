/// <reference path="../typings/globals/jquery/index.d.ts" />

module App {
    
    export interface ICameraControllerScope extends ng.IScope {

    }

    export class CameraController {

        private _browserHub: IBrowserHubProxy;
        moveUnits: number;
        imageUrl: string;
        imageCounter:number;

        constructor(
            private $scope: ICameraControllerScope, private notifierService: Services.INotifierService
        ) {
            this._browserHub = (<any>$.connection).browserHub;
            this.imageCounter = 0;

            this._browserHub.client.imageReady = () => {
                this.imageCounter++;
                this.imageUrl = "/image?cacheBusterId=" + this.imageCounter;
                $scope.$apply();
                console.log("imagr!");
            };

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