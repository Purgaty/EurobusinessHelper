import React, { useCallback, useState } from "react";
import { useDispatch } from "react-redux";
import { Tooltip } from "react-tooltip";
import "react-tooltip/dist/react-tooltip.css";
import { fetchDetails, getErrorMessage, transferMoney } from "../actions";
import { Player } from "../types";
import "./CurrentPlayers.scss";

interface CurrentPlayersProps {
  gameId: string;
  player: Player;
  getCurrentPlayerId: Function;
}

export const CurrentPlayers = ({
  gameId,
  player,
  getCurrentPlayerId,
}: CurrentPlayersProps) => {
  const [errorMessage, setErrorMessage] = useState<string>("");
  const [amount, setAmount] = useState<string>("");
  const dispatch = useDispatch();

  const showErrorMessage = useCallback(
    (message: string) => {
      setErrorMessage(message);
      setTimeout(() => setErrorMessage(""), 3000);
    },
    [setErrorMessage]
  );

  const handleTransfer = useCallback(async () => {
    if (!amount) {
      showErrorMessage("Invalid amount");
      return;
    } else if (getCurrentPlayerId() !== player.id) {
      try {
        await transferMoney(gameId, getCurrentPlayerId(), player.id, +amount);
        dispatch(fetchDetails(gameId, true));
      } catch (error: any) {
        showErrorMessage(getErrorMessage(error.response.data.ErrorCode));
      }
    }
  }, [
    amount,
    dispatch,
    gameId,
    getCurrentPlayerId,
    player.id,
    showErrorMessage,
  ]);

  return (
    <div className="current-players-list-item">
      <div className="current-player">
        <div className="current-player-title">
          <p className="current-player-name">{player.name}</p>
          <p className="current-player-email">{player.email}</p>
        </div>
        <div className="current-player-balance">{player.balance + "$"}</div>
      </div>
      {player.id !== getCurrentPlayerId() && (
        <div className="transfer-block">
          <Tooltip
            anchorId={`input-${player.id}`}
            className="tooltip"
            isOpen={errorMessage === "" ? false : true}
          />
          <input
            type="number"
            value={amount}
            id={`input-${player.id}`}
            data-tooltip-content={errorMessage}
            data-tooltip-place="top"
            data-tooltip-variant="error"
            className="input amount-input"
            placeholder="Value"
            onChange={(e) => setAmount(e.target.value)}
          />

          <div
            className="button button-hover transfer-button"
            onClick={handleTransfer}
          >
            <p className="text">Transfer</p>
          </div>
          <div className="button button-hover request-button">
            <p className="text">Request</p>
          </div>
        </div>
      )}
    </div>
  );
};

export default CurrentPlayers;
