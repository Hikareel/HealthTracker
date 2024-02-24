<template>
  <main class="new-pass-page">
    <NewPassForm />
  </main>
</template>
<script lang="ts" setup>
import NewPassForm from './components/NewPassForm.vue';
</script>
<style lang="scss" scoped>
.new-pass-page {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 100%;
  width: 100%;
}
</style>