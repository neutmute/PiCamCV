module App {

    export enum Direction {
        Unknown= 0,
        Pan,
        Tilt
    }

    export enum PanTiltSettingCommandType {
        Unknown = 0,
        MoveAbsolute,
        MoveRelative,
        MoveSmooth,
        SetRangeMotionUpper,
        SetRangeMotionLower,
        SetRangePursuitUpper,
        SetRangePursuitLower
    }

    export interface IPanTiltSetting {
        panPercent?: number;
        tiltPercent?: number;
    }

    export interface IPanTiltSettingCommand extends IPanTiltSetting {
        type: PanTiltSettingCommandType;
    }

    export enum ProcessingMode {
        Unknown = 0
        , FaceDetection
        , CamshiftTrack
        , ColourObjectTrack
        , CamshiftSelect
        , ColourObjectSelect
        , Static
        , Autonomous
    }

    export interface IBrowserServer {
        hello(message: string): JQueryPromise<void>;
        sendCommand(command: IPanTiltSettingCommand): JQueryPromise<void>;
        updateServerSettings(settings: IServerSettings): JQueryPromise<void>;
        updatePiSettings(settings: IPiSettingsModel): JQueryPromise<void>;
        setMode(mode: ProcessingMode): JQueryPromise<void>;
    }

    export interface IBrowserClient {
        imageReady: (data: string) => void;
        screenWriteLine: (message: string) => void;
        toast: (message: string) => void;
        screenClear: () => void;
        informSettings: ( settings: IServerSettings) => void;
    }

    export interface IPiSettingsModel {
        transmitImageEveryMilliseconds: number;
        enableConsoleTransmit: boolean;
        enableImageTransmit: boolean;
    }

    export interface IServerSettings {
        jpegCompression :number;

        transmitImageViaSignalR: boolean;

        showRegionOfInterest: boolean;

        // How much the ROI should consume of the viewport
        regionOfInterestPercent:boolean;
    }

    export interface IBrowserHubProxy {
        client: IBrowserClient;
        server: IBrowserServer;
    }

    export interface ISignalR {
        browserHub: IBrowserHubProxy ;
    }
}