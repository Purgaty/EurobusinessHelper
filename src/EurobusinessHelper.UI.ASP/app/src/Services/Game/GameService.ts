import axios from "axios";
import config from "../../app/config";

export default class GameService {
  static async getGames(): Promise<[]> {
    const response = await axios.get(config.apiUrl + "/api/game");
    return response.data.items;
  }
}