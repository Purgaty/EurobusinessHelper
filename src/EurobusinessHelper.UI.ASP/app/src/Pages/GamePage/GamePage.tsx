import React, { useEffect } from "react";
import { useSelector } from "react-redux";
import GameHub from "../../Services/Hubs/GameHub";
import Loader from "../Loader";
import CurrentGame from "./CurrentGame/CurrentGame";
import { GameDetails } from "./GameDetails/GameDetails";
import "./GamePage.scss";
import {
  selectGameDetails,
  selectOpenGameMode,
  selectSelectedGame,
} from "./gameSlice";
import NewGame from "./NewGame/NewGame";
import { GameState } from "./types";

const getGameDetailsComponent = (
  openGameState: GameState,
  gameId: string | undefined
): JSX.Element => {
  if (openGameState === GameState.NotCreated) return <NewGame />;
  if (!gameId) return <Loader />;
  switch (openGameState) {
    case GameState.New:
      return <GameDetails gameId={gameId} />;
    case GameState.Started:
      return <CurrentGame gameId={gameId} />;
    default:
      return <Loader />;
  }
};

const GamePage = () => {
  const selectedGame = useSelector(
    selectGameDetails(useSelector(selectSelectedGame))
  );
  const openGameState = useSelector(selectOpenGameMode);

  return (
    <div className="game-page">
      <div className="container details-container">
        {getGameDetailsComponent(openGameState, selectedGame?.id)}
      </div>
    </div>
  );
};

export default GamePage;
