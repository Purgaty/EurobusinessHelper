import GameService from "../../Services/Game/GameService";
import {
  selectGameDetails,
  selectGameListSearch,
  setGameDetails,
  setGameList,
  setGameListSearch,
  setMyGameList,
} from "./gameSlice";
import { GameState, JoinGameData, NewGame } from "./types";

export const refreshGames =
  () =>
  async (dispatch: Function, getState: Function): Promise<void> => {
    const query = selectGameListSearch(getState());
    await dispatch(fetchGames(query));
    await dispatch(fetchMyGames(query));
  };

export const fetchGames =
  (query: string) =>
  async (dispatch: Function): Promise<void> => {
    dispatch(setGameSearch(query));
    const gameList = await GameService.getGames(query);
    dispatch(setGameList(gameList));
  };

export const fetchMyGames =
  (query: string) =>
  async (dispatch: Function): Promise<void> => {
    dispatch(setGameSearch(query));
    const gameList = await GameService.getMyGames(query);
    dispatch(setMyGameList(gameList));
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
  await GameService.joinGame(gameId, password);
};

export const deleteGame = async (gameId: string): Promise<void> => {
  return await GameService.deleteGame(gameId);
};

export const setGameSearch =
  (search: string) =>
  (dispatch: Function): void => {
    dispatch(setGameListSearch(search));
  };

export const startGame = async (
  gameId: string,
  state: GameState
): Promise<void> => {
  await GameService.startGame(gameId, state);
};
