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
    left: 4rem;
    right: 0;
    position: fixed;
    height: 4rem;
    max-height: 4rem;
    line-height: 4rem;
    max-width: calc(100vw-4rem);
    align-content: center;
    z-index: 99;
    text-align: center;
    background: rgba(37,32,32,1);
    margin-bottom: 1px;
    color:rgb(225, 225, 225)
  }
</style>