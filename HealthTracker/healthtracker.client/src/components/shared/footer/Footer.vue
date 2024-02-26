<template>
  <footer>
    <div class="social">
      <SocialLinks />
      <h1>HealthTracker</h1>
    </div>
  </footer>
</template>
<script setup lang="ts">
import SocialLinks from './components/SocialLinks.vue'
</script>
<style lang="scss">
footer {
  min-height: 4rem;
  padding-left: 4rem;
  bottom: 0;
  right: 0;
  display: flex;
  align-items: center;
  justify-content: center;
}

.social {
  margin: auto;
  display: flex;
  align-items: center;
  justify-content: space-evenly;

  @media (max-width: 550px) {
    gap: 1rem;
    flex-direction: column;
  }
}</style>