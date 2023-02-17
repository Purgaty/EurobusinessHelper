import React from "react";
import { BrowserRouter } from "react-router-dom";
import "./AppLayout.scss";
import { AppRoutes } from "./AppRoutes";
import { AppFooter } from "./Footer/AppFooter";
import AppHeader from "./Header/AppHeader";
import AppSidebar from "./Sidebar/AppSidebar";

const AppLayout = () => {
  return (
    <div className="application-container">
      <BrowserRouter>
        <AppSidebar />
        <AppHeader />
        <div className="application-body">
          <div className="application-content">
            <AppRoutes />
          </div>
        </div>
        <AppFooter />
      </BrowserRouter>
    </div>
  );
};

export default AppLayout;
