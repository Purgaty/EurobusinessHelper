import { useEffect, useState } from "react";
import { IoMdAddCircleOutline } from "react-icons/io";
import { useSelector } from "react-redux";
import { useAppDispatch } from "../../app/hooks";
import { fetchDetails, refreshGames } from "./actions";
import CurrentGame from "./CurrentGame";
import { GameDetails } from "./GameDetails";
import GameList from "./GameList";
import "./GamePage.scss";
import GameSearch from "./GameSearch";
import { selectGameList } from "./gameSlice";
import { NewGame } from "./NewGame";
import { GameState } from "./types";

const GamePage = () => {
  const [selectedGame, setSelectedGame] = useState<string>("");
  const [isNewGame, setIsNewGame] = useState<boolean>(false);
  const [showGames, setShowGames] = useState<GameState>(GameState.New);
  const dispatch = useAppDispatch();
  const gameList = useSelector(selectGameList(GameState.New));
  const startedGamesList = useSelector(selectGameList(GameState.Started));

  useEffect(() => {
    if (selectedGame) dispatch(fetchDetails(selectedGame));
  }, [selectedGame, dispatch]);

  useEffect(() => {
    dispatch(refreshGames(GameState.New));
    dispatch(refreshGames(GameState.Started));
  }, [dispatch]);

  const checkGameList = () => {
    switch (showGames) {
      case GameState.New:
        return gameList;
      case GameState.Started:
        return startedGamesList;
      default:
        return gameList;
    }
  };

  return (
    <div className="game-page">
      <div className="container game-container">
        <GameSearch gameState={showGames} />
        <div className="games-switch">
          <p
            className={
              "games-switch-title " +
              (showGames === GameState.New && "current-list")
            }
            onClick={() => {
              setShowGames(GameState.New);
              setSelectedGame("");
            }}
          >
            New
          </p>
          <p
            className={
              "games-switch-title " +
              (showGames === GameState.Started && "current-list")
            }
            onClick={() => {
              setShowGames(GameState.Started);
              setSelectedGame("");
            }}
          >
            Started
          </p>
        </div>
        <GameList
          list={checkGameList()}
          setSelectedGame={setSelectedGame}
          setIsNewGame={setIsNewGame}
        />
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
        {!isNewGame && selectedGame && showGames === GameState.New && (
          <GameDetails
            gameId={selectedGame}
            clearSelectedGame={() => setSelectedGame("")}
            changeGameState={setShowGames}
          />
        )}
        {!isNewGame && selectedGame && showGames === GameState.Started && (
          <CurrentGame gameId={selectedGame} />
        )}
      </div>
    </div>
  );
};

export default GamePage;
