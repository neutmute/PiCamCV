/// <reference path="../typings/globals/jquery/index.d.ts" />

module App {
    
    export interface ICameraControllerScope extends ng.IScope {

    }

    export class CameraController {

        private _browserHub: IBrowserHubProxy;
        moveUnits: number;
        imageUrl: string;
        imageCounter: number;
        consoleScreen:string;

        constructor(
            private $scope: ICameraControllerScope, private notifierService: Services.INotifierService
        ) {
            this._browserHub = (<any>$.connection).browserHub;
            this.imageCounter = 0;
            this.consoleScreen = '';

            this._browserHub.client.imageReady = (s) => {
                this.imageCounter++;
                this.imageUrl = s;//"/image?cacheBusterId=" + this.imageCounter;
                $scope.$apply();
                console.log("imagr!");
            };

            this._browserHub.client.writeLine = (s) => {
                console.log(s);
                this.consoleScreen += s + "\r\n";
                $scope.$apply();
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

        private movePanTilt(plane: Direction, units: number): JQueryPromise<void> {
            this._browserHub.server.hello("sdf");
            return this._browserHub.server.movePanTilt(plane, units);
        }
    }
}