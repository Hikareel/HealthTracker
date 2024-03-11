<template>
  <footer v-if="!isCommunityPage">
    <div class="social">
      <SocialLinks />
      <h1>HealthTracker</h1>
    </div>
  </footer>
</template>

<script setup lang="ts">
import { useRoute } from 'vue-router';
import SocialLinks from './components/SocialLinks.vue';
import { computed } from 'vue';

const route = useRoute();
const isCommunityPage = computed(() => route.path === '/community');
</script>

<style lang="scss" scoped>
footer {
  min-height: 4rem;
  bottom: 0;
  right: 0;
  display: flex;
  align-items: center;
  justify-content: center;

  .social {
    margin: auto;
    display: flex;
    align-items: center;
    justify-content: space-evenly;

    @media (max-width: 550px) {
      gap: 1rem;
      flex-direction: column;
    }
  }
}
</style>