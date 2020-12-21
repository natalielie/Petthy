import React, { Component, Fragment } from 'react';
import { NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import authService from './AuthorizeService';
import { ApplicationPaths } from './ApiAuthorizationConstants';

export class LoginMenu extends Component {
    constructor(props) {
        super(props);

        this.state = {
            isAuthenticated: false,
            userName: null
        };
    }

    componentDidMount() {
        this._subscription = authService.subscribe(() => this.populateState());
        this.populateState();
    }

    componentWillUnmount() {
        authService.unsubscribe(this._subscription);
    }

    async populateState() {
        const [isAuthenticated, user] = await Promise.all([authService.isAuthenticated(), authService.getUser()])
        this.setState({
            isAuthenticated,
            userName: user && user.name
        });
    }

    render() {
        const { isAuthenticated, userName } = this.state;
        if (!isAuthenticated) {
            const registerPath = `${ApplicationPaths.Register}`;
            const loginPath = `${ApplicationPaths.Login}`;
            return this.anonymousView(registerPath, loginPath);
        } else {
            const profilePath = `${ApplicationPaths.Profile}`;
            const logoutPath = { pathname: `${ApplicationPaths.LogOut}`, state: { local: true } };
            return this.authenticatedView(userName, profilePath, logoutPath);
        }
    }

    authenticatedView(userName, profilePath, logoutPath) {
        if (userName != "admin@gmail.com") {
            return (<Fragment>
                <NavItem>
                    <NavLink tag={Link} className="text-dark" to="/pets">Pets</NavLink>
                </NavItem>
                <NavItem>
                    <NavLink tag={Link} className="text-dark" to="/assignments">My Assignments</NavLink>
                </NavItem>
                <NavItem>
                    <NavLink tag={Link} className="text-dark" to="/appointments">My Appointments</NavLink>
                </NavItem>
                <NavItem>
                    <NavLink tag={Link} className="text-dark" to="/schedule">My Schedule</NavLink>
                </NavItem>
                <NavItem>
                    <NavLink tag={Link} className="text-dark" to={profilePath}>Hello {userName}</NavLink>
                </NavItem>
                <NavItem>
                    <NavLink tag={Link} className="text-dark" to={logoutPath}>Logout</NavLink>
                </NavItem>
            </Fragment>);
        }
        else {
            return (<Fragment>
                <NavItem>
                    <NavLink tag={Link} className="text-dark" to="/professionals">Professionals</NavLink>
                </NavItem>
                <NavItem>
                    <NavLink tag={Link} className="text-dark" to="/pets">Pets</NavLink>
                </NavItem>
                <NavItem>
                    <NavLink tag={Link} className="text-dark" to="/assignments">Assignments</NavLink>
                </NavItem>
                <NavItem>
                    <NavLink tag={Link} className="text-dark" to="/appointments">Appointments</NavLink>
                </NavItem>
                <NavItem>
                    <NavLink tag={Link} className="text-dark" to={profilePath}>Hello {userName}</NavLink>
                </NavItem>
                <NavItem>
                    <NavLink tag={Link} className="text-dark" to={logoutPath}>Logout</NavLink>
                </NavItem>
            </Fragment>);
        }

    }

    anonymousView(registerPath, loginPath) {
        return (<Fragment>
            <NavItem>
                <NavLink tag={Link} className="text-dark" to={registerPath}>Register</NavLink>
            </NavItem>
            <NavItem>
                <NavLink tag={Link} className="text-dark" to={loginPath}>Login</NavLink>
            </NavItem>
        </Fragment>);
    }
}
