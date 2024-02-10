<script setup lang="ts">
import Header from './components/shared/headerBar/Header.vue'
import Footer from './components/shared/footer/Footer.vue'
import Sidebar from './components/shared/sidebar/Sidebar.vue'
</script>

<template>
  <Header />
  <div class="main">
    <Sidebar/>
    <div class="content">
      <RouterView/>
    </div>
  </div>
  <Footer />
</template>

<style scoped>
  .main{
    display: flex;
  }
  .content{
    margin: auto;
    display: flex;
    flex-direction: column;
    place-items: center;
  }
</style>
