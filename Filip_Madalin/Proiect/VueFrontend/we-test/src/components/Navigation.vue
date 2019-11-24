<template>
    <div>
         <v-toolbar dark
        src="https://cdn.vuetifyjs.com/images/backgrounds/vbanner.jpg"
        >
      <v-toolbar-title>WE-TEST</v-toolbar-title>
      <v-spacer ></v-spacer>
      <v-toolbar-items>
            <v-btn text>
                <router-link to="/" class="nav-link" active-class="active" exact style="cursor: pointer" >Home</router-link>
            </v-btn>
            <v-btn text>
                <router-link to="/tests" class="nav-link" active-class="active" exact style="cursor: pointer">Tests</router-link>
            </v-btn>
             <v-btn text>
                <router-link to="/login" class="nav-link" active-class="active" exact style="cursor: pointer">Login</router-link>
            </v-btn>
      </v-toolbar-items>
     
    </v-toolbar>



        
    </div>
</template>
<style>
.nav-link {
 color: #FFF;
 outline: none;
 text-decoration: none;
}
</style>