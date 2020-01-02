<template>
  <v-navigation-drawer permanent class="align">
    <h2 class="titlePage">Sick Leave</h2>
    <v-divider></v-divider>
    <v-list v-for="item in route" :key="item.name" class="alignBtn">
      <v-list-tile>
        <v-list-tile-content>
          <v-btn class="button" elevation="7" color="#bb0000" v-on:click="routeTo(item.path)">
            <v-list-tile-title class="title-menu">{{ item.name }}</v-list-tile-title>
          </v-btn>
        </v-list-tile-content>
      </v-list-tile>
    </v-list>
  </v-navigation-drawer>
</template>


<script>
/* eslint-disable */
import { router } from "../router";
export default {
  data() {
    return {
      route: [
        //vector de obiecte
        {
          name: "Register",
          path: "/Register"
        },
        {
          name: "Login",
          path: "/Login"
        },
        {
          name: "Sick Leave Request",
          path: "/SickLeaveRequest"
        }
      ]
    };
  },
  methods: {
    routeTo(routename) {
      console.log("Menu");
      router.push(routename);
    }
  }
};
</script>

<style>
.title-menu {
  /* margin-left: 50px; */
  color: antiquewhite;
}
.align {
  width: 180px !important;
  /* align-content: center!important; */
  align-items: center !important;
}
.titlePage{
  width: 100%;
  text-align: center;
  color:brown;
  /* font-family: "Impact"; */
  font-size: 3rem;
}
.button {
  width: 100%;
  /* font-size: 1rem; */
  margin-left: 5px;
}
.alignBtn {
  margin-top: 10px;
}
</style>