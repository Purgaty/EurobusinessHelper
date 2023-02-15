import { useState } from "react";
import { useSelector } from "react-redux";
import { useAppSelector } from "../../app/hooks";
import { selectIdentity } from "../../Layout/Footer/authSlice";
import "./CurrentGame.scss";
import CurrentPlayers from "./CurrentPlayers";
import { selectGameDetails } from "./gameSlice";
import Loader from "./Loader";
import { Player } from "./types";

interface CurrentGameProps {
  gameId: string;
}

export const CurrentGame = ({ gameId }: CurrentGameProps) => {
  const [amount, setAmount] = useState<number>(0);

  const gameDetails = useSelector(selectGameDetails(gameId));
  const identity = useAppSelector(selectIdentity);

  const getCurrentPlayerId = () => {
    let playerId = "";
    for (let i = 0; i < (gameDetails?.accounts?.length ?? 0); i++) {
      if (gameDetails?.accounts[i].email === identity?.email)
        playerId = gameDetails?.accounts[i].id || "";
    }
    return playerId;
  };

  if (gameDetails && identity) {
    return (
      <div className="current-game-container">
        <div className="current-game-title">{gameDetails?.name}</div>
        <div className="current-players-list">
          {gameDetails?.accounts?.map((player: Player) => (
            <CurrentPlayers
              gameId={gameId}
              player={player}
              getCurrentPlayerId={getCurrentPlayerId}
              key={player.id}
            />
          ))}
        </div>
        <div className="request-block">
          <p className="text">Bank Request:</p>
          <input
            type="number"
            value={amount}
            className="input amount-input"
            placeholder="Value"
            onChange={(e) => setAmount(+e.target.value)}
          />
          <div
            className="button button-hover request-button"
            onClick={() => console.log(amount)}
          >
            <p className="text">Request</p>
          </div>
        </div>
      </div>
    );
  } else {
    return <Loader />;
  }
};

export default CurrentGame;
