import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { RootState } from "../../app/store";
import { Identity } from "./types";

const authSlice = createSlice({
  name: "auth",
  initialState: { identity: undefined, providers: undefined },
  reducers: {
    setIdentity: (state, action: PayloadAction<any>) => {
      state.identity = action.payload;
    },
    setProviders: (state, action: PayloadAction<any>) => {
      state.providers = action.payload;
    },
  },
});

export const { setIdentity, setProviders } = authSlice.actions;

export const selectIdentity = (state: RootState): Identity | undefined =>
  state.auth.identity;

export const selectIsIdentityLoaded = (state: RootState) : boolean =>
    !!state.auth.identity
export const selectProviders = (state: RootState): string[] | undefined =>
  state.auth.providers;

export default authSlice.reducer;
