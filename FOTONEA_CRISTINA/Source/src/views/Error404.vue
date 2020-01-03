<template>
<v-layout
    class="backerror"
    align-space-around
    justify-start
    column
    fill-height
    :style="{'background-image':'url('+require('../assets/error.png')+')'}"
  >  </v-layout>
</template>


<script>
export default {};
</script>


<style>
.backerror {
  height: 100%;
  /* background-image: url(https://data.1freewallpapers.com/download/artwork-cats.jpg);  */
  background-size: cover;
}
</style>