<template>
  <header>
    <h1 class="app_name">
      {{ title }}
    </h1>
  </header>
</template>
<script lang="ts">
export default {
  props:{
    title: String
  }
}
</script>
<style>
  header{
    top: 0;
    left: 0;
    right: 0;
    position: fixed;
    padding: 1rem;
    z-index: 99;
    text-align: center;
    background: #121414;
  }
</style>