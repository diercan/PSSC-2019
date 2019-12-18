import React from "react";
// nodejs library that concatenates classes
import classNames from "classnames";
// react components for routing our app without refresh
// @material-ui/core components
import withStyles from "@material-ui/core/styles/withStyles";
// @material-ui/icons
// core components
import Header from "components/Header/Header.jsx";
import Footer from "components/Footer/Footer.jsx";
import GridContainer from "components/Grid/GridContainer.jsx";
import GridItem from "components/Grid/GridItem.jsx";
import Parallax from "components/Parallax/Parallax.jsx";
import Dialog from "@material-ui/core/Dialog";
import DialogTitle from "@material-ui/core/DialogTitle";
import TextField from '@material-ui/core/TextField';
import Button from "components/CustomButtons/Button.jsx";
import DialogContent from "@material-ui/core/DialogContent";
// sections for this page
import SectionTable from "./Sections/SectionTable.jsx";
//for requests
import axios from 'axios';

import componentsStyle from "assets/jss/material-kit-react/views/components.jsx";
axios.defaults.withCredentials = true;
class Components extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      user: "",
      userInfo: {},
      classicModal: false
    }
  }
  componentWillMount = () => {
    axios.get(process.env.REACT_APP_URL + `/ts/user/me`, {
      headers: {
        "Access-Control-Allow-Origin": "*",
        "authorization": JSON.stringify({
          userName: localStorage.getItem("userName"),
          password: localStorage.getItem("pass")
        })
      },
      withCredentials: true
    })
      .then(list => {
        this.setState({
          user: list.data[0].userName,
          userInfo: list.data[0]
        })
      })
      .catch(err => {
        this.setState({
          classicModal: true
        })
      })
  }

  handleClose = () => {
    this.setState({
      classicModal: false
    })
  }

  sendCredentialsForTraining = (pass) => {
    let Base64 = {
      // private property
      _keyStr: "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=",

      // public method for encoding
      encode: function (input) {
        let output = "";
        let chr1, chr2, chr3, enc1, enc2, enc3, enc4;
        let i = 0;
        input = Base64._utf8_encode(input);

        while (i < input.length) {
          chr1 = input.charCodeAt(i++);
          chr2 = input.charCodeAt(i++);
          chr3 = input.charCodeAt(i++);
          enc1 = chr1 >> 2;
          enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
          enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
          enc4 = chr3 & 63;

          if (isNaN(chr2)) {
            enc3 = enc4 = 64;
          } else if (isNaN(chr3)) {
            enc4 = 64;
          }

          output = output +
            this._keyStr.charAt(enc1) + this._keyStr.charAt(enc2) +
            this._keyStr.charAt(enc3) + this._keyStr.charAt(enc4);
        }
        return output;
      },

      // public method for decoding
      decode: function (input) {
        let output = "";
        let chr1, chr2, chr3;
        let enc1, enc2, enc3, enc4;
        let i = 0;
        input = input.replace(/[^A-Za-z0-9\+\/\=]/g, "");

        while (i < input.length) {
          enc1 = this._keyStr.indexOf(input.charAt(i++));
          enc2 = this._keyStr.indexOf(input.charAt(i++));
          enc3 = this._keyStr.indexOf(input.charAt(i++));
          enc4 = this._keyStr.indexOf(input.charAt(i++));
          chr1 = (enc1 << 2) | (enc2 >> 4);
          chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
          chr3 = ((enc3 & 3) << 6) | enc4;
          output = output + String.fromCharCode(chr1);

          if (enc3 != 64) {
            output = output + String.fromCharCode(chr2);
          }
          if (enc4 != 64) {
            output = output + String.fromCharCode(chr3);
          }
        }
        output = Base64._utf8_decode(output);
        return output;
      },

      // private method for UTF-8 encoding
      _utf8_encode: function (string) {
        string = string.replace(/\r\n/g, "\n");
        let utftext = "";

        for (let n = 0; n < string.length; n++) {
          let c = string.charCodeAt(n);
          if (c < 128) {
            utftext += String.fromCharCode(c);
          }
          else if ((c > 127) && (c < 2048)) {
            utftext += String.fromCharCode((c >> 6) | 192);
            utftext += String.fromCharCode((c & 63) | 128);
          }
          else {
            utftext += String.fromCharCode((c >> 12) | 224);
            utftext += String.fromCharCode(((c >> 6) & 63) | 128);
            utftext += String.fromCharCode((c & 63) | 128);
          }
        }
        return utftext;
      },

      // private method for UTF-8 decoding
      _utf8_decode: function (utftext) {
        let string = "";
        let i = 0;
        let c = 0;
        let c1 = 0;
        let c2 = 0;
        let c3 = 0;

        while (i < utftext.length) {
          c = utftext.charCodeAt(i);

          if (c < 128) {
            string += String.fromCharCode(c);
            i++;
          }
          else if ((c > 191) && (c < 224)) {
            c2 = utftext.charCodeAt(i + 1);
            string += String.fromCharCode(((c & 31) << 6) | (c2 & 63));
            i += 2;
          }
          else {
            c2 = utftext.charCodeAt(i + 1);
            c3 = utftext.charCodeAt(i + 2);
            string += String.fromCharCode(((c & 15) << 12) | ((c2 & 63) << 6) | (c3 & 63));
            i += 3;
          }
        }
        return string;
      }
    }
    return Base64.encode(pass)
  }

  handleSubmitCredentials = () => {
    localStorage.setItem("userName", document.getElementById("usernameField").value)
    localStorage.setItem("pass", this.sendCredentialsForTraining(document.getElementById("passwordField").value))
    window.location.reload()
  }

  render() {

    const { classes, ...rest } = this.props;
    return (
      <div>
        <Dialog classes={{
          root: classes.center,
          paper: classes.modal
        }}
          open={this.state.classicModal}
          keepMounted
          onClose={() => this.handleClose()}
          aria-labelledby="classic-modal-slide-title"
          aria-describedby="classic-modal-slide-description">
          <DialogTitle>
            Please provide credentials!
              </DialogTitle>
          <DialogContent>
            <TextField
              id="usernameField"
              placeholder="Username"
              helperText="Insert username here!"
              fullWidth
              margin="normal"
              InputLabelProps={{
                shrink: false,
              }}
            />
            <br></br>
            <TextField
              id="passwordField"
              placeholder="Password"
              helperText="Insert password here!"
              fullWidth
              type="password"
              margin="normal"
              InputLabelProps={{
                shrink: false,
              }}
            />
            <br></br>
            <Button color="info" style={{ width: "95%" }} onClick={this.handleSubmitCredentials}>Submit</Button>
          </DialogContent>
        </Dialog>
        <Header
          brand={"Welcome, " + this.state.user + "!"}
          fixed
          color="transparent"
          changeColorOnScroll={{
            height: 100,
            color: "white"
          }}
          {...rest}
        />
        <Parallax image={require("assets/img/bg5.jpg")}>
          <div className={classes.container}>
            <GridContainer>
              <GridItem>
                <div className={classes.brand}>
                  <h1 className={classes.title}>Training Planner</h1>
                </div>
              </GridItem>
            </GridContainer>
          </div>
        </Parallax>
        <div className={classNames(classes.main, classes.mainRaised)}>
          <SectionTable userInfo={this.state.userInfo} />
        </div>
        <Footer />
      </div>
    );
  }
}

export default withStyles(componentsStyle)(Components);
