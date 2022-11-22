import GameService from "../../Services/Game/GameService";
import { NewGame } from "./types";

export const getGames = async (query: string): Promise<[]> => {
  return await GameService.getGames(query);
};

export const postGame = async (game: NewGame): Promise<[]> => {
  return await GameService.postGame(game);
};
