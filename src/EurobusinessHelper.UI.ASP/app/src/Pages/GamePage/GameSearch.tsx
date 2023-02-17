import React, { useState } from "react";
import { BiSearch } from "react-icons/bi";
import { useAppDispatch } from "../../app/hooks";
import { fetchGames } from "./actions";
import "./GameSearch.scss";
import { GameState } from "./types";

export interface GameSearchProps {
  gameState: GameState;
}

const GameSearch = ({ gameState }: GameSearchProps) => {
  const dispatch = useAppDispatch();
  const [search, setSearch] = useState<string>("");

  const handleInput = (event: any) => {
    setSearch(event.target.value);
  };

  return (
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
        onClick={() => dispatch(fetchGames(gameState, search))}
      >
        <BiSearch />
      </div>
    </div>
  );
};

export default GameSearch;
