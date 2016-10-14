/// <reference path="../typings/globals/jquery/index.d.ts" />

module App {
    
    export interface ICameraControllerScope extends ng.IScope {

    }

    export class CameraController {

        private _browserHub: IBrowserHubProxy;
        imageUrl: string;
        imageCounter: number;
        consoleScreen: string;
        systemSettings: ISystemSettings;
        moveAbsoluteSetting: IPanTiltSetting;
        moveRelativeScale:number;

        constructor(
            private $scope: ICameraControllerScope
            , private notifierService: Services.INotifierService
        ) {
            this._browserHub = (<any>$.connection).browserHub;
            this.imageCounter = 0;
            this.consoleScreen = '';
            this.moveRelativeScale = 3;
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
            var command = this.commandFactory(PanTiltSettingCommandType.MoveRelative, 0, this.moveRelativeScale);
            this.sendCommand(command);
        }

        down(): void {
            var command = this.commandFactory(PanTiltSettingCommandType.MoveRelative, 0, -this.moveRelativeScale);
            this.sendCommand(command);
        }

        left(): void {
            var command = this.commandFactory(PanTiltSettingCommandType.MoveRelative, -this.moveRelativeScale, 0);
            this.sendCommand(command);
        }

        right(): void {
            var command = this.commandFactory(PanTiltSettingCommandType.MoveRelative, this.moveRelativeScale, 0);
            this.sendCommand(command);
        }
        
        changeSettings(): void {
            this._browserHub.server.changeSettings(this.systemSettings);
        }
        
        private commandFactory(type: PanTiltSettingCommandType, pan: number, tilt:number): IPanTiltSettingCommand {
            var command = <IPanTiltSettingCommand>{};
            command.type = type;
            command.panPercent = pan;
            command.tiltPercent = tilt;
            return command;
        }

        moveAbsolute(): void {
            this.sendAbsoluteCommand(PanTiltSettingCommandType.MoveAbsolute);
        }

        public setPursuitLower() {
            this.sendAbsoluteCommand(PanTiltSettingCommandType.SetRangePursuitLower);
        }

        public setPursuitUpper() {
            this.sendAbsoluteCommand(PanTiltSettingCommandType.SetRangePursuitUpper);
        }

        private sendAbsoluteCommand(type: PanTiltSettingCommandType) {
            var command = this.commandFactory(
                type
                , this.moveAbsoluteSetting.panPercent
                , this.moveAbsoluteSetting.tiltPercent);

            this.sendCommand(command);
        }

        public sendCommand(command: IPanTiltSettingCommand): JQueryPromise<void> {
            return this._browserHub.server.sendCommand(command);
        }

        private startColourTrack(): JQueryPromise<void> {
            return this._browserHub.server.setMode(ProcessingMode.ColourObjectSelect);
        }

        private startFaceTrack(): JQueryPromise<void> {
            return this._browserHub.server.setMode(ProcessingMode.FaceDetection);
        }

        private startStatic(): JQueryPromise<void> {
            return this._browserHub.server.setMode(ProcessingMode.Static);
        }

        private startAutonomousTrack(): JQueryPromise<void> {
            return this._browserHub.server.setMode(ProcessingMode.Autonomous);
        }
    }
}