import BaseHub from "./BaseHub";
import methodNames from "./methodNames";
import {
  CreateOperationLog,
  GameChangedNotification,
  RequestBankTransferApproval,
  RequestMoneyTransfer,
} from "./Types/methods";

export default class GameHub extends BaseHub {
  constructor(
    gameChangedNotification: GameChangedNotification,
    requestBankTransferApproval: RequestBankTransferApproval,
    requestMoneyTransfer: RequestMoneyTransfer,
    createOperationLog: CreateOperationLog
  ) {
    super("/game");

    this.connection.on(methodNames.gameChangedNotification, gameChangedNotification);
    this.connection.on(methodNames.requestBankTransferApproval, requestBankTransferApproval);
    this.connection.on(methodNames.requestMoneyTransfer, requestMoneyTransfer);
    this.connection.on(methodNames.createOperationLog, createOperationLog);
  }

  async initializeAccount(accountId: string): Promise<void> {
    await super.establishConnection();
    await this.connection.send(methodNames.registerAccount, accountId);
  }

  async initializeGame(gameId: string): Promise<void> {
    await super.establishConnection();
    await this.connection.send(methodNames.registerGame, gameId);
  }
}
