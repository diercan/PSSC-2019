<!-- MVVM => View -->
<template>
  <v-layout align-space-between justify-space-between column fill-height>
    <div class="back" :style="{'background-image':'url('+require('../assets/medical.png')+')'}">
      <v-layout align-center justify-space-between row fill-height>
    <v-card :height="530" :width="400" class="registerCard" flat>
          <div>
            <h2>Sick Leave Request</h2>
          </div>
          <v-text-field
            :rules="nameRules"
            v-model="sick_name"
            class="reg justify-center"
            label="Enter Name"
          ></v-text-field>
          <v-text-field
            v-model="sick_personnelID"
            :rules="idRules"
            class="reg justify-center"
            label="Enter Personnel ID"
          ></v-text-field>
          <v-text-field
            v-model="sick_username"
            :rules="userRules"
            class="reg justify-center"
            label="Enter Username"
          ></v-text-field>
          <v-text-field
            v-model="sick_startDate"
        
            class="reg justify-center"
            label="Enter Begin Date yyyy-mm-dd"
          ></v-text-field>
          <v-text-field
            v-model="sick_endDate"
            
            class="reg justify-center"
            label="Enter End Date yyyy-mm-dd"
          ></v-text-field>
             <v-text-field
            v-model="sick_days"
              :rules="daysRules"
            class="reg justify-center"
            label="Enter Number of Days "
          ></v-text-field>
           <v-text-field
            v-model="sick_code"
            :rules="codeRules"
            class="reg justify-center"
            label="Enter Code"
          ></v-text-field>
          <v-btn class="btnRequest" rounded color="primary" v-on:click="request()">Send Request</v-btn>
        </v-card>
      </v-layout>
    </div>
    <v-dialog v-model="dialog" :max-width="350">
      <v-card class="popUp" :height="200">
        <h3>{{popUpText}}</h3>
        <v-btn class="btnOk" rounded color="primary" v-on:click="requestOk()">Ok</v-btn>
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
      sick_name: "",
      sick_personnelID: "",
      sick_username: "",
      sick_startDate: "",
      sick_endDate: "",
      sick_code: "",
      sick_days: "",
      dialog: false,
      popUpText: "",
      sick_name: [
        v => !!v || "Name is required",
        v => v.length >= 3 || "Name must be at least 3 characters"
      ],
       sick_personnelID: [
        v => !!v || "Personnel ID is required",
        v => v.length >= 9 || "Personnel ID must be 9 digits",
        v => v.length < 10 || "Personnel ID must be 9 digits"
      ],
      sick_username: [
        v => !!v || "Username is required",
        v => v.length >= 8 || "Username must be at least 8 characters"
      ],
      sick_code: [
        v => !!v || "Code is required",
        v => v.length >= 3 || "Code must be at least 3 characters"
      ],
      sick_days: [
        v => !!v || "Number of days is required",
        v => v.length >= 1 || "Number of days must be at least 1 character"
      ]
   
    };
  },
  mounted() {
    (this.sick_name = ""),
      (this.sick_personnelID = ""),
      (this.sick_username = ""),
      (this.sick_startDate = ""),
      (this.sick_endDate = ""),
      (this.sick_days = ""),
      (this.sick_code = "");
  },
  watch: {
    $route(to, from) {
      (this.sick_name = ""),
        (this.sick_personnelID = ""),
        (this.sick_username = ""),
        (this.sick_startDate = ""),
        (this.sick_endDate = ""),
         (this.sick_days = ""),
        (this.sick_code = "");
    }
  },
  methods: {
    request() {
      if (
        this.sick_name.length < 4 ||
          this.sick_personnelID.length != 9 ||
        this.sick_username.length < 8 ||
        this.sick_days.length < 1 ||
        this.sick_code.length < 4
      ) {
        this.dialog = !this.dialog;
        this.popUpText = "Invalid data!";
      } else {
        console.log("api");
        console.log(this.sick_endDate);
         console.log(this.sick_endDate.toString());
        axios
        
          .post("/api/requestdb/request2", {
            name: this.sick_name,
            personnelID: this.sick_personnelID,
            username: this.sick_username,
            startdate: this.sick_startDate,
            enddate: this.sick_endDate,
            days:this.sick_days,
            code: this.sick_code
          })
          .then(
            response => {
              console.log(response);

              this.popUpText = "Request Send!";
            },
            error => {
              console.log(error);

              this.popUpText = "Server error!";
            }
          );
        this.dialog = !this.dialog;
      }
    },
    requestOk() {
      this.dialog = false;
    }
  }
};
</script>


<style>
.back {
  height: 100%;
  /* background-image: url(https://data.1freewallpapers.com/download/artwork-cats.jpg);  */
  background-size: cover;
}
</style>