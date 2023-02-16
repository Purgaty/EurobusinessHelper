import { useCallback } from "react";
import { FaLock, FaLockOpen } from "react-icons/fa";
import { Tooltip } from "react-tooltip";
import "react-tooltip/dist/react-tooltip.css";
import { useAppDispatch } from "../../app/hooks";
import "./GameList.scss";
import { setOpenGameMode, setSelectedGame } from "./gameSlice";
import { Game, GameState } from "./types";

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
              data-tooltip-delay-show={500}
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
