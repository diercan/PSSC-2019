<!-- MVVM => View -->
<template>
  <v-layout align-space-between justify-space-between column fill-height>
    <div class="back" :style="{'background-image':'url('+require('../assets/medical.png')+')'}">
   <v-layout align-center justify-space-between row fill-height>
        <v-card :height="400" :width="450" class="employeeDetails" flat>
            <h1>Details</h1>
        <h2> Name: <b>{{details_name}} </b></h2>
        <h2> Mail: <b> {{details_mail}}</b></h2>
        <h2> Personnel ID: <b>{{details_ID}}</b></h2>
        <h2> Company name: <b>{{details_company}}</b></h2>
        <h2> Sick days : <b>{{details_days}}</b></h2>
         <div class="btnAlignRow">
          <v-btn class="btnSignOut" rounded color="primary" v-on:click="UserSignOut()">Sign out</v-btn>
          <v-btn class="btnDeleteUser" rounded color="primary" v-on:click="UserDelete()">Delete User</v-btn>
        </div>
      </v-card>
      
  </v-layout>
</template>


<script>
/* Model*/
import { router } from "../router";
import axios from "axios";
export default {
  data() {
    return {
       dialogDetailsEvent: false,
         detailsFlag: true,
          deleteText: "",
         dialogDeleteEvent: false,
       details_name: "",
       details_mail: "",
       details_ID: "",
       details_company: "",
       details_days: "",
       details: [],
       popUpUser: "",
    };
  },
  methods:{
     UserSignOut() {
      localStorage.setItem("userData_name", "");
      localStorage.setItem("userData_mail", "");
      localStorage.setItem("userData_personnelID", "");
      localStorage.setItem("usersdb_company", "");
      localStorage.setItem("isLogged", "false");
      router.push("/Login");
    },
    UserDelete() {
       this.details_ID= localStorage.getItem("userData_personnelID");
        console.log("delete");
      axios
        .post("/api/deleteUser", {
           personnelID:this.details_ID
        })
        .then(response => {
            
           localStorage.setItem("userData_name", "");
      localStorage.setItem("userData_mail", "");
       localStorage.setItem("userData_username", "");
      localStorage.setItem("userData_password", "");
      localStorage.setItem("userData_personnelID", "");
      localStorage.setItem("usersdb_company", "");
          localStorage.setItem("isLogged", "false");
          router.push("/Login");
        });
    },
    DetailsUser(){
        this.details_ID= localStorage.getItem("userData_personnelID");
      axios
      .post("/api/usersdb/details2",{
        personnelID:this.details_ID
      })
      .then(response=>{
          
          console.log(response.data);
          console.log(response.data.usersdb_name);
      
           this.details_name= response.data.usersdb_name;
           this.details_mail= response.data.usersdb_mail;
           this.details_company=response.data.usersdb_company;
      });

    },
    DetailsDays(){
        this.details_ID = localStorage.getItem("userData_personnelID");
        console.log(this.details_ID)
      axios
      .post("/api/requestdb/requestdays",{
        requestdb_personnelID:this.details_ID
      })
      .then(response=>{
          
      console.log( response.data);
           this.details_days= response.data;
      });

  }
  },
  mounted(){
     this.DetailsUser();
     this.DetailsDays();
  }
};
</script>


<style>
.back {
  height: 100%;
  /* background-image: url(https://data.1freewallpapers.com/download/artwork-cats.jpg);  */
  background-size: cover;
}
.employeeDetails{
  margin-bottom: 200px;
}
.employeeDetails h1{
font-size: 2rem;
  font-weight: 400;
  font-family: "Roboto", sans-serif;
  font-style: italic;
  text-align: center;
    /* margin-top: 30px; */
     margin-left: 200px;
}
.employeeDetails h2{
font-size: 1.2rem;
  font-weight: 300;
  font-family: "Roboto", sans-serif;
  font-style: italic;
  margin-left: 70px;
  margin-top: 20px;
}
.btnAlignRow {
  display: flex;
  /* align-items: center; */
  /* justify-content: center; */
}
.btnSignOut {
  margin-left:100px;
  align-self: center;
  width: 130px;
  margin-top: 40px;
}
.btnDeleteUser {
  margin-top: 40px;
  margin-left:150px;
}
</style>