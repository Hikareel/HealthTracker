<script setup lang="ts">
import Footer from './components/shared/footer/Footer.vue'
import Sidebar from './components/shared/sidebar/Sidebar.vue'
import Header from './components/shared/header/Header.vue'
</script>

<template>
  <div class="main">
    <Sidebar/>
    <div class="content">
      <Header title="HealthTracker"/>
      <RouterView v-slot="{Component}" class="view">
        <Transition name="fade" mode="out-in">
          <component :is="Component"/>
        </Transition>
      </RouterView>
      <Footer />
    </div>
  </div>
</template>

<style scoped>
  .main{
    display: flex;
  }
  .content{
    margin: auto;
    width: 100%;
    display: flex;
    flex-direction: column;
    @media (max-width: 768px) {
      margin-left: 4rem;
    }
  }
  .view{
    padding-top: 4rem;
    padding-left: calc(2rem + 32px);
    min-height: calc(100vh - 116px);
  }
  .fade-enter-from,
  .fade-leave-to {
    opacity: 0;
  }
  .fade-enter-active,
  .fade-leave-active{
    transition: opacity 0.3s ease-out;
  }
</style>
