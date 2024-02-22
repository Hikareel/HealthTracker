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
    display: flex;
    padding: 20px;
    bottom: 0;
    @media  (max-width: 550px){
      margin-left: 4rem;
    }
  }
  .social{
    margin: auto;
    display: flex;
    align-items: center;
    justify-content: space-evenly;
    @media (max-width: 550px) {
      gap: 1rem;
      flex-direction: column;
    }
  }
</style>