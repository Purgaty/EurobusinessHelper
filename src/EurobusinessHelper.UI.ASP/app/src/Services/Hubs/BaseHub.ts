import config from "../../app/config";
import * as signalR from "@microsoft/signalr";

export default class BaseHub
{
    protected connection: signalR.HubConnection;

    constructor(hubPath: string) {
        const hubUrl = config.apiUrl + hubPath;
        
        this.connection = new signalR.HubConnectionBuilder()
        .withUrl(hubUrl)
        .build()
    }
}