<template>
  <div class="sidebar_item">
    <p>{{ content }}</p>
  </div>
</template>
<script lang="ts">
  export default{
    props:{
      content: {
        type: String,
        required: true
      }
    }
  }
</script>
<style lang="scss" scoped>
  .sidebar_item p{
    color: black;
    font-weight: bold;
    font-size: large;
  }
</style>