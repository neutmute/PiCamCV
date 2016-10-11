/// <reference path="../typings/globals/jquery/index.d.ts" />

module App {
    
    export interface ICameraControllerScope extends ng.IScope {

    }

    export class CameraController {

        private _browserHub: IBrowserHubProxy;
        moveUnits: number;
        imageUrl: string;
        imageCounter: number;
        consoleScreen: string;
        systemSettings: ISystemSettings;
        moveAbsoluteSetting: IPanTiltSetting;

        constructor(
            private $scope: ICameraControllerScope
            , private notifierService: Services.INotifierService
        ) {
            this._browserHub = (<any>$.connection).browserHub;
            this.imageCounter = 0;
            this.consoleScreen = '';
            this.moveUnits = 10;
            this.systemSettings = <ISystemSettings>{};
            this.moveAbsoluteSetting = <IPanTiltSetting>{panPercent:50, tiltPercent :50};

            this._browserHub.client.imageReady = (s) => {
                this.imageCounter++;
                if (s) {
                    // image content embedded
                    this.imageUrl = s;
                }
                else
                {
                    // need to request image binary
                    this.imageUrl = "/image?cacheBusterId=" + this.imageCounter;
                }
                $scope.$apply();
            };

            this._browserHub.client.screenWriteLine = (s) => {
                console.log(s);
                this.consoleScreen += s + "\r\n";
                $scope.$apply();
            };

            this._browserHub.client.toast = (s) => {
                this.notifierService.success(s);
            };

            this._browserHub.client.screenClear = () => {
                this.consoleScreen = '';
                $scope.$apply();
            };

            this._browserHub.client.screenWriteLine = (s) => {
                this.consoleScreen = s + "\r\n" + this.consoleScreen;
                $scope.$apply();
            };

            this._browserHub.client.informSettings = (settings) => {
                this.systemSettings = settings;
                //this.notifierService("System settings received");
                $scope.$apply();
            };

            $.connection.hub.start().done(() => {
                console.log("joined");
            });
        }

        up(): void {
            this.moveRelative(Direction.Tilt, this.moveUnits);
        }

        down(): void {
            this.moveRelative(Direction.Tilt, -this.moveUnits);
        }

        left(): void {
            this.moveRelative(Direction.Pan, this.moveUnits);
        }

        right(): void {
            this.moveRelative(Direction.Pan, -this.moveUnits);
        }

        changeSettings(): void {
            this._browserHub.server.changeSettings(this.systemSettings);
        }

        private moveRelative(plane: Direction, units: number): JQueryPromise<void> {
            return this._browserHub.server.moveRelative(plane, units);
        }

        public moveAbsolute(): JQueryPromise<void> {
            return this._browserHub.server.moveAbsolute(this.moveAbsoluteSetting);
        }

        private startColourTrack(): JQueryPromise<void> {
            return this._browserHub.server.setMode(ProcessingMode.ColourObjectSelect);
        }

        private startFaceTrack(): JQueryPromise<void> {
            return this._browserHub.server.setMode(ProcessingMode.FaceDetection);
        }

        private startAutonomousTrack(): JQueryPromise<void> {
            return this._browserHub.server.setMode(ProcessingMode.Autonomous);
        }
    }
}