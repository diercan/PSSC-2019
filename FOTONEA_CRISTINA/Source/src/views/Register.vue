<!-- MVVM => View -->
<template>
  <v-layout align-space-between justify-space-between column fill-height>
    <div class="back" :style="{'background-image':'url('+require('../assets/medical.png')+')'}">
      <v-layout align-center justify-space-between row fill-height>
        <v-card :height="530" :width="400" class="registerCard" flat>
          <div>
            <h2>Register</h2>
          </div>
          <v-text-field
            :rules="nameRules"
            v-model="reg_name"
            class="reg justify-center"
            label="Name"
          ></v-text-field>
          <v-text-field
            v-model="reg_mail"
            :rules="mailRules"
            class="reg justify-center"
            label="E-mail"
          ></v-text-field>
          <v-text-field
            v-model="reg_user"
            :rules="userRules"
            class="reg justify-center"
            label="Enter Username"
          ></v-text-field>
          <v-text-field
            v-model="reg_pass"
            :rules="passRules"
            class="reg justify-center"
            label="Enter Password"
             :type="hiddenRegister ? 'text' : 'password'"
          ></v-text-field>
          <v-text-field
            v-model="reg_persID"
            :rules="persRules"
            class="reg justify-center"
            label="Personnel ID"
          ></v-text-field>
           <v-text-field
            v-model="reg_company"
            :rules="companyRules"
            class="reg justify-center"
            label="Company"
          ></v-text-field>
          <v-btn class="btnRegister" rounded color="primary" v-on:click="register()">Register</v-btn>
        </v-card>
        <div class="register">
          <h2>Welcome!</h2>
          <h3>Don't have an account? Create your account! It takes less than a minute.</h3>
        </div>
      </v-layout>
    </div>
    <v-dialog v-model="dialog" :max-width="350">
      <v-card class="popUp" :height="200">
        <h3>{{popUpText}}</h3>
        <v-btn class="btnOk" rounded color="primary" v-on:click="registerOk()">Ok</v-btn>
      </v-card>
    </v-dialog>
  </v-layout>
</template>

<script>
/* Model*/
/*eslint-disable*/
import axios from "axios";

export default {
  data() {
    return {
      reg_name: "",
      reg_mail: "",
      reg_user: "",
      reg_pass: "",
      reg_persID: "",
      reg_company: "",
      hiddenRegister: false,
      dialog: false,
      popUpText: "",
      reg_name: [
        v => !!v || "Name is required",
        v => v.length >= 3 || "Name must be at least 3 characters"
      ],
      reg_mail: [
        v => !!v || "E-mail is required",
        v => /.+@.+/.test(v) || "E-mail must be valid"
      ],
      reg_user: [
        v => !!v || "Username is required",
        v => v.length >= 8 || "Username must be at least 8 characters"
      ],
      reg_pass: [
        v => !!v || "Password is required",
        v => v.length >= 8 || "Password must be at least 8 characters"
      ],
      reg_persID: [
        v => !!v || "Personnel ID is required",
        v => v.length >= 9 || "Personnel ID must be 9 digits",
        v => v.length < 10 || "Personnel ID must be 9 digits"
      ],
       reg_company: [
        v => !!v || "Company is required",
        v => v.length >= 3 || "Company must be at least 3 characters"
      ]
    };
  },
  mounted() {
    (this.reg_name = ""),
      (this.reg_mail = ""),
      (this.reg_user = ""),
      (this.reg_pass = ""),
      (this.reg_persID = ""),
      (this.reg_company = "");
  },
  watch: {
    $route(to, from) {
      (this.reg_name = ""),
        (this.reg_mail = ""),
        (this.reg_user = ""),
        (this.reg_pass = ""),
        (this.reg_persID = ""),
        (this.reg_company = "");
    }
  },
  methods: {
    register() {
      if (
        this.reg_name.length < 4 ||
        this.reg_user.length < 8 ||
        this.reg_pass.length < 8 ||
        this.reg_persID.length != 9 ||
        this.reg_company.length < 4
      ) {
        this.dialog = !this.dialog;
        this.popUpText = "Invalid data!";
      } else {
        axios
          .post("/api/usersdb/register2", {
            name: this.reg_name,
            mail: this.reg_mail,
            username: this.reg_user,
            password: this.reg_pass,
            personnelID: this.reg_persID,
            company: this.reg_company
          })
          .then(
            response => {
              console.log(response);

              this.popUpText = "Registration complete!";
            },
            error => {
              console.log(error);

              this.popUpText = "Server error!";
            }
          );
        // .catch(function(error) {
        //   console.log(error);

        //   this.popUpText="Server error!";
        // });
        this.dialog = !this.dialog;
      }
    },
    registerOk() {
      this.dialog = false;
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
.register {
  color: white;
  margin-right: 40px;
  font-size: 1rem;
  text-align: center;
  /* background-color: aqua; */
  width: 300px;
  height: 200px;
  text-shadow: 1px 5px 12px #000000;
}
.register h2 {
  font-size: 2rem;
  margin-bottom: 30px;
  text-shadow: 1px 5px 12px #000000;
  font-family: "Roboto", sans-serif;
  font-style: italic;
}
.registerCard {
  margin-left: 70px;
  /* margin-top: auto; */
  /* margin-bottom: auto; */
}
.registerCard h2 {
  font-family: "Roboto", sans-serif;
  font-style: italic;
}
.btnRegister {
  margin-left: 250px;
  width: 150px;
}

.popUp h3 {
  font-family: "Roboto", sans-serif;

  text-align: center;
  padding: 60px;
  font-size: 1.4rem;
}
.btnOk {
  margin-left: 100px;
  width: 150px;
}
</style>