import { useCallback, useState } from "react";
import { useDispatch } from "react-redux";
import { Tooltip } from "react-tooltip";
import "react-tooltip/dist/react-tooltip.css";
import { fetchDetails, transferMoney } from "./actions";
import "./CurrentPlayers.scss";
import { Player } from "./types";

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
  const [amount, setAmount] = useState<number>(0);
  const dispatch = useDispatch();

  const handleTransfer = useCallback(async () => {
    if (amount === 0) {
      setErrorMessage("You can't transfer 0$");
      setTimeout(() => setErrorMessage(""), 2000);
      return;
    } else if (getCurrentPlayerId() !== player.id) {
      try {
        await transferMoney(gameId, getCurrentPlayerId(), player.id, amount);
        dispatch(fetchDetails(gameId, true));
      } catch (error: any) {
        setErrorMessage(error.response.data.Message);
        setTimeout(() => setErrorMessage(""), 2000);
      }
    } else {
      setErrorMessage("You can't transfer money to yourself");
      setTimeout(() => setErrorMessage(""), 2000);
    }
  }, [amount, dispatch, gameId, getCurrentPlayerId, player.id]);

  return (
    <div className="current-players-list-item">
      <div className="current-player">
        <div className="current-player-name">{player.email}</div>
        <div className="current-player-balance">{player.balance + "$"}</div>
      </div>
      <Tooltip
        anchorId={`input-${player.id}`}
        className="tooltip"
        isOpen={errorMessage === "" ? false : true}
      />
      <div className="amount-block">
        <input
          type="number"
          value={amount}
          id={`input-${player.id}`}
          data-tooltip-content={errorMessage}
          data-tooltip-place="top"
          data-tooltip-variant="error"
          className="input amount-input"
          placeholder="Value"
          onChange={(e) => setAmount(+e.target.value)}
        />
      </div>

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
  );
};

export default CurrentPlayers;
