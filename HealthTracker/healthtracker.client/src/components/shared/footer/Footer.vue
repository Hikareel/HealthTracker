<template>
  <footer>
    <div class="social">
      <SocialLinks/>
      <h1>HealthTracker</h1>
    </div>
  </footer>
</template>
<script setup lang="ts">
  import SocialLinks from './components/SocialLinks.vue'
</script>
<style lang="scss">
  footer{
    height: 96px;
    margin-top: 20px;
    display: flex;
    justify-content: space-between;
    bottom: 0;
  }
  .social{
    margin: auto;
    display: flex;
    align-items: center;
    justify-content: space-evenly;
  }
</style>