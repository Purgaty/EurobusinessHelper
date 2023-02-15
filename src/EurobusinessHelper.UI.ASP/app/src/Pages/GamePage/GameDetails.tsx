import { useCallback, useEffect, useMemo, useState } from "react";
import { BiTrash } from "react-icons/bi";
import Moment from "react-moment";
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
  changeGameState: Function;
}

export const GameDetails = ({
  gameId,
  clearSelectedGame,
  changeGameState,
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
      dispatch(refreshGames(GameState.New));
    } catch (error: any) {
      setErrorMessage("Incorrect password");
    }
  }, [dispatch, gameId, password]);

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

  const playerCheck = useMemo(checkPlayer, [identity, gameDetails]);
  if (gameDetails) {
    return (
      <div className="game-details">
        <div className="game-title">
          <div className="game-details-name">{gameDetails?.name} </div>
          {gameDetails?.createdBy.email === identity?.email && (
            <div
              className="delete-game"
              onClick={async () => {
                await deleteGame(gameId);
                dispatch(refreshGames(GameState.New));
                clearSelectedGame();
              }}
            >
              <BiTrash />
            </div>
          )}
        </div>
        <div className="dates">
          <div className="created">
            Created on:{" "}
            <Moment format="yyyy-MM-DD HH:mm">{gameDetails.createdOn}</Moment>
          </div>
          <div className="modified">
            Last modified:{" "}
            <Moment format="yyyy-MM-DD HH:mm">{gameDetails.modifiedOn}</Moment>
          </div>
        </div>
        <div className="line-block">
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
          {gameDetails?.createdBy.email === identity?.email &&
            gameDetails?.state !== GameState.Started && (
              <div className="start-block">
                <div
                  className="button button-hover start-button"
                  onClick={async () => {
                    await startGame(gameDetails?.id, GameState.Started);
                    dispatch(refreshGames(GameState.New));
                    dispatch(fetchDetails(gameId, true));
                    changeGameState(GameState.Started);
                  }}
                >
                  <p className="text">Start Game</p>
                </div>
              </div>
            )}
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
          {!playerCheck && (
            <div className="join-block">
              <div className="password">
                {gameDetails?.isPasswordProtected && (
                  <>
                    <input
                      type="password"
                      className="input password-input"
                      value={password}
                      onChange={(e) => setPassword(e.target.value)}
                    />
                    <div className="error-message">{errorMessage}</div>
                  </>
                )}
              </div>
              <div
                className="button button-hover join-button"
                onClick={async () => {
                  await onJoinGameClick();
                }}
              >
                <p className="text">Join</p>
              </div>
            </div>
          )}
        </div>
      </div>
    );
  } else {
    return <Loader />;
  }
};
