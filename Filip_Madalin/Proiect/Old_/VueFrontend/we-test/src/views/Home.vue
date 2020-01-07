<template>
  <div class="home">
    <v-container class="my-8">
      <v-card flat class="pa-3" v-for="test in tests" :key="test.id">
        <v-layout column wrap :class="`pa-3 test ${test.tester}`">
          <v-flex xs6 sm2 md4>
            <div class="caption--grey">Test Title</div>
            <div>{{test.testTitle}}</div>
          </v-flex>
          <v-flex xs6 sm4 md2>
            <div class="caption--grey">Project</div>
            <div>{{test.project}}</div>
          </v-flex>
          <v-flex xs6 sm4 md2>
            <div class="caption--grey">Tester</div>
            <div>{{test.tester}}</div>
          </v-flex>
          <v-flex xs6 sm4 md2>
            <div class="caption--grey">Functionality</div>
            <div>{{test.functionality}}</div>
          </v-flex>
          <v-flex xs6 sm4 md2>
            <div class="caption--grey">Descripiton</div>
            <div>{{test.description}}</div>
          </v-flex>
        </v-layout>
      </v-card>
    </v-container>
  </div>
</template>

<script>
// @ is an alias to /src
// import HelloWorld from '@ /components/HelloWorld.vue';

export default {
  name: "home",
  components: {},
  data() {
    return {
      tests: [
        {
          testId: "proj_xxxx",
          testTitle: "Queued request in bootloader_ try testing thiss....",
          tester: "TheOne",
          functionality: "gateway",
          project: "BMW_Mare_Proiect_Plecam_Acasa",
          description:
            "this magic test does magic things , do not touch , may break the simulation =)))",
          files: "file1,2,3,34,4,4"
        },
        {
          testId: "proj_yyyy",
          testTitle: "Test some frames on the bus",
          tester: "TheTryHarderButNoTalent",
          functionality: "flexray_data_link_layer",
          project: "China_Copiem_BMW_da_sa_mearga_",
          description: "it worked ,needs 3 config files and a bless from god",
          files: "file1,2,3"
        },
        {
          testId: "proj_yyyy",
          testTitle: "Test some frames on the bus",
          tester: "TheTryHarderButNoTalent",
          functionality: "flexray_data_link_layer",
          project: "China_Copiem_BMW_da_sa_mearga_",
          description: "it worked ,needs 3 config files and a bless from god",
          files: "file1,2,3"
        },
        {
          testId: "proj_yyyy",
          testTitle: "Test some frames on the bus",
          tester: "TheTryHarderButNoTalent",
          functionality: "flexray_data_link_layer",
          project: "China_Copiem_BMW_da_sa_mearga_",
          description: "it worked ,needs 3 config files and a bless from god",
          files: "file1,2,3"
        }
      ]
    };
  }
};
</script>
<style>
.caption--grey {
  color: darkblue;
}
</style>