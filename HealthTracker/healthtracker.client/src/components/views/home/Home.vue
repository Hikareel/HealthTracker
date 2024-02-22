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
  /* padding-top: 5rem; */
  display: flex;
  flex-wrap: wrap;
  margin-left: calc(2rem + 32px);
  text-align: center;
  justify-content: center;
}

@media (max-width: 600px) {
  .main-view {
    grid-template-columns: 1fr;
  }
}
</style>