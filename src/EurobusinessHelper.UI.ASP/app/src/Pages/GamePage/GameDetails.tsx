import React, { useEffect, useState } from "react";
import { BiTrash } from "react-icons/bi";
import { useDispatch, useSelector } from "react-redux";
import { useAppSelector } from "../../app/hooks";
import { selectIdentity } from "../../Layout/Footer/authSlice";
import { deleteGame, fetchDetails, fetchGames, joinGame } from "./actions";
import "./GameDetails.scss";
import { selectGameDetails } from "./gameSlice";
import { Player } from "./types";

export interface GameDetailsProps {
  gameId: string;
  clearSelectedGame: Function;
}

export const GameDetails = ({
  gameId,
  clearSelectedGame,
}: GameDetailsProps) => {
  const [password, setPassword] = useState<string>("");

  const identity = useAppSelector(selectIdentity);
  const gameDetails = useSelector(selectGameDetails(gameId));
  const dispatch = useDispatch();

  const handleInput = (event: any) => {
    setPassword(event.target.value);
  };

  useEffect(() => {
    setPassword("");
  }, [gameId]);

  return (
    <div className="game-details">
      <div className="game-title">
        <div className="game-name">{gameDetails?.name} </div>
        <div
          className="delete-game"
          style={{
            display:
              gameDetails?.createdBy.email === identity?.email
                ? "block"
                : "none",
          }}
          onClick={async () => {
            await deleteGame(gameId);
            dispatch(fetchGames(""));
            clearSelectedGame();
          }}
        >
          <BiTrash />
        </div>
      </div>
      <div className="dates">
        <div className="created">Created on: {gameDetails?.createdOn}</div>
        <div className="modified">Last modified: {gameDetails?.modifiedOn}</div>
      </div>
      <div className="tags">
        <div className="state">{gameDetails?.state}</div>
        <div className="is-active">
          {gameDetails?.isActive ? (
            <>
              <div className="active"></div>
              <div>Active</div>
            </>
          ) : (
            <>
              <div className="not-active"></div>
              <div>Not Active</div>
            </>
          )}
        </div>
        <div className="players">Players: {gameDetails?.accountCount}</div>
      </div>
      <div className="details-block">
        <div className="players-list">
          Players:
          {gameDetails?.accounts?.map((player: Player) => (
            <div className="player" key={player.id}>
              <div className="player-name">{player.name}</div>
              <div className="player-balance">{player.balance}$</div>
            </div>
          ))}
        </div>

        <div className="join-block">
          <div className="password">
            <input
              type="text"
              className="input password-input"
              value={password}
              onChange={handleInput}
              style={{
                display: gameDetails?.isPasswordProtected ? "block" : "none",
              }}
            />
          </div>
          <div
            className="button button-hover join-button"
            onClick={async () => {
              await joinGame(gameId, { password });
              dispatch(fetchDetails(gameId, true));
            }}
          >
            <p className="text">Join</p>
          </div>
        </div>
      </div>
    </div>
  );
};
