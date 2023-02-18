import React, { useEffect } from "react";
import GameHub from "../Services/Hubs/GameHub";

//To be removed after creating the messages logic in game
export const SignalrTest = ({ accountId }: { accountId: string }) => {
  useEffect(() => {
    const hub = new GameHub(
      () =>
        alert(`Game has changes`),
      (accountFrom, accountTo, amount) =>
        alert(
          `New account transfer. From ${accountFrom}, to ${accountTo}, amount ${amount}`
        )
    );
    hub.initializeConnection(accountId);
  }, [accountId]);

  return <div style={{ paddingLeft: "100px" }}>HEHE</div>;
};
