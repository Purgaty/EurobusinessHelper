import axios from "axios";
import config from "../../app/config";
import { NewGame } from "../../Pages/GamePage/types";

export default class GameService {
  static async getGames(query: string): Promise<[]> {
    const response = await axios.get(
      config.apiUrl + "/api/game?query=" + query
    );
    return response.data.items;
  }

  static async postGame(game: NewGame): Promise<[]> {
    const response = await axios.post(config.apiUrl + "/api/game", game);
    return response.data.items;
  }
}
