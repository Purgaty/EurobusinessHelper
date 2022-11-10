import React from "react";
import { Route, Routes } from "react-router";
import LoginPage from "../LoginPage/LoginPage";
import GamePage from "./GamePage/GamePage";

export const AppRoutes = () => {
  return (
    <Routes>
      <Route path="/" element={<p>HOME</p>} />
      <Route
        path="/users"
        element={
          <div>
            <p>USERS</p>
          </div>
        }
      />
      <Route path="/games" element={<GamePage />} />
      <Route path="/login" element={<LoginPage />} />
    </Routes>
  );
};
