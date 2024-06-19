<template>
  <ButtonElement @click="loginWithGoogle" button-label="Log in with Google" align="center" full size="lg"
    :columns="{ container: 12, label: 0, wrapper: 12 }" />
</template>

<script setup>
const isLogging = defineModel();

function loginWithGoogle() {
  isLogging.value = true;
  window.location.href = "https://localhost:7170/login-google";
}
</script>
