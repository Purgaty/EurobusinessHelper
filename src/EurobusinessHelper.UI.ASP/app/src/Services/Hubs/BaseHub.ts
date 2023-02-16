import config from "../../app/config";
import * as signalR from "@microsoft/signalr";
import methodNames from "./methodNames";

export default class BaseHub
{
    protected connection: signalR.HubConnection;

    constructor(hubPath: string) {
        const hubUrl = config.apiUrl + hubPath;
        
        this.connection = new signalR.HubConnectionBuilder()
        .withUrl(hubUrl)
        .build()
    }
    
    public async initializeConnection(accountId: string, retryCount: number = 0) {
        if(retryCount > 5) {
            alert("connection error");
            return;
        }
        try {
            await this.connection.start();
            await this.connection.send(methodNames.registerAccount, accountId);
        } catch(ex) {
            await this.timeout(2000);
            await this.initializeConnection(accountId, retryCount + 1);
        }
    }

    private timeout(ms: number) {
        return new Promise(resolve => setTimeout(resolve, ms));
    }
}