import React, { useState } from "react";
import { MdOutlineVideogameAssetOff } from "react-icons/md";
import { useDispatch, useSelector } from "react-redux";
import { useAppSelector } from "../../../app/hooks";
import { selectIdentity } from "../../../Layout/Footer/authSlice";
import { SignalrTest } from "../../../Layout/SignalrTest";
import Loader from "../../Loader";
import { bankRequest, changeGameState, refreshGames } from "../actions";
import { selectGameDetails } from "../gameSlice";
import { GameState, Player } from "../types";
import "./CurrentGame.scss";
import CurrentPlayers from "./CurrentPlayers";

interface CurrentGameProps {
  gameId: string;
}

export const CurrentGame = ({ gameId }: CurrentGameProps) => {
  const [amount, setAmount] = useState<string>("");

  const gameDetails = useSelector(selectGameDetails(gameId));
  const identity = useAppSelector(selectIdentity);

  const dispatch = useDispatch();

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
        <SignalrTest
          accountId={
            gameDetails.accounts.filter((a) => a.email === identity?.email)[0]
              .id
          }
        />
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
          {gameDetails?.accounts?.map((player: Player) => (
            <CurrentPlayers
              gameId={gameId}
              player={player}
              getCurrentPlayerId={getCurrentPlayerId}
              key={player.id}
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
              onClick={() => bankRequest(getCurrentPlayerId(), +amount)}
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
