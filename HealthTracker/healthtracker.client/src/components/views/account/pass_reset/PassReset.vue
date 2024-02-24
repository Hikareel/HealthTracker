<template>
  <main class="pass-reset-page">
    <PassResetForm />
  </main>
</template>
<script lang="ts" setup>
import PassResetForm from './components/PassResetForm.vue'
</script>
<style lang="scss" scoped>
.pass-reset-page {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 100%;
  width: 100%;
}
</style>