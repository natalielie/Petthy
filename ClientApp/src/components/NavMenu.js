import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import { LoginMenu } from './api-authorization/LoginMenu';
import './NavMenu.css';

import App from "../App";
import LanguageSwitcher from "./LanguageSwitcher";
import { withTranslation } from "react-i18next";

//const ProfileComponent = withTranslation()(Profile)

class NavMenu extends Component {
    static displayName = NavMenu.name;

    constructor(props) {
        super(props);

        this.toggleNavbar = this.toggleNavbar.bind(this);

        this.state = {
            collapsed: true
        };
    }

    toggleNavbar() {
        this.setState({
            collapsed: !this.state.collapsed
        });
    }

    render() {
        // var home = language["locale"] === "eng" ? language["home_eng"] : language["home_uk"]
        const { t } = this.props;
        return (
            <header>
                <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3">
                <Container>
                <NavbarBrand tag={Link} to="/">Petthy Health System</NavbarBrand>
                <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
                <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
                    <ul className="navbar-nav flex-grow">
                     <NavItem>
                        <NavLink tag={Link} className="text-dark" to="/">{t("Home")}</NavLink>
                                    
                    </NavItem>
                    <LoginMenu>
                                </LoginMenu>
                    </ul>
                </Collapse>
                </Container>
            </Navbar>
            </header>
        );
    }
}

export default withTranslation()(NavMenu);

