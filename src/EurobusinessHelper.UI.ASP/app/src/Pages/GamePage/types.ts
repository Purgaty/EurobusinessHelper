export interface Game {
  isPasswordProtected: boolean;
  name: string;
  id: string;
  state: GameState;
}

export interface NewGame extends Game {
  password: string;
}

export interface GameInfo extends Game {
  createdBy: {
    id: string;
    email: string;
    firstName: string;
    lastName: string;
  };
  isActive: boolean;
  createdOn: string;
  modifiedOn: string;
  accounts: [Player];
  accountCount: number;
}

export interface Player {
  id: string;
  name: string;
  email: string;
  balance: number;
}

export interface JoinGameData {
  password: string;
}

export interface GameInfoList {
  [key: string]: GameInfo;
}

export enum GameState {
  New = "New",
  Started = "Started",
  Finished = "Finished",
}

export enum ErrorCodes {
  InternalAppError = "InternalAppError",
  UnauthorizedUser = "UnauthorizedUser",
  GameNotFound = "GameNotFound",
  AccountAlreadyExists = "AccountAlreadyExists",
  InvalidGamePassword = "InvalidGamePassword",
  PasswordNotProvided = "PasswordNotProvided",
  GameAccessDenied = "GameAccessDenied",
  InvalidGameStateChange = "InvalidGameStateChange",
  CannotJoinNotNewGame = "CannotJoinNotNewGame",
  StartingAccountBalanceNotProvided = "StartingAccountBalanceNotProvided",
  AccountNotFound = "AccountNotFound",
  InsufficientFunds = "InsufficientFunds",
  AccountNotRegistered = "AccountNotRegistered",
  MinimalBankTransferApprovalsNotProvided = "MinimalBankTransferApprovalsNotProvided",
  TransferRequestNotFound = "TransferRequestNotFound",
}
