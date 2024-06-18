<template>
  <button @click="loginWithGoogle">Log in with Google</button>
</template>

<script>
export default {
  methods: {
    loginWithGoogle() {
      window.location.href = "https://localhost:7170/login-google";
    }
  }
}
</script>
