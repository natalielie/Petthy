import React, { Component } from "react";
import { Route, Switch } from "react-router";
import { Layout } from "./components/Layout";
import { Home } from "./components/Home";
import { NotFound } from "./components/NotFound";

import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import ApiAuthorizationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import { ApplicationPaths } from './components/api-authorization/ApiAuthorizationConstants';

import "./custom.css";
import Professionals from "./components/views/Professionals/Professionals";
import Pets from "./components/views/Clients/Pets";
import Assignments from "./components/views/Assignments/Assignments";
import Appointments from "./components/views/Appointments/Appointments";
import Schedule from "./components/views/Schedule/Schedules";

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Switch>
                    <Route exact path="/" component={Home} />
                    <Route path="/professionals" component={Professionals} />
                    <Route path="/pets" component={Pets} />
                    <Route path="/assignments" component={Assignments} />
                    <Route path="/appointments" component={Appointments} />
                    <Route path="/schedule" component={Schedule} />
                    <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
                </Switch>
            </Layout>
        );
    }
}
