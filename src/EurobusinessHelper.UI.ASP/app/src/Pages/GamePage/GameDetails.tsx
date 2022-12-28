import React, { useEffect, useState } from "react";
import { getGameDetails } from "./actions";
import "./GameDetails.scss";
import { GameInfo, Player } from "./types";

export const GameDetails = ({ gameId }: { gameId: string }) => {
  const [details, setDetails] = useState<GameInfo | any>();
  const getDetails = async () => {
    const data = await getGameDetails(gameId);
    setDetails(data);
  };

  useEffect(() => {
    getDetails();
  }, [gameId]);

  return (
    <div className="game-details">
      <div className="game-name">{details?.name} </div>
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

        <div className="game-buttons">
          <div className="button button-hover join-button">
            <p className="text">Join</p>
          </div>
          <div className="button  button-hover delete-button">
            <p className="text">Delete</p>
          </div>
        </div>
      </div>
    </div>
  );
};
