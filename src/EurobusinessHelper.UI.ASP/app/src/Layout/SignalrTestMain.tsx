import React, { useEffect } from "react";
import MainHub from "../Services/Hubs/MainHub";
import {GameState} from "../Pages/GamePage/types";

//To be removed after creating the messages logic in game
export const SignalrTestMain = () => {
  useEffect(() => {
    const hub = new MainHub(
      (state: GameState) =>
        alert(`Game list ${state} has changes`)
    );
    hub.initializeConnection();
  }, []);

  return <div style={{ paddingLeft: "100px" }}>HEHE</div>;
};
