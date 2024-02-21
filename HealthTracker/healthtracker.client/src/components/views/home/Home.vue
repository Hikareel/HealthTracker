<template>
  <main class="home-page">
    <Header title="Home" />
    <div class="main-view">
        <RouterLink v-for="(link, index) in Object.values(Links).slice(1)"
                    :key="index" :to="`${link.link}`" class="segment">
          <HomeMainField class="home-main-field"/>
        </RouterLink>
    </div>

  </main>
</template>
<script lang="ts" setup>
import Header from '../../shared/header/Header.vue'
import HomeMainField from './components/HomeMainField.vue'
import { Links } from '../../shared/sidebar/data/sidebarLinks'
//const objectToArray = (obj: Object) => Object.values(obj)
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
.segment:hover{
  background-color: transparent;
  text-decoration: none;
}

.main-view {
  padding-top: 4rem;
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