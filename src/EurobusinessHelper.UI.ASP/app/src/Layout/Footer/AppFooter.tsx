import React, { useEffect, useState } from "react";
import { useLocation } from "react-router";
import { useAppDispatch, useAppSelector } from "../../app/hooks";
import { refreshGames } from "../../Pages/GamePage/actions";
import { GameState } from "../../Pages/GamePage/types";
import MainHub from "../../Services/Hubs/MainHub";
import { fetchCurrentIdentity } from "./actions";
import "./AppFooter.scss";
import { selectIdentity } from "./authSlice";

export const AppFooter = () => {
  const dispatch = useAppDispatch();
  const identity = useAppSelector(selectIdentity);
  const [, setHub] = useState<MainHub | undefined>(undefined);
  const location = useLocation();

  useEffect(() => {
    if (!identity && location?.pathname !== "/login") {
      dispatch(fetchCurrentIdentity());
    }
  }, [dispatch, identity, location]);

  useEffect(() => {
    const hub = new MainHub(async (state: GameState) => {
      await dispatch(refreshGames(state));
    });
    hub.initializeConnection().then(() => setHub(hub));
  }, [dispatch]);

  return (
    <div className="application-footer">
      <p className="application-footer-name">{identity?.name}</p>
      <p className="application-footer-email">{identity?.email}</p>
    </div>
  );
};
