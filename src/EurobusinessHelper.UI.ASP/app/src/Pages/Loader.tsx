import React from "react";
import "./Loader.scss";

export const Loader = () => {
  return (
    <div className="lds-ripple">
      <div></div>
      <div></div>
    </div>
  );
};

export default Loader;
