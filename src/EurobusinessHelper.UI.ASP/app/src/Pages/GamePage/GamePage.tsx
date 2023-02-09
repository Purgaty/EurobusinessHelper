import { useCallback, useEffect, useState } from "react";
import { FaLock, FaLockOpen } from "react-icons/fa";
import { IoMdAddCircleOutline } from "react-icons/io";
import { useSelector } from "react-redux";
import { useAppDispatch } from "../../app/hooks";
import { fetchDetails, refreshGames } from "./actions";
import { GameDetails } from "./GameDetails";
import "./GamePage.scss";
import GameSearch from "./GameSearch";
import { selectGameList } from "./gameSlice";
import { NewGame } from "./NewGame";

const GamePage = () => {
  const [selectedGame, setSelectedGame] = useState<string>("");
  const [isNewGame, setIsNewGame] = useState<boolean>(false);
  const dispatch = useAppDispatch();
  const gameList = useSelector(selectGameList);

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
  }, []);

  return (
    <div className="game-page">
      <div className="container game-container">
        <GameSearch />
        <div className="game-list">
          {gameList?.map((game, i) => (
            <div className="game" key={i} onClick={() => onGameClick(game.id)}>
              <p className="game-name">{game.name}</p>
              <div className="lock-icon">
                {game.isPasswordProtected ? <FaLock /> : <FaLockOpen />}
              </div>
            </div>
          ))}
        </div>
        <div
          className="button button-hover add-game-button"
          onClick={() => setIsNewGame(true)}
        >
          <p className="add-text">Add Game</p>
          <IoMdAddCircleOutline className="add-icon" />
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
