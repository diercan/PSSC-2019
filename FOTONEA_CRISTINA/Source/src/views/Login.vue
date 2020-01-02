<!-- MVVM => View -->
<template>
  <v-layout align-space-between justify-space-between column fill-height> 
     <div class="back" :style="{'background-image':'url('+require('../assets/medical.png')+')'}">
      <v-layout align-center justify-space-between row fill-height>
        <v-card :height="400" :width="450" class="loginCard" flat>
          <div>
            <h2>Login</h2>
          </div>
          <v-text-field
            :rules="logUserRules"
            v-model="log_user"
            class="log justify-center"
            label="Enter Username"
          ></v-text-field>
          <v-text-field
            :rules="logPassRules"
            v-model="log_pass"
            class="log justify-center"
            label="Enter Password"
            :type="hidden ? 'text' : 'password'"
          ></v-text-field>
          <v-text-field
            :rules="logIDRules"
            v-model="log_persID"
            class="log justify-center"
            label="Personnel ID"
          ></v-text-field>
          <v-btn class="btnLogin" rounded color="primary" v-on:click="logIn()">Login</v-btn>
        </v-card>
        <div class="login">
          <h2>Hello!</h2>
          <h3>Login to Your Account.</h3>
        </div>
      </v-layout>
    </div>
    <v-dialog v-model="dialogLogIn" :max-width="350">
      <v-card class="popUp" :height="200">
        <h3>{{popUptxt}}</h3>
        <v-btn class="btnLog" rounded color="primary" v-on:click="LogInOk()">Ok</v-btn>
      </v-card>
    </v-dialog>
  </v-layout>
</template>

<script>
/* eslint-disable*/
/* Model*/
import axios from "axios";
import { router } from "../router";
export default {
  data() {
    return {
      log_user: "",
      log_pass: "",
      hidden: false,
      log_persID: "",
      dialogLogIn: false,
      popUptxt: "",
      logUserRules: [
        v => !!v || "Username is required",
        v => v.length >= 4 || "Username must be at least 4 characters"
      ],
      logPassRules: [
        v => !!v || "Password is required",
        v => v.length >= 8 || "Password must be at least 8 characters"
      ],
      logIDRules: [
        v => !!v || "Personnel ID is required",
        v => v.length >= 9 || "Personnel ID must be 9 digits",
        v => v.length < 10 || "Personnel ID must be 9 digits"
      ]
    };
  },
  methods: {
    logIn() { 
      if (
        this.log_user.length < 3 ||
        this.log_pass.length < 8 ||
        this.log_persID.length < 9
      ) {
        this.dialogLogIn = !this.dialogLogIn;
        this.popUptxt = "Invalid data!";
      } else {
        axios
          .post("/api/usersdb/login2", {
            username: this.log_user,
            password: this.log_pass,
            personnelID: this.log_persID
          })
          .then(
            response => {
                console.log("dddddddddddd"+response.data.user.usersdb_name);
              if (typeof response.data === "object") {
                this.popUptxt = "Login successfully!";
                this.log_user = null;
                this.log_pass = null;
                this.log_persID = null;
               console.log(response.data.user.usersdb_username);
               console.log(response);
                localStorage.setItem(
                  "userData_username",
                  response.data.user.usersdb_username
                ); // save user data in browser till the browser is close
                localStorage.setItem("userData_name", response.data.user.usersdb_name);
                localStorage.setItem(
                  "userData_password",
                  response.data.user.usersdb_password
                );
                localStorage.setItem("userData_mail", response.data.user.usersdb_mail);
                localStorage.setItem("userData_personnelID", response.data.user.usersdb_personnelID);
                localStorage.setItem("userData_company", response.data.user.usersdb_company);
                localStorage.setItem("isLogged", "true");
                console.log(localStorage.getItem("userData_name"));
              } else {
                this.popUptxt = " Username or password invalid! ";
              }
            },
            error => {
              console.log(error);

              this.popUptxt = "Server error!";
            }
          );
        this.dialogLogIn = !this.dialogLogIn;
      }
    },
    LogInOk() {
      this.dialogLogIn = false;
      if (this.popUptxt == "Login successfully!") {
        console.log("Login");
        router.push("/EmployeeAccDetails");
      }
    },

    mounted() {
      (this.log_user = ""), (this.log_pass = ""), (this.log_persID = "");
    },
    watch: {
      $route(to, from) {
        (this.log_user = ""), (this.log_pass = ""), (this.log_persID = "");
      }
    }
  }
};
</script>
<style>
.back {
  height: 100%;
  /* background-image: url(https://scontent.ftsr1-1.fna.fbcdn.net/v/t1.15752-9/69380368_483731745757002_388805131400904704_n.png?_nc_cat=111&_nc_oc=AQkZswsosTbTP_Q0IfRxjQCHUxlUzTe4-jBZw8bFKdddQlPscAgTZ9QsxGUVw8Mzmfl4pI5pTYs4e0eGQa0dbmdC&_nc_ht=scontent.ftsr1-1.fna&oh=4e4d5359e9ae02fde7320c0472301c18&oe=5E10210F); */
  background-size: cover;
}
.login {
  color: white;
  margin-right: 30px;
  font-size: 1rem;
  text-align: center;
  /* background-color: aqua; */

  width: 300px;
  height: 200px;
  text-shadow: 1px 5px 12px #000000;
}
.login h2 {
  font-size: 2rem;
  margin-bottom: 30px;
  text-shadow: 1px 5px 12px #000000;
  font-family: "Roboto", sans-serif;
  font-style: italic;
}
.loginCard {
  margin-left: 70px;
  /* margin-top: auto; */
    /* opacity: 0.75; */
  /* margin-bottom: auto; */

}
.loginCard h2 {
  font-family: "Roboto", sans-serif;
  font-style: italic;
}
.btnLogin {
  margin-left: 250px;
  width: 150px;
}
.btnLog {
  margin-left: 100px;
  width: 150px;
}
</style>