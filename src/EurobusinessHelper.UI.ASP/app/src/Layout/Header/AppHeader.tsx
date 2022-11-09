import { FaDice } from "react-icons/fa";
import "./AppHeader.scss";

const AppHeader = () => {
  return (
    <div className="application-header">
      <div className="header">
        <FaDice className="header-icon" />
        <p className="title">Eurobusiness helper</p>
      </div>
    </div>
  );
};

export default AppHeader;
