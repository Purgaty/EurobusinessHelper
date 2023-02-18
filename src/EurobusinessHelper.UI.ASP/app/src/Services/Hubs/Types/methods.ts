import {GameState} from "../../../Pages/GamePage/types";

export type GameChangedNotification = () => void;
export type RequestBankTransferApproval = (requestId: string, accountId: string, amount: number) => void;
export type GameListChangedNotification = (state: GameState) => void;