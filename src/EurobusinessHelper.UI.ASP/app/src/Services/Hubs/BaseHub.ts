import * as signalR from "@microsoft/signalr";
import config from "../../app/config";

export default class BaseHub {
  protected connection: signalR.HubConnection;

  constructor(hubPath: string) {
    const hubUrl = config.apiUrl + hubPath;

    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(hubUrl)
      .build();
  }

  protected async establishConnection(retryCount: number = 0): Promise<void> {
    if (retryCount > 5) {
      alert("connection error");
      return;
    }
    try {
      await this.connection.start();
    } catch (ex) {
      await this.timeout(2000);
      await this.establishConnection(retryCount + 1);
    }
  }

  private timeout(ms: number) {
    return new Promise((resolve) => setTimeout(resolve, ms));
  }
}
