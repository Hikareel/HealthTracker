<template>
  <main class="home-page">
    <div class="main-view">
      <RouterLink v-for="card in HomeCardData" :key="card.name" :to="card.link" class="segment">
        <HomeCard class="home-main-card" :item="card" />
      </RouterLink>
    </div>
  </main>
</template>

<script lang="ts" setup>
import HomeCard from './components/HomeCard.vue';
import { HomeCardData } from './data/homeCardModel';
</script>


<style scope>
.segment {
  margin: 1rem;
}

.home-main-card:hover {
  cursor: pointer;
}

.home-main-card:click {
  cursor: progress;
}

.segment:hover {
  background-color: transparent;
  text-decoration: none;
}

.main-view {
  display: flex;
  flex-wrap: wrap;
  text-align: center;
  justify-content: center;
  align-items: center;
  margin-top: 1rem;
}

@media (max-width: 600px) {
  .main-view {
    flex-direction: column;
  }
}

.home-page {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 100%;
  width: 100%;
}
</style>