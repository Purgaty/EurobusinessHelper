import { Action, configureStore, ThunkAction } from "@reduxjs/toolkit";
import authSlice from "../Layout/Footer/authSlice";
import gameSlice from "../Pages/GamePage/gameSlice";

export const store = configureStore({
  reducer: {
    auth: authSlice,
    game: gameSlice,
  },
});

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;
export type AppThunk<ReturnType = void> = ThunkAction<
  ReturnType,
  RootState,
  unknown,
  Action<string>
>;
