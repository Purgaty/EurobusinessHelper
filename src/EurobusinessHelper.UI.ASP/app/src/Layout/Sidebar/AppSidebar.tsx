import { AiFillHome, AiOutlineMenuUnfold } from "react-icons/ai";
import { FaDice } from "react-icons/fa";
import { RiLoginBoxFill } from "react-icons/ri";
import { Link } from "react-router-dom";
import "./AppSidebar.scss";

const AppSidebar = () => {
  return (
    <div className="sidebar">
      <div className="menu-buttons">
        <AiOutlineMenuUnfold className="menu-open" />
      </div>
      <div className="menu-links">
        <div className="link">
          <AiFillHome className="link-icon" />
          <Link className="link-text" to="/">
            Home
          </Link>
        </div>
        <div className="link">
          <FaDice className="link-icon" />
          <Link className="link-text" to="/">
            Games
          </Link>
        </div>
        <div className="link">
          <RiLoginBoxFill className="link-icon" />
          <Link className="link-text" to="/login">
            Login
          </Link>
        </div>
      </div>
    </div>
  );
};

export default AppSidebar;
