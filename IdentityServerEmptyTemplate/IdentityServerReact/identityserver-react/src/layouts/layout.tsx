import React from "react";
import { Link, BrowserRouter as Router } from "react-router-dom";
import Routing from "../routing/routing";

const Layout = () => {
  return (
    <Router>
      <nav>
        <ul>
          <li>
            <Link to="/">Dashboard</Link>
          </li>
          <li>
            <Link to="/callback">Callback</Link>
          </li>
          <li>
            <Link to="/about">About</Link>
          </li>
        </ul>
      </nav>
      <Routing />
    </Router>
  );
};

export default Layout;
