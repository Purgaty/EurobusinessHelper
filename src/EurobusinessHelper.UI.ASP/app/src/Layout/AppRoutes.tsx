import React from "react";
import { Route, Routes } from "react-router";
import GamePage from "../Pages/GamePage/GamePage";
import LoginPage from "../Pages/LoginPage/LoginPage";

export const AppRoutes = () => {
  return (
    <Routes>
      <Route path="/" element={<GamePage />} />
      <Route path="/login" element={<LoginPage />} />
    </Routes>
  );
};
