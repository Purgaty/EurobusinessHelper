import axios from "axios";
import config from "../../app/config";

export default class GameService {
  static async getGames(query: string): Promise<[]> {
    const response = await axios.get(
      config.apiUrl + "/api/game?query=" + query
    );
    return response.data.items;
  }
}
