import React, { useCallback, useEffect, useMemo, useState } from "react";
import { MdOutlineVideogameAssetOff } from "react-icons/md";
import { useDispatch, useSelector } from "react-redux";
import { selectIdentity } from "../../../Layout/Footer/authSlice";
import GameHub from "../../../Services/Hubs/GameHub";
import { GameOperatingLog } from "../../../Services/Hubs/Types/types";
import { useAppSelector } from "../../../app/hooks";
import Loader from "../../Loader";
import {
  approveRequest,
  bankRequest,
  bankTransfer,
  changeGameState,
  fetchDetails,
  getErrorMessage,
  refreshGames,
  requestMoney,
  transferMoney,
} from "../actions";
import { selectGameDetails } from "../gameSlice";
import { Account, GameState } from "../types";
import CurrentAccounts from "./CurrentAccounts";
import "./CurrentGame.scss";

interface CurrentGameProps {
  gameId: string;
}

export const CurrentGame = ({ gameId }: CurrentGameProps) => {
  const [amount, setAmount] = useState<string>("");
  const [errorMessage, setErrorMessage] = useState<string>("");
  const [operationLog, setOperationLog] = useState<string[]>([]);
  const [hub, setHub] = useState<GameHub | undefined>(undefined);

  const gameDetails = useSelector(selectGameDetails(gameId));
  const identity = useAppSelector(selectIdentity);

  const dispatch = useDispatch();
  const currentAccountId = useMemo(
    () => gameDetails?.accounts?.find((a) => a.email === identity?.email)?.id || "",
    [gameDetails, identity]
  );

  const getAccountNameAndEmail = useCallback(
    (accountId: string) => {
      const account = gameDetails?.accounts?.find((a) => a.id === accountId);

      return `${account?.name} (${account?.email})`;
    },
    [gameDetails]
  );

  const setLogMessage = useCallback(
    (logType: GameOperatingLog, toAccount: string, amount: number, fromAccount: string) => {
      const time = new Date().toLocaleTimeString();
      let logMessage = "";

      if (logType === GameOperatingLog.TransferCompleted) {
        logMessage = `[${time}] ${getAccountNameAndEmail(
          toAccount
        )} recieved $${amount} from ${getAccountNameAndEmail(fromAccount)}.`;
      } else if (logType === GameOperatingLog.BankTransferCompleted) {
        logMessage = `[${time}] ${getAccountNameAndEmail(
          toAccount
        )} recieved $${amount} from the bank.`;
      } else if (logType === GameOperatingLog.PaymentCompleted) {
        logMessage = `[${time}] ${getAccountNameAndEmail(
          fromAccount
        )} paid $${amount} to the bank.`;
      }
      setOperationLog([logMessage, ...operationLog]);
    },
    [getAccountNameAndEmail, operationLog]
  );

  useEffect(() => {
    if (!currentAccountId) return;
    const hub = new GameHub(
      () => dispatch(fetchDetails(gameId, true)),
      (requestId, accountTo, amount) => {
        if (
          window.confirm(
            `Account ${getAccountNameAndEmail(accountTo)} requested $${amount} from the bank.`
          )
        )
          approveRequest(requestId);
      },
      (account, amount) => {
        if (window.confirm(`Account ${getAccountNameAndEmail(account)} requested $${amount}.`))
          transferMoney(gameId, currentAccountId, account, amount);
      }
    );
    hub.initializeAccount(currentAccountId).then(() => setHub(hub));
  }, [currentAccountId, dispatch, gameId, getAccountNameAndEmail]);

  useEffect(() => {
    hub?.setOperationLog((logType, toAccount, amount, fromAccount) =>
      setLogMessage(logType, toAccount, amount, fromAccount)
    );
  });

  const showErrorMessage = useCallback(
    (message: string) => {
      setErrorMessage(message);
      setTimeout(() => setErrorMessage(""), 3000);
    },
    [setErrorMessage]
  );

  const validateTransferCommand = useCallback(
    (amount: string, account: Account) => {
      if (!amount) {
        showErrorMessage("Invalid amount");
        return false;
      }

      if (currentAccountId === account.id) {
        showErrorMessage("Invalid account");
        return false;
      }

      return true;
    },
    [currentAccountId, showErrorMessage]
  );

  const handleTransfer = useCallback(
    async (account: Account, amount: string) => {
      if (!validateTransferCommand(amount, account)) return;

      try {
        await transferMoney(gameId, currentAccountId, account.id, +amount);
        dispatch(fetchDetails(gameId, true));
      } catch (error: any) {
        showErrorMessage(getErrorMessage(error.response.data.ErrorCode));
      }
    },
    [validateTransferCommand, gameId, currentAccountId, dispatch, showErrorMessage]
  );

  const handleRequest = useCallback(
    async (account: Account, amount: string) => {
      if (!validateTransferCommand(amount, account)) return;

      try {
        await requestMoney(gameId, currentAccountId, account.id, +amount);
        dispatch(fetchDetails(gameId, true));
      } catch (error: any) {
        showErrorMessage(getErrorMessage(error.response.data.ErrorCode));
      }
    },
    [validateTransferCommand, gameId, currentAccountId, dispatch, showErrorMessage]
  );

  if (gameDetails && identity) {
    return (
      <div className="current-game-container">
        <div className="current-game-title">
          {gameDetails?.name}
          {gameDetails?.createdBy.email === identity?.email && (
            <div
              className="finish-icon"
              onClick={async () => {
                await changeGameState(gameDetails?.id, GameState.Finished);
                dispatch(refreshGames(GameState.Started, true));
              }}
            >
              <MdOutlineVideogameAssetOff />
            </div>
          )}
        </div>

        <div className="current-players-list">
          {gameDetails?.accounts?.map((account: Account) => (
            <CurrentAccounts
              account={account}
              currentAccountId={currentAccountId}
              key={account.id}
              handleTransfer={handleTransfer}
              handleRequest={handleRequest}
              errorMessage={errorMessage}
            />
          ))}
        </div>
        <div className="bottom-block">
          <div className="bank-block">
            <p className="rquest-text">Bank Transactions:</p>
            <input
              type="text"
              value={amount}
              className="input amount-input"
              placeholder="Value"
              onChange={(e) => setAmount(e.target.value)}
            />
            <div className="buttons-block">
              <div
                className="button button-hover request-button"
                onClick={() => bankRequest(currentAccountId, +amount)}
              >
                <p className="text">Request</p>
              </div>
              <div
                className="button button-hover transfer-button"
                onClick={() => bankTransfer(gameId, currentAccountId, +amount)}
              >
                <p className="text">Transfer</p>
              </div>
            </div>
          </div>
          <div className="log-container">
            <div className="log-messages">
              {operationLog.map((entry, i) => {
                return (
                  <p className="log-entry" key={i}>
                    {entry}
                  </p>
                );
              })}
            </div>
          </div>
        </div>
      </div>
    );
  } else {
    return <Loader />;
  }
};

export default CurrentGame;
