<script setup lang="ts">
import Footer from './components/shared/footer/Footer.vue'
import Sidebar from './components/shared/sidebar/Sidebar.vue'
import Header from './components/shared/header/Header.vue'
</script>

<template>
  <div class="main">
    <Sidebar />
    <div class="content">
      <Header title="HealthTracker" />
      <RouterView v-slot="{Component}" >
        <Transition name="fade" mode="out-in">
          <component :is="Component" class="view"/>
        </Transition>
      </RouterView>
      <Footer />
    </div>
  </div>
</template>

<style scoped>
  .main{
    overflow-x: hidden;
    background: rgb(37,32,32);
    background: linear-gradient(135deg, rgba(37,32,32,1) 0%, rgba(62,50,50,1) 50%, rgba(126,99,99,1) 100%); 
    display: flex;
    .content{
      margin: auto;
      display: flex;
      flex-direction: column;
      .view{
        width: calc(100vw - 4rem);
        margin-left: 4rem;
        padding-top: 4rem;
        min-height: calc(100vh - 20px - 4rem);
      }
    }
  }
  .content{
    right:0;
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
