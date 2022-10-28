import React, { useEffect } from "react";
import { useAppDispatch, useAppSelector } from "../../app/hooks";
import { fetchCurrentIdentity } from "./actions";
import { selectIdentity } from "./authSlice";

export const AppFooter = () => {
  const dispatch = useAppDispatch();
  const identity = useAppSelector(selectIdentity);

  useEffect(() => {
    if (!identity) {
      dispatch(fetchCurrentIdentity());
    }
  }, [dispatch, identity]);

  return <div className="application-footer">{identity?.name}</div>;
};
