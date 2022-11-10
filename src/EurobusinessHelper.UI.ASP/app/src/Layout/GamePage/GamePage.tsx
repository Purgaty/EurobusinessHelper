import React, { useEffect, useState } from "react";
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
      <div className="games">
        {games?.map((game, i) => (
          <div className="container game-container" key={i}>
            <p>{game.name}</p>
          </div>
        ))}
      </div>
    </div>
  );
};

export default GamePage;
