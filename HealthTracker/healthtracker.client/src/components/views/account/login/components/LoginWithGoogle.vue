<template>
  <ButtonElement @click="loginWithGoogle" name="submit" button-label="Log in with Google" align="center" :submits="true"
    full size="lg" :columns="{ container: 12, label: 0, wrapper: 12 }" />
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
