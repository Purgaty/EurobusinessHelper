export interface Game {
  isPasswordProtected: boolean;
  name: string;
  id: string;
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
  state: string;
  accounts: [Player];
  accountCount: number;
}

export interface Player {
  id: string;
  name: string;
  email: string;
  balance: number;
}
