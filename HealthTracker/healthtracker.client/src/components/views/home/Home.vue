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
  height: 100%;

  .main-view {
    position: relative;
    display: flex;
    flex-direction: column;
    width: 100%;
    height: calc(100vh - 8rem);

    .segment {
      flex: 1;
      transition: flex 5.0s ease;
      height: 100%;

      &:hover {
        cursor: pointer;
        transition: flex 0.8s ease;
        flex: 7;

        ::v-deep .content {
          transition: font-size 0.8s ease;
          opacity: 1;
          font-size: medium;
        }
      }

      &:click {
        cursor: progress;
      }
    }
  }
}
</style>