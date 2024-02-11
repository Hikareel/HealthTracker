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
    margin: 0 0 20px 0;
    padding: 1rem;
    text-align: center;
  }
</style>