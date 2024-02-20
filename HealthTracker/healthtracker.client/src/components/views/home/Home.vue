<template>
  <main class="home-page">
    <Header title="Home" />
    <div class="main-view">
      <HomeMainField @click="redirectorTo('Diary')" class="home-main-field"/>
      <HomeMainField @click="redirectorTo('Planner')" class="home-main-field"/>
      <HomeMainField @click="redirectorTo('Health')" class="home-main-field"/>
      <HomeMainField @click="redirectorTo('Goals')" class="home-main-field"/>
      <HomeMainField @click="redirectorTo('Community')" class="home-main-field"/>
      <HomeMainField @click="redirectorTo('About')" class="home-main-field"/>
    </div>

  </main>
</template>
<script lang="ts" setup>
import Header from '../../shared/header/Header.vue'
import HomeMainField from './components/HomeMainField.vue'
import { useRouter } from 'vue-router';

const router = useRouter();

function redirectorTo(url: string){
  router.push({ name: url });
}

</script>
<style scope>
.home-main-field {
  margin: 4%;
}
.home-main-field:hover{
  cursor:pointer;
}
.home-main-field:click{
  cursor:progress;
}

.main-view {
  display: grid;
  text-align: center;
  grid-template-columns: repeat(2, 1fr);
}

@media (max-width: 600px) {
  .main-view {
    grid-template-columns: 1fr;
  }
}
</style>