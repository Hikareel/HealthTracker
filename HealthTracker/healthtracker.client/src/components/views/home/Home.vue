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
import { HomeCardData } from '@/data/models/homeCardModel';
</script>

<style lang="scss" scoped>
.home-page {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 100%;
  width: 100%;
  .main-view {
    display: flex;
    flex-wrap: wrap;
    text-align: center;
    justify-content: center;
    align-items: center;
    margin-top: 1rem;
    @media (max-width: 600px) {
        flex-direction: column;
    }
    .segment {
      margin: 1rem;
      &:hover {
        background-color: transparent;
        text-decoration: none;
      }
      .home-main-card{
        &:click {
          cursor: progress;
        }
        &:hover {
          cursor: pointer;
        }
      }
    }
  }
}
</style>