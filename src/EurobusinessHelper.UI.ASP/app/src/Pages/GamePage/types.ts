export interface Game {
  isPasswordProtected: boolean;
  name: string;
  id: string;
}

export interface NewGame extends Game {
  password: string;
}
