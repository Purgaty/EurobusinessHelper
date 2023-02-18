import BaseHub from "./BaseHub";
import methodNames from "./methodNames";
import {GameChangedNotification, RequestBankTransferApproval, RequestMoneyTransfer} from "./Types/methods";

export default class GameHub extends BaseHub
{
    constructor(gameChangedNotification: GameChangedNotification,
                requestBankTransferApproval: RequestBankTransferApproval,
                requestMoneyTransfer: RequestMoneyTransfer) {
        super("/game");
        
        this.connection.on(methodNames.gameChangedNotification, gameChangedNotification);
        this.connection.on(methodNames.requestBankTransferApproval, requestBankTransferApproval);
        this.connection.on(methodNames.requestMoneyTransfer, requestMoneyTransfer);
    }

    async initializeConnection(accountId: string): Promise<void> {
        await super.establishConnection();
        await this.connection.send(methodNames.registerAccount, accountId);
        console.log(`Account ${accountId} registered`);
    }
}

