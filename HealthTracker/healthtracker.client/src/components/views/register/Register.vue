<template>
  <main class="register-page">
    <RegisterForm/>
  </main>
</template>
<script lang="ts" setup>
import RegisterForm from './components/RegisterForm.vue'
</script>
<style>
.register-page {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 100%;
  width: 100%;
}
</style>