import React, { useCallback, useEffect, useState } from "react";
import { BiSearch } from "react-icons/bi";
import { FaLock, FaLockOpen } from "react-icons/fa";
import { IoMdAddCircleOutline } from "react-icons/io";
import { useSelector } from "react-redux";
import { useAppDispatch, useAppSelector } from "../../app/hooks";
import { fetchDetails, fetchGames } from "./actions";
import { GameDetails } from "./GameDetails";
import "./GamePage.scss";
import { selectGameDetails, selectGameList } from "./gameSlice";
import { NewGame } from "./NewGame";
import { Game } from "./types";

const GamePage = () => {
  const [search, setSearch] = useState<string>("");
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

  const handleInput = (event: any) => {
    setSearch(event.target.value);
  };
  useEffect(() => {
    if (selectedGame) dispatch(fetchDetails(selectedGame));
  }, [selectedGame, dispatch]);

  useEffect(() => {
    dispatch(fetchGames(search));
  }, []);

  return (
    <div className="game-page">
      <div className="container game-container">
        <div className="search-bar">
          <input
            type="text"
            className="input search-input"
            placeholder="Search..."
            value={search}
            onChange={handleInput}
          />
          <div
            className="button search-button"
            onClick={() => dispatch(fetchGames(search))}
          >
            <BiSearch />
          </div>
        </div>
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
