import { GameState } from "../../../Pages/GamePage/types";
import { GameOperatingLog } from "./types";

export type GameChangedNotification = () => void;
export type RequestBankTransferApproval = (
  requestId: string,
  accountId: string,
  amount: number
) => void;
export type GameListChangedNotification = (state: GameState) => void;
export type RequestMoneyTransfer = (accountId: string, amount: number) => void;
export type CreateOperationLog = (
  logType: GameOperatingLog,
  toAccount: string,
  amount: number,
  fromAccount: string
) => void;
