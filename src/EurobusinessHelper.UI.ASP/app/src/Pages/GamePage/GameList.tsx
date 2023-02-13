import { useCallback } from "react";
import { FaLock, FaLockOpen } from "react-icons/fa";
import tippy from "tippy.js";
import "tippy.js/dist/tippy.css";
import "./GameList.scss";
import { Game } from "./types";

export interface GameListProps {
  list: Game[];
  setSelectedGame: Function;
  setIsNewGame: Function;
}

export const GameList = ({
  list,
  setSelectedGame,
  setIsNewGame,
}: GameListProps) => {
  const onGameClick = useCallback(
    (gameId: string) => {
      setSelectedGame(gameId);
      setIsNewGame(false);
    },
    [setSelectedGame, setIsNewGame]
  );

  return (
    <>
      {list?.map((game, i) => {
        tippy(`#game-name-${i}`, {
          content: game.name,
        });
        return (
          <div
            className="game-list-item"
            key={i}
            onClick={() => onGameClick(game.id)}
          >
            <div className="game">
              <p className="game-name" id={`game-name-${i}`}>
                {game.name}
              </p>
              <div className="lock-icon">
                {game.isPasswordProtected ? <FaLock /> : <FaLockOpen />}
              </div>
            </div>
          </div>
        );
      })}
    </>
  );
};

export default GameList;
