module App {

    export enum Direction {
        Unknown= 0,
        Pan,
        Tilt
    }

    export interface IPanTiltSetting {
        panPercent?: number;
        tiltPercent?: number;
    }

    export interface IBrowserServer {
        hello(message: string): JQueryPromise<void>;
        movePanTilt(plane: Direction, units: number): JQueryPromise<void>;
        changeSettings(settings:ISystemSettings): JQueryPromise<void>;
    }

    export interface IBrowserClient {
        imageReady: (data: string) => void;
        screenWriteLine: (message: string) => void;
        toast: (message: string) => void;
        screenClear: () => void;
        informSettings: ( settings: ISystemSettings) => void;
    }

    export interface ISystemSettings {
        jpegCompression :number;

        transmitImageEveryMilliseconds: number;

        transmitImageViaSignalR :boolean;
    }

    export interface IBrowserHubProxy {
        client: IBrowserClient;
        server: IBrowserServer;
    }

    export interface ISignalR {
        browserHub: IBrowserHubProxy ;
    }
}