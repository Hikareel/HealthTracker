<template>
  <main class="home-page">
    <Header title="Home" />
    <div class="main-view">
      <HomeMainField class="home-main-field"/>
      <HomeMainField class="home-main-field"/>
      <HomeMainField class="home-main-field"/>
      <HomeMainField class="home-main-field"/>
      <HomeMainField class="home-main-field"/>
      <HomeMainField class="home-main-field"/>
    </div>

  </main>
</template>
<script lang="ts" setup>
import Header from '../../shared/header/Header.vue'
import HomeMainField from './components/HomeMainField.vue'
</script>
<style scope>
.home-main-field {
  margin: 4%;
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