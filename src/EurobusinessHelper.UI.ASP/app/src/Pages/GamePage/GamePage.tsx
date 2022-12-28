import React, { useEffect, useState } from "react";
import { BiSearch } from "react-icons/bi";
import { FaLock, FaLockOpen } from "react-icons/fa";
import { IoMdAddCircleOutline } from "react-icons/io";
import { getGames } from "./actions";
import { GameDetails } from "./GameDetails";
import "./GamePage.scss";
import { NewGame } from "./NewGame";
import { Game } from "./types";

const GamePage = () => {
  const [games, setGames] = useState<Game[]>();
  const [search, setSearch] = useState<string>("");
  const [gameDetails, setGameDetails] = useState<any>();

  const showGames = async () => {
    const data = await getGames(search);
    setGames(data);
  };

  const handleInput = (event: any) => {
    setSearch(event.target.value);
  };

  useEffect(() => {
    showGames();
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
          <div className="button search-button" onClick={showGames}>
            <BiSearch />
          </div>
        </div>
        <div className="game-list">
          {games?.map((game, i) => (
            <div
              className="game"
              key={i}
              onClick={() => setGameDetails(<GameDetails gameId={game.id} />)}
            >
              <p className="game-name">{game.name}</p>
              <div className="lock-icon">
                {game.isPasswordProtected ? <FaLock /> : <FaLockOpen />}
              </div>
            </div>
          ))}
        </div>
        <div
          className="button button-hover add-game-button"
          onClick={() => setGameDetails(<NewGame />)}
        >
          <p className="add-text">Add Game</p>
          <IoMdAddCircleOutline className="add-icon" />
        </div>
      </div>
      <div className="container details-container">{gameDetails}</div>
    </div>
  );
};

export default GamePage;
