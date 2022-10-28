import { setProviders } from "../Layout/Footer/authSlice";
import AuthService from "../Services/Auth/AuthService";

export const challenge = (providerName: string): void => {
  AuthService.challenge(providerName);
};

export const fetchProviders = () => async (dispatch: Function) => {
  const providers = await getProviders();
  dispatch(setProviders(providers));
};

const getProviders = async (): Promise<string[]> => {
  return await AuthService.getProviders();
};
