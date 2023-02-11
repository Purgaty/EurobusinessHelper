import { useCallback, useEffect, useState } from "react";
import { FaLock, FaLockOpen } from "react-icons/fa";
import { IoMdAddCircleOutline } from "react-icons/io";
import { useSelector } from "react-redux";
import tippy from "tippy.js";
import "tippy.js/dist/tippy.css";
import { useAppDispatch } from "../../app/hooks";
import { fetchDetails, refreshGames } from "./actions";
import { GameDetails } from "./GameDetails";
import "./GamePage.scss";
import GameSearch from "./GameSearch";
import { selectGameList, selectMyGameList } from "./gameSlice";
import { NewGame } from "./NewGame";
import { Game } from "./types";

const GamePage = () => {
  const [selectedGame, setSelectedGame] = useState<string>("");
  const [isNewGame, setIsNewGame] = useState<boolean>(false);
  const [showMyGames, setShowMyGames] = useState<boolean>(false);
  const dispatch = useAppDispatch();
  const gameList = useSelector(selectGameList);
  const myGameList = useSelector(selectMyGameList);

  const onGameClick = useCallback(
    (gameId: string) => {
      setSelectedGame(gameId);
      setIsNewGame(false);
    },
    [setSelectedGame, setIsNewGame]
  );
  useEffect(() => {
    if (selectedGame) dispatch(fetchDetails(selectedGame));
  }, [selectedGame, dispatch]);

  useEffect(() => {
    dispatch(refreshGames());
  }, [dispatch]);

  const showGameList = (list: Game[]) => {
    return list?.map((game, i) => {
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
    });
  };

  return (
    <div className="game-page">
      <div className="container game-container">
        <GameSearch />
        <div className="game-list">
          {showMyGames ? showGameList(myGameList) : showGameList(gameList)}
        </div>
        <div
          className="button button-hover add-game-button"
          onClick={() => setIsNewGame(true)}
        >
          <p className="add-text">Add Game</p>
          <IoMdAddCircleOutline className="add-icon" />
        </div>
        <div className="my-games-switch">
          <p className="my-games-title">Joinable</p>
          <label className="switch">
            <input
              type="checkbox"
              onChange={() => setShowMyGames(!showMyGames)}
            />
            <span className="slider round"></span>
          </label>
          <p className="my-games-title">Mine</p>
        </div>
      </div>
      <div className="container details-container">
        {isNewGame && <NewGame />}
        {!isNewGame && selectedGame && (
          <GameDetails
            gameId={selectedGame}
            clearSelectedGame={() => setSelectedGame("")}
          />
        )}
      </div>
    </div>
  );
};

export default GamePage;
