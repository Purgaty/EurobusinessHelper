import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { RootState } from "../../app/store";
import { Game, GameInfo, GameInfoList } from "./types";

const gameSlice = createSlice({
  name: "game",
  initialState: { gameList: [] as Game[], gameDetails: {} as GameInfoList },
  reducers: {
    setGameList: (state, action: PayloadAction<Game[]>) => {
      state.gameList = action.payload;
    },
    setGameDetails: (state, action: PayloadAction<GameInfo>) => {
      state.gameDetails[action.payload.id] = action.payload;
    },
  },
});

export const { setGameList, setGameDetails } = gameSlice.actions;

export const selectGameList = (state: RootState): Game[] => state.game.gameList;
export const selectGameDetails =
  (guid: string) =>
  (state: RootState): GameInfo | undefined =>
    state.game.gameDetails[guid];

export default gameSlice.reducer;
