import GameService from "../../Services/Game/GameService";
import { selectGameDetails, setGameDetails, setGameList } from "./gameSlice";
import { JoinGameData, NewGame } from "./types";

export const fetchGames =
  (query: string) =>
  async (dispatch: Function): Promise<void> => {
    const gameList = await GameService.getGames(query);
    dispatch(setGameList(gameList));
  };

export const fetchDetails =
  (gameId: string, forceReload: boolean = false) =>
  async (dispatch: Function, getState: Function): Promise<void> => {
    if (!forceReload && !!selectGameDetails(gameId)(getState())) return;
    const details = await GameService.getGameDetails(gameId);
    dispatch(setGameDetails(details));
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
