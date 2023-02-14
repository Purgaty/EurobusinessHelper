import BaseHub from "./BaseHub";

//transfer między graczami i transfer z banku
export default class NotificationsHub extends BaseHub
{
    /**
     *
     */
    constructor() {
        super("/notifications");
        

        this.connection.on("requestTransferNotification", (accountId: string, amount: number, requestId: string) => {
            alert(`Transfer for ${amount} requested from ${accountId}. RequestId: ${requestId}`);
        });
    }

    public async requestTransfer(amount: number) {
        await this.connection.send("requestBankTransfer", amount);
    }
}

