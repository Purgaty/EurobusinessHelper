import { useEffect } from "react";
import { useSelector } from "react-redux";
import { useAppDispatch } from "../../app/hooks";
import { fetchDetails, refreshGames } from "./actions";
import CurrentGame from "./CurrentGame";
import { GameDetails } from "./GameDetails";
import "./GamePage.scss";
import {
  selectGameDetails,
  selectIsNewGame,
  selectSelectedGame,
} from "./gameSlice";
import Loader from "./Loader";
import { NewGame } from "./NewGame";
import { GameState } from "./types";

const GamePage = () => {
  const dispatch = useAppDispatch();
  const selectedGame = useSelector(
    selectGameDetails(useSelector(selectSelectedGame))
  );
  const isNewGame = useSelector(selectIsNewGame);

  useEffect(() => {
    if (selectedGame) dispatch(fetchDetails(selectedGame.id));
  }, [selectedGame, dispatch]);

  useEffect(() => {
    dispatch(refreshGames(GameState.New, true));
    dispatch(refreshGames(GameState.Started));
  }, [dispatch]);

  return (
    <div className="game-page">
      <div className="container details-container">
        {isNewGame ? (
          <NewGame />
        ) : selectedGame?.state === GameState.New ? (
          <GameDetails gameId={selectedGame.id} />
        ) : selectedGame?.state === GameState.Started ? (
          <CurrentGame gameId={selectedGame.id} />
        ) : (
          <Loader />
        )}
      </div>
    </div>
  );
};

export default GamePage;
