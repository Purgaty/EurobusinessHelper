import IdentityService from "../../Services/Identity/IdentityService";
import {setIdentity} from "./authSlice";
import {Identity} from "./types";

const getCurrentIdentity = async (): Promise<Identity> => {
  return await IdentityService.getCurrentIdentity();
};

export const fetchCurrentIdentity = () => async (dispatch: Function) => {
  const identity = await getCurrentIdentity();
  dispatch(setIdentity(identity));
};
