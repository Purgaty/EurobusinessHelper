import GameService from "../../Services/Game/GameService";
import { Game, GameInfo, JoinGameData, NewGame } from "./types";

export const getGames = async (query: string): Promise<Game[]> => {
  return await GameService.getGames(query);
};

export const getGameDetails = async (gameId: string): Promise<GameInfo> => {
  return await GameService.getGameDetails(gameId);
};

export const postGame = async (game: NewGame): Promise<void> => {
  return await GameService.postGame(game);
};

export const joinGame = async (
  gameId: string,
  password: JoinGameData
): Promise<void> => {
  return await GameService.joinGame(gameId, password);
};

export const deleteGame = async (gameId: string): Promise<void> => {
  return await GameService.deleteGame(gameId);
};
