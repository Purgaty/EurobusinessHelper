import BaseHub from "./BaseHub";
import methodNames from "./methodNames";
import { GameListChangedNotification } from "./Types/methods";

export default class MainHub extends BaseHub {
  constructor(gameListChangedNotification: GameListChangedNotification) {
    super("/main");

    this.connection.on(
      methodNames.gameListChangedNotification,
      gameListChangedNotification
    );
  }

  public initializeConnection = async () => {
    await super.establishConnection();
  };
}
