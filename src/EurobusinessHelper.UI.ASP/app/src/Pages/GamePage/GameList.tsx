import { useCallback } from "react";
import { FaLock, FaLockOpen } from "react-icons/fa";
import { Tooltip } from "react-tooltip";
import "react-tooltip/dist/react-tooltip.css";
import { useAppDispatch } from "../../app/hooks";
import "./GameList.scss";
import { setIsNewGame, setSelectedGame } from "./gameSlice";
import { Game } from "./types";

export interface GameListProps {
  list: Game[];
}

export const GameList = ({ list }: GameListProps) => {
  const dispatch = useAppDispatch();

  const onGameClick = useCallback(
    (gameId: string) => {
      dispatch(setSelectedGame(gameId));
      dispatch(setIsNewGame(false));
    },
    [dispatch]
  );

  return (
    <div className="game-list">
      {list?.map((game, i) => {
        return (
          <div
            className="game-list-item"
            key={i}
            onClick={() => onGameClick(game.id)}
          >
            <Tooltip
              anchorId={`game-${game.id}`}
              className="tooltip"
              positionStrategy="fixed"
            />
            <div
              className="game"
              id={`game-${game.id}`}
              data-tooltip-content={game.name}
              data-tooltip-place="bottom"
            >
              <p className="game-name">{game.name}</p>

              <div className="lock-icon">
                {game.isPasswordProtected ? <FaLock /> : <FaLockOpen />}
              </div>
            </div>
          </div>
        );
      })}
    </div>
  );
};

export default GameList;
