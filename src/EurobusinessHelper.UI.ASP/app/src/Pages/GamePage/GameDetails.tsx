import { useCallback, useEffect, useState } from "react";
import { BiTrash } from "react-icons/bi";
import { useDispatch, useSelector } from "react-redux";
import { useAppSelector } from "../../app/hooks";
import { selectIdentity } from "../../Layout/Footer/authSlice";
import {
  deleteGame,
  fetchDetails,
  joinGame,
  refreshGames,
  startGame,
} from "./actions";
import "./GameDetails.scss";
import { selectGameDetails } from "./gameSlice";
import Loader from "./Loader";
import { GameState, Player } from "./types";

export interface GameDetailsProps {
  gameId: string;
  clearSelectedGame: Function;
}

export const GameDetails = ({
  gameId,
  clearSelectedGame,
}: GameDetailsProps) => {
  const [password, setPassword] = useState<string>("");
  const [errorMessage, setErrorMessage] = useState<string>("");

  const identity = useAppSelector(selectIdentity);
  const gameDetails = useSelector(selectGameDetails(gameId));
  const dispatch = useDispatch();

  const onJoinGameClick = useCallback(async () => {
    try {
      await joinGame(gameId, { password });
      dispatch(fetchDetails(gameId, true));
    } catch (error: any) {
      setErrorMessage("Incorrect password");
      error.preventDefault();
    }
  }, [dispatch, gameId, password]);

  const handleInput = (event: any) => {
    setPassword(event.target.value);
  };

  const checkPlayer = () => {
    let check = false;
    for (let i = 0; i < (gameDetails?.accounts?.length ?? 0); i++) {
      if (gameDetails?.accounts[i].email === identity?.email) check = true;
    }
    return check;
  };

  useEffect(() => {
    setPassword("");
    setErrorMessage("");
  }, [gameId]);

  if (gameDetails) {
    return (
      <div className="game-details">
        <div className="game-title">
          <div className="game-details-name">{gameDetails?.name} </div>
          <div
            className="delete-game"
            style={{
              display:
                gameDetails?.createdBy.email === identity?.email ? "" : "none",
            }}
            onClick={async () => {
              await deleteGame(gameId);
              dispatch(refreshGames());
              clearSelectedGame();
            }}
          >
            <BiTrash />
          </div>
        </div>
        <div className="dates">
          <div className="created">Created on: {gameDetails?.createdOn}</div>
          <div className="modified">
            Last modified: {gameDetails?.modifiedOn}
          </div>
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
                  display: gameDetails?.isPasswordProtected ? "" : "none",
                }}
              />
              <div className="error-message">{errorMessage}</div>
            </div>
            <div
              className="button button-hover join-button"
              onClick={async () => {
                await onJoinGameClick();
                dispatch(refreshGames());
                clearSelectedGame();
              }}
              style={{
                display: checkPlayer() ? "none" : "",
              }}
            >
              <p className="text">Join</p>
            </div>
            <div
              className="button button-hover start-button"
              onClick={async () => {
                await startGame(gameDetails?.id, GameState.Started);
                dispatch(refreshGames());
                dispatch(fetchDetails(gameId, true));
              }}
              style={{
                display:
                  gameDetails?.createdBy.email === identity?.email &&
                  gameDetails?.state !== GameState.Started
                    ? ""
                    : "none",
              }}
            >
              <p className="text">Start Game</p>
            </div>
          </div>
        </div>
      </div>
    );
  } else {
    return <Loader />;
  }
};
