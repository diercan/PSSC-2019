<template>
  <v-app>
    <v-content transition="slide-x-transition">
      <v-layout align-space-around justify-space-between row fill-height>
        <Menu></Menu>
        <keep-alive>
          <router-view></router-view>
        </keep-alive>
      </v-layout>
    </v-content>
  </v-app>
</template>

<script>
import Menu from "./components/Menu.vue";
/* eslint-disable */
export default {
  name: "App",
  components: {
    Menu
  },
  created() {
    localStorage.setItem("isLogged","false");
    console.log("Pana aici");
    this.$vuetify.theme.themes.light.primary = "#CE2727";
  }
};
</script>
<style>
@import url("https://fonts.googleapis.com/css?family=Roboto:100,100i,300,300i,400,400i,500&display=swap");
</style>
