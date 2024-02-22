<template>
  <main class="home-page">
    <div class="main-view">
        <RouterLink v-for="(link, index) in Object.values(Links).slice(1)"
                    :key="index" :to="`${link.link}`" class="segment">
          <HomeMainField class="home-main-field"/>
        </RouterLink>
    </div>

  </main>
</template>
<script lang="ts" setup>
import HomeMainField from './components/HomeMainField.vue'
import { Links } from '../../shared/sidebar/data/sidebarLinks'
</script>
<style scope>
.segment{
  margin: 1rem;
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
  display: flex;
  flex-wrap: wrap;
  text-align: center;
  justify-content: center;
  align-items: center;
}

@media (max-width: 600px) {
  .main-view {
    flex-direction: column;
  }
}
</style>