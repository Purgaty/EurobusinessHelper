import { useEffect, useState } from "react";
import { useAppSelector } from "../app/hooks";
import NotificationsHub from "../Services/Hubs/NotificationsHub";
import { selectIdentity } from "./Footer/authSlice";

//To be removed after creating the messages logic in game
export const SignalrTest = () => {
    const identity = useAppSelector(selectIdentity);
    const [hub, setHub] = useState<NotificationsHub | null>(null);

    useEffect(() => {
        if(!identity)
            return;
        const hub = new NotificationsHub();
        hub.initializeConnection(identity.id)
        .then(() => setHub(hub));
    }, [identity, setHub]);

    return <div style={{ paddingLeft: "100px" }}>HEHE<button onClick={() => hub?.requestTransfer(200)}>SEND</button></div>
}
