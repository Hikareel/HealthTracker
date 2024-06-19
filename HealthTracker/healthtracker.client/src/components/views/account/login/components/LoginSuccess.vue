<template>
  <div>Login successful</div>
</template>

<script>
import router from "@/router";
import { updateUser } from "../../../../../data/service/userData";
export default {
  mounted() {
    const urlParams = new URLSearchParams(window.location.search);
    const userDTO = urlParams.get('user');

    if (userDTO) {
      const userData = JSON.parse(decodeURIComponent(userDTO));
      localStorage.setItem('user', JSON.stringify(userData));
      router.push("/").then(() => {
        updateUser()
      });
    }
  }
}
</script>
