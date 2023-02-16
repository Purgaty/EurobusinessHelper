import BaseHub from "./BaseHub";
import methodNames from "./methodNames";
import {AccountTransferNotification, BankTransferNotification, RequestBankTransferApproval} from "./Types/methods";

export default class GameHub extends BaseHub
{
    constructor(bankTransferNotification: BankTransferNotification,
                accountTransferNotification: AccountTransferNotification,
                requestBankTransferApproval: RequestBankTransferApproval) {
        super("/game");
        
        this.connection.on(methodNames.bankTransferNotification, bankTransferNotification);
        this.connection.on(methodNames.accountTransferNotification, accountTransferNotification);
        this.connection.on(methodNames.requestBankTransferApproval, requestBankTransferApproval);
    }
}

