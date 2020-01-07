/*!

=========================================================
* Material Dashboard React - v1.8.0
=========================================================

* Product Page: https://www.creative-tim.com/product/material-dashboard-react
* Copyright 2019 Creative Tim (https://www.creative-tim.com)
* Licensed under MIT (https://github.com/creativetimofficial/material-dashboard-react/blob/master/LICENSE.md)

* Coded by Creative Tim

=========================================================

* The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

*/
// @material-ui/icons
import Dashboard from "@material-ui/icons/Dashboard";
import Calendar from "@material-ui/icons/EventAvailable"
import Person from "@material-ui/icons/Person";
import LibraryBooks from "@material-ui/icons/LibraryBooks";
import BubbleChart from "@material-ui/icons/BubbleChart";
import LocationOn from "@material-ui/icons/LocationOn";
import Notifications from "@material-ui/icons/Notifications";
import Unarchive from "@material-ui/icons/Unarchive";
import Language from "@material-ui/icons/Language";
// core components/views for Admin layout
import UserProfile from "views/UserProfile/UserProfile.js";
import TableList from "views/TableList/TableList.js";
import Typography from "views/Typography/Typography.js";
import Icons from "views/Icons/Icons.js";
import Maps from "views/Maps/Maps.js";
import NotificationsPage from "views/Notifications/Notifications.js";
import UpgradeToPro from "views/UpgradeToPro/UpgradeToPro.js";
// core components/views for RTL layout
import RTLPage from "views/RTLPage/RTLPage.js";
import RegisterPage from "views/RegisterPage";
import LoginPage from "views/LoginPage";
import CalendarPage from "views/Calendar/CalendarPage";
import PollList from "views/PollList/PollList";
import InsertEvents from "views/InsertEvents/InsertEvents";


const dashboardRoutes = [
  {
    path: "/login",
    name: "Login",
    icon: Dashboard,
    component: LoginPage,
    layout: "/auth"
  },
  {
    path: "/register",
    name: "Register",
    icon: Dashboard,
    component: RegisterPage,
    layout: "/auth"
  },

  {
    path: "/calendar",
    name: "Calendar",
    icon: Calendar,
    component: CalendarPage,
    layout: "/dashboard"
  },  
  {
    path: "/addEvent",
    name: "Add Event",
    icon: Dashboard,
    component: InsertEvents,
    layout: "/dashboard"
  },
 
];

export default dashboardRoutes;
