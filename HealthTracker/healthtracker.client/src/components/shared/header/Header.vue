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
    left: calc(2rem + 32px);
    right: 0;
    position: fixed;
    height: 4rem;
    line-height: 4rem;
    align-content: center;
    z-index: 99;
    text-align: center;
    background: #121414;
    margin-bottom: 1px;
  }
</style>