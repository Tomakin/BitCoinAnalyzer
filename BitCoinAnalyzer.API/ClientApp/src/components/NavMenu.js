import React, { useState } from "react";
import {
  Collapse,
  Navbar,
  NavbarBrand,
  NavbarToggler,
  NavItem,
  NavLink,
} from "reactstrap";
import { Link } from "react-router-dom";
import "./NavMenu.css";
import { authenticationService } from "../services";
import { useNavigate } from "react-router-dom";

function NavMenu() {
  const [collapsed, setCollapsed] = useState(true);
  const navigate = useNavigate();

  const toggleNavbar = () => {
    setCollapsed(!collapsed);
  };

  const handleLogout = () => {
    // navigate("/login");
    authenticationService.logout();
  };

  return (
    <header>
      <Navbar
        className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3"
        container
        light
      >
        <NavbarBrand tag={Link} to="/">
          BitCoinAnalyzer
        </NavbarBrand>
        <NavbarToggler onClick={toggleNavbar} className="mr-2" />
        <Collapse
          className="d-sm-inline-flex flex-sm-row-reverse"
          isOpen={!collapsed}
          navbar
        >
          {!authenticationService.currentUserValue ? (
            <ul className="navbar-nav flex-grow">
              <NavItem>
                <NavLink tag={Link} className="text-dark" to="/">
                  Charts
                </NavLink>
              </NavItem>
              <NavItem>
                <NavLink tag={Link} className="text-dark" to="/login">
                  Login
                </NavLink>
              </NavItem>
              <NavItem>
                <NavLink tag={Link} className="text-dark" to="/register">
                  Register
                </NavLink>
              </NavItem>
            </ul>
          ) : (
            <ul className="navbar-nav flex-grow">
              <NavItem>
                <NavLink tag={Link} className="text-dark" to="/">
                  Charts
                </NavLink>
              </NavItem>
              <NavItem>
              <Link className="text-dark nav-link" to="/login" onClick={handleLogout}>
                  Çıkış Yap
                </Link>
              </NavItem>
            </ul>
          )}
        </Collapse>
      </Navbar>
    </header>
  );
}

export { NavMenu };
