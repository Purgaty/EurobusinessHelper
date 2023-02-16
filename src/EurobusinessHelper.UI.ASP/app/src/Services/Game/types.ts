import { NewGameForm } from "../../Pages/GamePage/types";

export class NewGame {
  constructor(form: NewGameForm) {
    this.name = form.gameName;
    this.startingAccountBalance = +form.startingBalance;
    this.minimalBankTransferApprovals = +form.minTransferRequestApprovals;
    this.isPasswordProtected = form.hasPassword;
    this.password = form.password;
  }

  name: string;
  startingAccountBalance: number;
  minimalBankTransferApprovals: number;
  isPasswordProtected: boolean;
  password: string;
}
