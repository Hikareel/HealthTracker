<template>
  <main class="form-page">
    <LoginForm />
    <LoginWithGoogle />
  </main>
</template>

<script lang="ts" setup>
import LoginForm from './components/LoginForm.vue'
import LoginWithGoogle from './components/LoginWithGoogle.vue'
</script>

<style lang="scss" scoped>
@use '@/assets/form';
</style>