import { useCallback, useEffect, useMemo, useState } from "react";
import { BiTrash } from "react-icons/bi";
import Moment from "react-moment";
import { useDispatch, useSelector } from "react-redux";
import { Tooltip } from "react-tooltip";
import { useAppSelector } from "../../app/hooks";
import { selectIdentity } from "../../Layout/Footer/authSlice";
import {
  changeGameState,
  deleteGame,
  fetchDetails,
  getErrorMessage,
  joinGame,
  refreshGames,
} from "./actions";
import "./GameDetails.scss";
import { selectGameDetails, setOpenGameMode, setShowGames } from "./gameSlice";
import Loader from "./Loader";
import { GameState, Player } from "./types";

export interface GameDetailsProps {
  gameId: string;
}

export const GameDetails = ({ gameId }: GameDetailsProps) => {
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
      setErrorMessage(getErrorMessage(error.response.data.ErrorCode));
      setTimeout(() => setErrorMessage(""), 3000);
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
                dispatch(refreshGames(GameState.New, true));
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
            <div className="min-approvals">
              Min Approvals: {gameDetails?.minimalBankTransferApprovals}
            </div>
            <div className="players">Players: {gameDetails?.accountCount}</div>
          </div>
          {gameDetails?.createdBy.email === identity?.email && (
            <div className="start-block">
              <div
                className="button button-hover start-button"
                onClick={async () => {
                  await changeGameState(gameDetails?.id, GameState.Started);
                  dispatch(refreshGames(GameState.New));
                  dispatch(fetchDetails(gameId, true));
                  dispatch(setShowGames(GameState.Started));
                  dispatch(setOpenGameMode(GameState.Started));
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
                      id="password-input"
                      data-tooltip-content={errorMessage}
                      data-tooltip-place="top"
                      data-tooltip-variant="error"
                      value={password}
                      onChange={(e) => setPassword(e.target.value)}
                    />

                    <Tooltip
                      anchorId="password-input"
                      isOpen={errorMessage === "" ? false : true}
                    />
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
