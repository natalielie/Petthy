import React, { Component, useEffect } from "react";
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
import AddAppointment from "./components/views/Appointments/AddAppointment";
import Schedule from "./components/views/Schedule/Schedules";

import { withTranslation } from "react-i18next";
import { NavMenu } from "./components/NavMenu";
import LanguageSwitcher from "./components/LanguageSwitcher";


const ProfessionalsComponent = withTranslation()(Professionals);

export default class App extends Component {
    static displayName = App.name;

    constructor(props) {
        super(props)
    }


    render() {

        return (
            <Layout>
                <LanguageSwitcher />
                <Route path="/appointments-add/" component={AddAppointment} />
                <Switch>
                    <Route exact path="/" component={Home} />
                    <Route path="/professionals" component={ProfessionalsComponent} />
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

//export { app };


