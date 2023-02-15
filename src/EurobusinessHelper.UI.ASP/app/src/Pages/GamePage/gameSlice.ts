import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { RootState } from "../../app/store";
import { Game, GameInfo, GameInfoList, GameState } from "./types";

interface GameListInfo {
  state: GameState;
  list: Game[];
}

const gameSlice = createSlice({
  name: "game",
  initialState: {
    gameList: {
      new: [] as Game[],
      started: [] as Game[],
    },
    gameDetails: {} as GameInfoList,
    gameListSearch: "" as string,
    selectedGame: "" as string,
    isNewGame: false as boolean,
    showGames: GameState.New as GameState,
  },
  reducers: {
    setGameList: (state, action: PayloadAction<GameListInfo>) => {
      if (action.payload.state === GameState.New)
        state.gameList.new = action.payload.list;
      else if (action.payload.state === GameState.Started)
        state.gameList.started = action.payload.list;
    },
    setGameDetails: (state, action: PayloadAction<GameInfo>) => {
      state.gameDetails[action.payload.id] = action.payload;
    },
    setGameListSearch: (state, action: PayloadAction<string>) => {
      state.gameListSearch = action.payload;
    },
    setSelectedGame: (state, action: PayloadAction<string>) => {
      state.selectedGame = action.payload;
    },
    setIsNewGame: (state, action: PayloadAction<boolean>) => {
      state.isNewGame = action.payload;
    },
    setShowGames: (state, action: PayloadAction<GameState>) => {
      state.showGames = action.payload;
    },
  },
});

export const {
  setGameList,
  setGameDetails,
  setGameListSearch,
  setSelectedGame,
  setIsNewGame,
  setShowGames,
} = gameSlice.actions;

export const selectGameList =
  (gameState: GameState) =>
  (state: RootState): Game[] => {
    if (gameState === GameState.New) return state.game.gameList.new;
    else if (gameState === GameState.Started)
      return state.game.gameList.started;

    return [];
  };
export const selectGameDetails =
  (guid: string) =>
  (state: RootState): GameInfo | undefined =>
    state.game.gameDetails[guid];
export const selectGameListSearch = (state: RootState): string =>
  state.game.gameListSearch;
export const selectSelectedGame = (state: RootState): string =>
  state.game.selectedGame;
export const selectIsNewGame = (state: RootState): boolean =>
  state.game.isNewGame;
export const selectShowGame = (state: RootState): GameState =>
  state.game.showGames;

export default gameSlice.reducer;
