import { setStatusBarBackgroundColor } from "expo-status-bar";
import initialState from "../initialState";

export const actions = {
  SET_ACCOUNT: "SET_ACCOUNT"
};

export default (state = initialState.config, action) => {
  switch(action) {
    case actions.SET_ACCOUNT:
      return {...state, account: action};
    default:
      return state;
  }
}