import IdentityService from "../../Services/Identity/IdentityService";
import { setIdentity } from "./authSlice";
import { Identity } from "./types";

const getCurrentIdentity = async (): Promise<Identity> => {
  const response = await IdentityService.getCurrentIdentity();
  return response;
};

export const fetchCurrentIdentity = () => async (dispatch: Function) => {
  const identity = await getCurrentIdentity();
  dispatch(setIdentity(identity));
};
