
<template>
  <v-layout align-space-between justify-space-between column fill-height>
    <div class="back" :style="{'background-image':'url('+require('../assets/medicalBack.jpg')+')'}">
      <v-layout align-center justify-space-between row fill-height>
        <v-card :height="530" :width="400" class="loginCard" flat>
          <div>
            <h2>Login</h2>
          </div>
      </v-card>
  </v-layout>
    </div>
     </v-layout>
</template>

<script>
/* eslint-disable*/
import axios from "axios";
import { router } from "../router";
export default {
  data() {
    return {
     
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
  margin-top: auto;
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