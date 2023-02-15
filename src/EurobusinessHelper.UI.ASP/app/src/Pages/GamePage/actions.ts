import GameService from "../../Services/Game/GameService";
import {
  selectGameDetails,
  selectGameListSearch,
  setGameDetails,
  setGameList,
  setGameListSearch,
  setSelectedGame,
} from "./gameSlice";
import { GameState, JoinGameData } from "./types";

export const refreshGames =
  (gameState: GameState, resetSelection = false) =>
  async (dispatch: Function, getState: Function): Promise<void> => {
    const query = selectGameListSearch(getState());
    await dispatch(fetchGames(gameState, query, resetSelection));
  };

export const fetchGames =
  (state: GameState, query: string, resetSelection = false) =>
  async (dispatch: Function): Promise<void> => {
    dispatch(setGameSearch(query));
    const gameList = await GameService.getGames(state, query);
    dispatch(setGameList({ state, list: gameList }));
    if (resetSelection) dispatch(setSelectedGame(gameList[0].id));
  };

export const fetchDetails =
  (gameId: string, forceReload: boolean = false) =>
  async (dispatch: Function, getState: Function): Promise<void> => {
    if (!forceReload && !!selectGameDetails(gameId)(getState())) return;
    const details = await GameService.getGameDetails(gameId);
    dispatch(setGameDetails(details));
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

export const startGame = async (
  gameId: string,
  state: GameState
): Promise<void> => {
  await GameService.startGame(gameId, state);
};

export const setGameSearch =
  (search: string) =>
  (dispatch: Function): void => {
    dispatch(setGameListSearch(search));
  };

export const transferMoney = async (
  gameId: string,
  payerId: string,
  receiverId: string,
  amount: number
): Promise<void> => {
  await GameService.transferMoney(gameId, payerId, receiverId, amount);
};
