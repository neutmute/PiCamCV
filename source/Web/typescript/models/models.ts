module App {

    export enum Direction {
        Unknown= 0,
        Pan,
        Tilt
    }

    export interface IBrowserServer {
        movePanTilt(plane: Direction, units: number): JQueryPromise<void>;
    }
    export interface IBrowserClient {
        imageReady: (data: string) => void;
        writeLine:(message: string)=>void;
    }
    export interface IBrowserHubProxy {
        client: IBrowserClient;
        server: IBrowserServer;
    }

    export interface ISignalR {
        browserHub: IBrowserHubProxy ;
    }


}