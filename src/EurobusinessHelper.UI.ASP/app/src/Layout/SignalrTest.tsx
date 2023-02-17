import React, { useEffect } from "react";
import GameHub from "../Services/Hubs/GameHub";

//To be removed after creating the messages logic in game
export const SignalrTest = ({ accountId }: { accountId: string }) => {
  useEffect(() => {
    const hub = new GameHub(
      (accountTo, amount) =>
        alert(`New bank transfer. Account ${accountTo}, amount ${amount}`),
      (accountFrom, accountTo, amount) =>
        alert(
          `New account transfer. From ${accountFrom}, to ${accountTo}, amount ${amount}`
        ),
      (accountId, amount, requestId) =>
        alert(
          `New bank transfer approval request. Account ${accountId}, amount ${amount}, requestId ${requestId}`
        )
    );
    hub.initializeConnection(accountId);
  }, [accountId]);

  return <div style={{ paddingLeft: "100px" }}>HEHE</div>;
};
