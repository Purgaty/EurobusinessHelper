import React, { useEffect, useState } from "react";
import { BiTrash } from "react-icons/bi";
import { useAppSelector } from "../../app/hooks";
import { selectIdentity } from "../../Layout/Footer/authSlice";
import { deleteGame, getGameDetails, joinGame } from "./actions";
import "./GameDetails.scss";
import { GameInfo, Player } from "./types";

export const GameDetails = ({ gameId }: { gameId: string }) => {
  const [details, setDetails] = useState<GameInfo>();
  const [password, setPassword] = useState<string>("");

  const identity = useAppSelector(selectIdentity);

  const getDetails = async () => {
    const data = await getGameDetails(gameId);
    setDetails(data);
  };

  const handleInput = (event: any) => {
    setPassword(event.target.value);
  };

  useEffect(() => {
    getDetails();
    setPassword("");
  }, [gameId]);

  return (
    <div className="game-details">
      <div className="game-title">
        <div className="game-name">{details?.name} </div>
        <div
          className="delete-game"
          style={{
            display:
              details?.createdBy.email === identity?.email ? "block" : "none",
          }}
          onClick={() => deleteGame(gameId)}
        >
          <BiTrash />
        </div>
      </div>
      <div className="dates">
        <div className="created">Created on: {details?.createdOn}</div>
        <div className="modified">Last modified: {details?.modifiedOn}</div>
      </div>
      <div className="tags">
        <div className="state">{details?.state}</div>
        <div className="is-active">
          {details?.isActive ? (
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
        <div className="players">Players: {details?.accountCount}</div>
      </div>
      <div className="details-block">
        <div className="players-list">
          Players:
          {details?.accounts?.map((player: Player) => (
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
                display: details?.isPasswordProtected ? "block" : "none",
              }}
            />
          </div>
          <div
            className="button button-hover join-button"
            onClick={async () => {
              await joinGame(gameId, { password });
              getDetails();
            }}
          >
            <p className="text">Join</p>
          </div>
        </div>
      </div>
    </div>
  );
};
