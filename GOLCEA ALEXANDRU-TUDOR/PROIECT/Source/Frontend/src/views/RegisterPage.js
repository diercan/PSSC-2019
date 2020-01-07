import React from "react";
import { Link } from "react-router-dom"
// @material-ui/core components
import { makeStyles } from "@material-ui/core/styles";
import InputAdornment from "@material-ui/core/InputAdornment";
import InputLabel from "@material-ui/core/InputLabel";
import axios from "axios";
// @material-ui/icons

import People from "@material-ui/icons/People";
import Email from "@material-ui/icons/Email";
import Icon from "@material-ui/core/Icon";


// core components
import Header from "components/Header/Header.js";
// import HeaderLinks from "components/Header/HeaderLinks.js";
import GridContainer from "components/Grid/GridContainer.js";
import GridItem from "components/Grid/GridItem.js";
import Button from "components/CustomButtons/Button.js";
import Card from "components/Card/Card.js";
import CardBody from "components/Card/CardBody.js";
import CardHeader from "components/Card/CardHeader.js";
import CardFooter from "components/Card/CardFooter.js";
import CustomInput from "components/CustomInput/CustomInput.js";

import styles from "assets/jss/material-kit-react/views/loginPage.js";
import history from '../history';
import image from "assets/img/bg.jpg";
import UserProfile from "UserProfile.js"
axios.defaults.withCredentials = true;

const useStyles = makeStyles(styles);

export default function RegisterPage(props) {
  const [cardAnimaton, setCardAnimation] = React.useState("cardHidden");
  setTimeout(function () {
    setCardAnimation("");
  }, 700);
  const classes = useStyles();
  // const history = useHistory();

  // function handleClick() {
  //   history.push("/auth/register");
  // }
  // const { ...rest } = props;

  function authenticationHandler() {


    const firstName = document.getElementById("firstName").value;
    const lastName = document.getElementById("lastName").value;
    const email = document.getElementById("email").value;
    const password = document.getElementById("password").value;

    const payload = {firstName: firstName, lastName, email: email,password:password}
    const url = "https://my-events-pssc.herokuapp.com/api/user/register";

    fetch(url, {
      method: 'post',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload)
    })
    .then(response => response.json())
    .then((response) => {
      UserProfile.setAuth(response);
      console.log("register");
    })
    .catch(err => console.log(err))


  }


  return (
    <div>
      <Header
        absolute
        color="transparent"
        brand="My-Events"
      />
      <div
        className={classes.pageHeader}
        style={{
          backgroundImage: "url(" + image + ")",
          backgroundSize: "cover",
          backgroundPosition: "top center"
        }}
      >
        <div className={classes.container}>
          <GridContainer justify="center">
            <GridItem xs={12} sm={12} md={8}>
              <Card>
              <CardHeader color="info" className={classes.cardHeader}>
                    <h4>Register</h4>
                  </CardHeader>
                <CardBody>
                  <GridContainer>
                    <GridItem xs={12} sm={12} md={6}>
                      <CustomInput
                        labelText="First Name"
                        id="firstName"
                        formControlProps={{
                          fullWidth: true
                        }}
                        inputProps={{
                          disabled: false
                        }}
                      />
                    </GridItem>
                    <GridItem xs={12} sm={12} md={6}>
                      <CustomInput
                        labelText="Last Name"
                        id="lastName"
                        formControlProps={{
                          fullWidth: true
                        }}
                      />
                    </GridItem>
                    <GridItem xs={12} sm={12} md={12}>
                      <CustomInput
                        labelText="Email"
                        id="email"
                        formControlProps={{
                          fullWidth: true
                        }}
                      />
                    </GridItem>
                  </GridContainer>
                  <GridContainer>
                    <GridItem xs={12} sm={12} md={12}>
                      <CustomInput
                        labelText="Password"
                        id="password"
                        formControlProps={{
                          fullWidth: true
                        }}
                        inputProps={{
                          type: "password",
                          autoComplete: "off"
                        }}
                      />
                    </GridItem>
                    {/* <GridItem xs={12} sm={12} md={6}>
                      <CustomInput
                        labelText="Last Name"
                        id="last-name"
                        formControlProps={{
                          fullWidth: true
                        }}
                      />
                    </GridItem> */}
                  </GridContainer>
                 
                  {/* <GridContainer>
                    <GridItem xs={12} sm={12} md={12}>
                      <InputLabel style={{ color: "#AAAAAA" }}>About me</InputLabel>
                      <CustomInput
                        labelText="Lamborghini Mercy, Your chick she so thirsty, I'm in that two seat Lambo."
                        id="about-me"
                        formControlProps={{
                          fullWidth: true
                        }}
                        inputProps={{
                          multiline: true,
                          rows: 5
                        }}
                      />
                    </GridItem>
                  </GridContainer> */}
                </CardBody>
                <CardFooter>
                  <GridContainer>
                    <GridItem xs={12} sm={12} md={6}>
                      <Link to="/auth/login">
                        <Button simple color="info">Login</Button>
                      </Link>
                    </GridItem>
                    <GridItem xs={12} sm={12} md={6}>
                      <Button color="info" onClick={authenticationHandler}>Register</Button>
                    </GridItem>
                  </GridContainer>
                </CardFooter>
              </Card>
            </GridItem>
          </GridContainer>
        </div>
      </div>
    </div>
  );
}












