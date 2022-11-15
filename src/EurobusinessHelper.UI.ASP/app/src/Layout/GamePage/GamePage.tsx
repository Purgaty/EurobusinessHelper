import React, { useEffect, useState } from "react";
import { BiSearch } from "react-icons/bi";
import { FaLock, FaLockOpen } from "react-icons/fa";
import { getGames } from "./actions";
import "./GamePage.scss";
import { Game } from "./types";

const GamePage = () => {
  const [games, setGames] = useState<Game[]>();

  const showGames = async () => {
    const data = await getGames();
    setGames(data);
  };

  useEffect(() => {
    showGames();
  }, []);

  return (
    <div className="game-page">
      <div className="container game-container">
        <div className="search-bar">
          <input type="text" className="input" placeholder="Search..." />
          <div className="button search-button">
            <BiSearch />
          </div>
        </div>
        <div className="game-list">
          {games?.map((game, i) => (
            <div className="game" key={i}>
              <p className="game-name">{game.name}</p>
              <div className="lock-icon">
                {game.isPasswordProtected ? <FaLock /> : <FaLockOpen />}
              </div>
            </div>
          ))}
        </div>
      </div>
      <div className="container details-container"></div>
    </div>
  );
};

export default GamePage;
