import React, { useCallback, useEffect } from "react";
import { BiSearch } from "react-icons/bi";
import { IoMdAddCircleOutline } from "react-icons/io";
import { useSelector } from "react-redux";
import { useAppDispatch } from "../../app/hooks";
import { fetchDetails, refreshGames } from "../../Pages/GamePage/actions";
import {
  selectGameList,
  selectSelectedGame,
  selectShowGame,
  setOpenGameMode,
  setShowGames,
} from "../../Pages/GamePage/gameSlice";
import { GameState } from "../../Pages/GamePage/types";
import { selectIsIdentityLoaded } from "../Footer/authSlice";
import "./AppSidebar.scss";
import GameList from "./GameList";
import GameSearch from "./GameSearch";

const AppSidebar = () => {
  const dispatch = useAppDispatch();
  const gameList = useSelector(selectGameList(GameState.New));
  const startedGamesList = useSelector(selectGameList(GameState.Started));
  const selectedGame = useSelector(selectSelectedGame);
  const showGames = useSelector(selectShowGame);
  const isIdentityLoaded = useSelector(selectIsIdentityLoaded);

  const checkGameList = useCallback(() => {
    switch (showGames) {
      case GameState.New:
        return gameList;
      case GameState.Started:
        return startedGamesList;
      default:
        return gameList;
    }
  }, [gameList, startedGamesList, showGames]);

  useEffect(() => {
    if (selectedGame) {
      dispatch(fetchDetails(selectedGame));
    }
  }, [selectedGame, dispatch, checkGameList]);

  useEffect(() => {
    if (!isIdentityLoaded) return;
    dispatch(refreshGames(GameState.Started));
    dispatch(refreshGames(GameState.New, true));
  }, [dispatch, isIdentityLoaded]);

  if (isIdentityLoaded)
    return (
      <div className="sidebar">
        <BiSearch className="search-icon" />
        <div className="menu-open">
          <GameSearch gameState={showGames} />
          <div className="games-switch">
            <p
              className={"games-switch-title " + (showGames === GameState.New && "current-list")}
              onClick={() => {
                dispatch(setShowGames(GameState.New));
              }}
            >
              New
            </p>
            <p
              className={
                "games-switch-title " + (showGames === GameState.Started && "current-list")
              }
              onClick={() => {
                dispatch(setShowGames(GameState.Started));
              }}
            >
              Started
            </p>
          </div>
          <GameList list={checkGameList()} gameMode={showGames} />
          <div
            className="button button-hover add-game-button"
            onClick={() => dispatch(setOpenGameMode(GameState.NotCreated))}
          >
            <p className="add-text">Add Game</p>
            <IoMdAddCircleOutline className="add-icon" />
          </div>
        </div>
      </div>
    );
  else return <></>;
};

export default AppSidebar;
