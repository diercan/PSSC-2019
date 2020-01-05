import React from "react";
import { Switch, Route, Redirect } from "react-router-dom";
import "perfect-scrollbar/css/perfect-scrollbar.css";
// @material-ui/core components
import { makeStyles } from "@material-ui/core/styles";
// core components
import GridContainer from "components/Grid/GridContainer.js";
import GridItem from "components/Grid/GridItem.js";
import routes from "routes.js";

import styles from "assets/jss/material-dashboard-react/layouts/adminStyle.js";



let ps;




const useStyles = makeStyles(styles);

export default function AuthLayout({ ...rest }) {


    function getRoutes(routes) {
        return routes.map((prop, key) => {
            if (prop.layout === "/auth") {
                return (
                    <Route
                        path={prop.layout + prop.path}
                        component={prop.component}
                        key={key}
                    />
                );
            } else {
                return null;
            }
        });
    };
    // styles
    const classes = useStyles();
    // ref to help us initialize PerfectScrollbar on windows devices
    const mainPanel = React.createRef();
    // states and functions

    return (
        <div>
            <Switch>{getRoutes(routes)}</Switch>
        </div>
    );
}
