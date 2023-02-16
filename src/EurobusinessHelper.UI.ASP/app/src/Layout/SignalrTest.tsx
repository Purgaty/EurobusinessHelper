import { useEffect, useState } from "react";
import { useAppSelector } from "../app/hooks";
import { selectIdentity } from "./Footer/authSlice";
import GameHub from "../Services/Hubs/GameHub";

//To be removed after creating the messages logic in game
export const SignalrTest = () => {
    const identity = useAppSelector(selectIdentity);
    const [hub, setHub] = useState<GameHub | null>(null);

    useEffect(() => {
        if(!identity)
            return;
        const hub = new GameHub((accountTo, amount) =>
                alert(`New bank transfer. Account ${accountTo}, amount ${amount}`),
            (accountFrom, accountTo, amount) =>
                alert(`New account transfer. From ${accountFrom}, to ${accountTo}, amount ${amount}`),
            (accountId, amount, requestId) =>
                alert(`New bank transfer approval request. Account ${accountId}, amount ${amount}, requestId ${requestId}`));
        hub.initializeConnection(identity.id)
        .then(() => setHub(hub));
    }, [identity, setHub]);

    return <div style={{ paddingLeft: "100px" }}>HEHE</div>;
}
