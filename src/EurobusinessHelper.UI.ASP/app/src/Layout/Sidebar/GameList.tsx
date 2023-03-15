import React, { useCallback } from "react";
import { FaLock, FaLockOpen } from "react-icons/fa";
import { ImCross } from "react-icons/im";
import { Tooltip } from "react-tooltip";
import "react-tooltip/dist/react-tooltip.css";
import { useAppDispatch } from "../../app/hooks";
import { setOpenGameMode, setSelectedGame } from "../../Pages/GamePage/gameSlice";
import { Game, GameState } from "../../Pages/GamePage/types";
import "./GameList.scss";

export interface GameListProps {
  list: Game[];
  gameMode: GameState;
}

export const GameList = ({ list, gameMode }: GameListProps) => {
  const dispatch = useAppDispatch();

  const onGameClick = useCallback(
    (gameId: string) => {
      dispatch(setSelectedGame(gameId));
      dispatch(setOpenGameMode(gameMode));
    },
    [dispatch, gameMode]
  );

  return (
    <div className="game-list">
      {list.length > 0 &&
        list?.map((game, i) => {
          return (
            <div className="game-list-item" key={i}>
              <Tooltip anchorId={`game-${game.id}`} className="tooltip" positionStrategy="fixed" />
              <div
                className="game"
                id={`game-${game.id}`}
                data-tooltip-content={game.name}
                data-tooltip-place="bottom"
                data-tooltip-delay-show={500}
              >
                <p className="game-name" onClick={() => onGameClick(game.id)}>
                  {game.name}
                </p>

                <div className="lock-icon">
                  {game.isPasswordProtected ? <FaLock /> : <FaLockOpen />}
                </div>
              </div>
            </div>
          );
        })}
      {list.length === 0 && (
        <div className="no-results-block">
          <ImCross className="no-results-icon" />
          <div className="no-restults-text">No results</div>
        </div>
      )}
    </div>
  );
};

export default GameList;
