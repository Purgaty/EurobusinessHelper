import BaseHub from "./BaseHub";

export default class NotificationsHub extends BaseHub
{
    /**
     *
     */
    constructor() {
        super("/notifications");

        this.connection.on("requestTransferNotification", (accountId: string, amount: number) => {
            alert(`Tranfer for ${amount} requested from ${accountId}`);
        });
    }

    public async initializeConnection(accountId: string, retryCount: number = 0) {
        if(retryCount > 5) {
            alert("connection error");
            return;
        }
        try {
            await this.connection.start();
            await this.connection.send("registerAccount", accountId);
        } catch(ex) {
            await this.timeout(2000);
            await this.initializeConnection(accountId, retryCount + 1);
        }
    }

    public async requestTransfer(amount: number) {
        await this.connection.send("requestBankTransfer", amount);
    }

    private timeout(ms: number) {
        return new Promise(resolve => setTimeout(resolve, ms));
    }
}