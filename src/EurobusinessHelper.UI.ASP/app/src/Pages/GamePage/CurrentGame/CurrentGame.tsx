import React, { useCallback, useEffect, useMemo, useState } from "react";
import { MdOutlineVideogameAssetOff } from "react-icons/md";
import { useDispatch, useSelector } from "react-redux";
import { useAppSelector } from "../../../app/hooks";
import { selectIdentity } from "../../../Layout/Footer/authSlice";
import GameHub from "../../../Services/Hubs/GameHub";
import Loader from "../../Loader";
import {
  approveRequest,
  bankRequest,
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
  const [, setHub] = useState<GameHub | undefined>(undefined);

  const gameDetails = useSelector(selectGameDetails(gameId));
  const identity = useAppSelector(selectIdentity);

  const dispatch = useDispatch();
  const currentAccountId = useMemo(
    () =>
      gameDetails?.accounts?.find((a) => a.email === identity?.email)?.id || "",
    [gameDetails, identity]
  );

  useEffect(() => {
    if (!currentAccountId) return;
    const hub = new GameHub(
      () => dispatch(fetchDetails(gameId, true)),
      (requestId, accountTo, amount) => {
        if (
          window.confirm(
            `Account ${accountTo} requested $${amount}. Request id: ${requestId}`
          )
        )
          approveRequest(requestId);
      },
      (account, amount) => {
        if (window.confirm(`Account ${account} requested $${amount}`))
          transferMoney(gameId, currentAccountId, account, amount);
      }
    );
    hub.initializeAccount(currentAccountId).then(() => setHub(hub));
  }, [currentAccountId, dispatch, gameId]);

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
    [
      validateTransferCommand,
      gameId,
      currentAccountId,
      dispatch,
      showErrorMessage,
    ]
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
    [
      validateTransferCommand,
      gameId,
      currentAccountId,
      dispatch,
      showErrorMessage,
    ]
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
          <div className="request-block">
            <p className="rquest-text">Bank Request:</p>
            <input
              type="text"
              value={amount}
              className="input amount-input"
              placeholder="Value"
              onChange={(e) => setAmount(e.target.value)}
            />
            <div
              className="button button-hover request-button"
              onClick={() => bankRequest(currentAccountId, +amount)}
            >
              <p className="text">Request</p>
            </div>
          </div>
          <div className="log-container"></div>
        </div>
      </div>
    );
  } else {
    return <Loader />;
  }
};

export default CurrentGame;
