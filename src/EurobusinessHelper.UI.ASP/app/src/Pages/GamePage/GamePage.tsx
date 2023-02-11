import { useEffect, useState } from "react";
import { IoMdAddCircleOutline } from "react-icons/io";
import { useSelector } from "react-redux";
import { useAppDispatch } from "../../app/hooks";
import { fetchDetails, refreshGames } from "./actions";
import { GameDetails } from "./GameDetails";
import GameList from "./GameList";
import "./GamePage.scss";
import GameSearch from "./GameSearch";
import { selectGameList, selectMyGameList } from "./gameSlice";
import { NewGame } from "./NewGame";

const GamePage = () => {
  const [selectedGame, setSelectedGame] = useState<string>("");
  const [isNewGame, setIsNewGame] = useState<boolean>(false);
  const [showMyGames, setShowMyGames] = useState<boolean>(false);
  const dispatch = useAppDispatch();
  const gameList = useSelector(selectGameList);
  const myGameList = useSelector(selectMyGameList);

  useEffect(() => {
    if (selectedGame) dispatch(fetchDetails(selectedGame));
  }, [selectedGame, dispatch]);

  useEffect(() => {
    dispatch(refreshGames());
  }, [dispatch]);

  return (
    <div className="game-page">
      <div className="container game-container">
        <GameSearch />
        <div className="game-list">
          {showMyGames ? (
            <GameList
              list={myGameList}
              setSelectedGame={setSelectedGame}
              setIsNewGame={setIsNewGame}
            />
          ) : (
            <GameList
              list={gameList}
              setSelectedGame={setSelectedGame}
              setIsNewGame={setIsNewGame}
            />
          )}
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
