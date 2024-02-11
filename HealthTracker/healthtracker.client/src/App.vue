<script setup lang="ts">
import Footer from './components/shared/footer/Footer.vue'
import Sidebar from './components/shared/sidebar/Sidebar.vue'
</script>

<template>
  <div class="main">
    <Sidebar/>
    <div class="content">
      <RouterView/>
      <Footer />
    </div>
  </div>
</template>

<style scoped>
  .main{
    display: flex;
  }
  .content{
    margin: 0 auto 0 auto;
    width: 100vw;
    flex-direction: column;
    @media (max-width: 768px) {
      margin-left: 4rem;
    }
  }
</style>
