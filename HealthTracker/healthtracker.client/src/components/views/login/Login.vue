<template>
  <main class="login-page">
    <LoginForm />
  </main>
</template>
<script lang="ts" setup>
import LoginForm from './components/LoginForm.vue'
</script>
<style>
.login-page {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 100%;
  width: 100%;
}
</style>