import React, { useState } from "react";
import { BiSearch } from "react-icons/bi";
import { useAppDispatch } from "../../app/hooks";
import { fetchGames } from "../../Pages/GamePage/actions";
import { GameState } from "../../Pages/GamePage/types";
import "./GameSearch.scss";

export interface GameSearchProps {
  gameState: GameState;
}

const GameSearch = ({ gameState }: GameSearchProps) => {
  const dispatch = useAppDispatch();
  const [search, setSearch] = useState<string>("");

  const handleInput = (event: any) => {
    setSearch(event.target.value);
  };
  const handleKeyDown = (event: any) => {
    if (event.key === "Enter") {
      handleInput(event);
      dispatch(fetchGames(gameState, search));
    }
  };

  return (
    <div className="search-bar">
      <input
        type="text"
        className="input search-input"
        placeholder="Search..."
        value={search}
        onChange={handleInput}
        onKeyDown={handleKeyDown}
      />
      <div className="button search-button" onClick={() => dispatch(fetchGames(gameState, search))}>
        <BiSearch />
      </div>
    </div>
  );
};

export default GameSearch;
