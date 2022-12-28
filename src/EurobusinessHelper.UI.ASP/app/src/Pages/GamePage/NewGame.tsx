import React, { useState } from "react";
import "./NewGame.scss";

export const NewGame = () => {
  const [password, setPassword] = useState<boolean>(false);

  return (
    <div className="new-game">
      <p className="new-game-title">New Game</p>
      <input
        type="text"
        className="input new-game-input"
        placeholder="Game Name"
      />
      <div className="password-switch">
        <p className="password-title">Password</p>
        <label className="switch">
          <input type="checkbox" onChange={() => setPassword(!password)} />
          <span className="slider round"></span>
        </label>
      </div>
      <input
        style={{ opacity: password ? 1 : 0 }}
        type="text"
        className="input new-game-input"
        placeholder="Password"
      />
      <div className="buttons">
        <div className="button button-hover game-button">Add New Game</div>
        <div className="button button-hover game-button">Cancel</div>
      </div>
    </div>
  );
};
