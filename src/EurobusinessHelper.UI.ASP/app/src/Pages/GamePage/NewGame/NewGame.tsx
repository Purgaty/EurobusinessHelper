import React, { useCallback, useState } from "react";
import { BiHelpCircle } from "react-icons/bi";
import { useDispatch, useSelector } from "react-redux";
import { Tooltip } from "react-tooltip";
import { Field, formValueSelector, reduxForm } from "redux-form";
import { createNewGame, refreshGames } from "../actions";
import { setOpenGameMode, setSelectedGame } from "../gameSlice";
import { GameState, NewGameForm } from "../types";
import "./NewGame.scss";
import { newGameFieldNames, newGameFormName } from "./newGameConsts";

interface NewGameProps {
  handleSubmit: Function;
  change: Function;
}

const NewGame = (props: NewGameProps) => {
  const [errorMessage, setErrorMessage] = useState<string>("");

  const hasPassword = useSelector((state) =>
    formValueSelector(newGameFormName)(state, newGameFieldNames.hasPassword)
  );
  const dispatch = useDispatch();

  const onSubmit = useCallback(
    async (values: NewGameForm): Promise<void> => {
      try {
        const newGameId = await createNewGame(values);
        await dispatch(refreshGames(GameState.New));
        dispatch(setSelectedGame(newGameId));
        dispatch(setOpenGameMode(GameState.New));
      } catch (error: any) {
        switch (error.response.status) {
          case 500:
            showErrorMessage("All fields need values");
            break;
          case 400:
            showErrorMessage("Password not provided");
            break;
        }
      }
    },
    [dispatch]
  );

  const showErrorMessage = useCallback(
    (message: string) => {
      setErrorMessage(message);
      setTimeout(() => setErrorMessage(""), 3000);
    },
    [setErrorMessage]
  );

  return (
    <form className="new-game-form" onSubmit={props.handleSubmit(onSubmit)}>
      <p className="new-game-title">New Game</p>
      <div className="new-game">
        <p className="game-name-title">Game Name</p>
        <Field
          name={newGameFieldNames.gameName}
          component="input"
          type="text"
          className="input new-game-input"
          placeholder="Game Name"
          autoComplete="off"
        />
        <p className="starting-balance">Starting Account Balance</p>
        <Field
          name={newGameFieldNames.startingBalance}
          component="input"
          type="number"
          className="input new-game-input"
          placeholder="Starting Account Balance"
          autoComplete="off"
        />
        <Tooltip
          anchorId="help-icon"
          className="tooltip"
          positionStrategy="fixed"
        />
        <p className="minimum-accounts">
          Min Approvals{" "}
          <BiHelpCircle
            id="help-icon"
            data-tooltip-content="Number of players who have to approve a bank transfer"
            data-tooltip-place="right"
            data-tooltip-delay-show={500}
          />
        </p>
        <Field
          name={newGameFieldNames.minTransferRequestApprovals}
          component="input"
          type="number"
          className="input new-game-input"
          placeholder="Min Approvals"
          autoComplete="off"
        />
        <div className="password-switch">
          <p className="password-title">Password</p>
          <label className="switch">
            <Field
              name={newGameFieldNames.hasPassword}
              component="input"
              type="checkbox"
            />
            <span className="slider round"></span>
          </label>
        </div>
        <Field
          style={{ opacity: hasPassword ? 1 : 0 }}
          name={newGameFieldNames.password}
          component="input"
          type="password"
          className="input new-game-input"
          placeholder="Password"
          autoComplete="off"
        />
        <Tooltip
          anchorId="submit-button"
          className="tooltip"
          isOpen={errorMessage === "" ? false : true}
        />
        <button
          type="submit"
          className="button button-hover game-button"
          id="submit-button"
          data-tooltip-content={errorMessage}
          data-tooltip-place="top"
          data-tooltip-variant="error"
        >
          Add New Game
        </button>
      </div>
    </form>
  );
};

export default reduxForm({
  form: newGameFormName,
})(NewGame);
