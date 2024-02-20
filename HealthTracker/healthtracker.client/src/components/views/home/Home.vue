<template>
  <main class="home-page">
    <Header title="Home" />
    <div class="main-view">
      <div v-for="(link, index) in objectToArray(Links).slice(1)" :key="index">
        <RouterLink :to="`${link.link}`">
          <HomeMainField class="home-main-field"/>
        </RouterLink>
      </div>
    </div>

  </main>
</template>
<script lang="ts" setup>
import Header from '../../shared/header/Header.vue'
import HomeMainField from './components/HomeMainField.vue'
import { Links } from '../../shared/sidebar/data/sidebarLinks'
const objectToArray = (obj: Object) => Object.values(obj)
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
  display: flex;
  flex-wrap: wrap;
  margin: auto;
  text-align: center;
  justify-content: center;
}

@media (max-width: 600px) {
  .main-view {
    grid-template-columns: 1fr;
  }
}
</style>