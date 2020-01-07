import React from "react";
import { Link } from "react-router-dom"
// @material-ui/core components
import { makeStyles } from "@material-ui/core/styles";
import InputAdornment from "@material-ui/core/InputAdornment";
// @material-ui/icons

import People from "@material-ui/icons/People";
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
import axios from "axios"
import image from "assets/img/bg.jpg";
import UserProfile from "UserProfile.js"
const useStyles = makeStyles(styles);

export default function LoginPage(props) {
  const [cardAnimaton, setCardAnimation] = React.useState("cardHidden");
  setTimeout(function () {
    setCardAnimation("");
  }, 700);
  const classes = useStyles();
  localStorage.clear();
  // const history = useHistory();

  // function handleClick() {
  //   history.push("/auth/register");
  // }
  // const { ...rest } = props;
  function handleSign() {
    const email = document.getElementById("email").value;
    const password = document.getElementById("password").value;
    const payload = {email: email,password:password}
    const url = "https://my-events-pssc.herokuapp.com/api/user/login";

    fetch(url, {
      method: 'post',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload)
    })
    .then(response => response.json())
    .then((response) => {
      UserProfile.setAuth(response);
      console.log("login made");
    })
    .catch(err => console.log(err))

    // axios.post(url, {
    //   "email": email,
    //   "password": password
    // })
    // .then(function (response) {
    //   UserProfile.setAuth(response);
    //   console.log("login made");
    // })
    // .catch(function (error) {
    //   console.log(error);
    //   alert("Login Failed!!");
    // });

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
            <GridItem xs={12} sm={12} md={5}>
              <Card className={classes[cardAnimaton]}>
                <form className={classes.form}>
                  <CardHeader color="info" className={classes.cardHeader}>
                    <h4>Login</h4>
                  </CardHeader>
                  <CardBody>
                    <CustomInput
                      labelText="Email"
                      id="email"
                      formControlProps={{
                        fullWidth: true
                      }}
                      inputProps={{
                        type: "text",
                        endAdornment: (
                          <InputAdornment position="end">
                            <People className={classes.inputIconsColor} />
                          </InputAdornment>
                        )
                      }}
                    />
                    <CustomInput
                      labelText="Password"
                      id="password"
                      formControlProps={{
                        fullWidth: true
                      }}
                      inputProps={{
                        type: "password",
                        endAdornment: (
                          <InputAdornment position="end">
                            <Icon className={classes.inputIconsColor}>
                              lock_outline
                            </Icon>
                          </InputAdornment>
                        ),
                        autoComplete: "off"
                      }}
                    />
                  </CardBody>
                  <CardFooter className={classes.cardFooter}>
                    <Link to="/auth/register">
                      <Button simple color="info">
                        Register
                      </Button>
                    </Link>
                    <Button color="info" onClick={handleSign}>
                      Login
                    </Button>
                  </CardFooter>
                </form>
              </Card>
            </GridItem>
          </GridContainer>
        </div>
      </div>
    </div>
  );
}
