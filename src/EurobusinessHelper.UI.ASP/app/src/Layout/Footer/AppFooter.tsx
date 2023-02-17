import React, { useEffect } from "react";
import { useAppDispatch, useAppSelector } from "../../app/hooks";
import { fetchCurrentIdentity } from "./actions";
import {selectIdentity} from "./authSlice";
import {useLocation} from "react-router";

export const AppFooter = () => {
  const dispatch = useAppDispatch();
  const identity = useAppSelector(selectIdentity);
  const location = useLocation();

  useEffect(() => {
    if (!identity && location?.pathname !== "/login") {
      dispatch(fetchCurrentIdentity());
    }
  }, [dispatch, identity, location]);

  return <div className="application-footer">{identity?.name}</div>;
};
