import axios from "axios";
import config from "../../app/config";
import {
  Game,
  GameInfo,
  GameState,
  JoinGameData,
  NewGame,
} from "../../Pages/GamePage/types";

export default class GameService {
  static async getGames(state: GameState, query: string): Promise<Game[]> {
    const response = await axios.get(
      config.apiUrl + "/api/game/" + state + "?query=" + query
    );
    return response.data.items;
  }

  static async getGameDetails(gameId: string): Promise<GameInfo> {
    const response = await axios.get(config.apiUrl + "/api/game/" + gameId);
    return response.data;
  }

  static async joinGame(gameId: string, password: JoinGameData): Promise<void> {
    await axios.put(config.apiUrl + "/api/game/" + gameId, password);
  }

  static async deleteGame(gameId: string): Promise<void> {
    const response = await axios.delete(config.apiUrl + "/api/game/" + gameId);
    return response.data;
  }

  static async startGame(gameId: string, state: GameState): Promise<void> {
    await axios.put(config.apiUrl + "/api/game/" + gameId + "/state/" + state);
  }

  static async transferMoney(
    gameId: string,
    payerId: string,
    receiverId: string,
    amount: number
  ): Promise<void> {
    await axios.post(
      config.apiUrl +
        "/api/game/" +
        gameId +
        "/account/" +
        payerId +
        "/transfer",
      { accountId: receiverId, amount: amount }
    );
  }
}
