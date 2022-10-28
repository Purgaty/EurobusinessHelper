import AuthService from "../Services/Auth/AuthService";

export const getProviders = async (): Promise<string[]> => {
  return await AuthService.getProviders();
};

export const challenge = (providerName: string): void => {
  AuthService.challenge(providerName);
};
