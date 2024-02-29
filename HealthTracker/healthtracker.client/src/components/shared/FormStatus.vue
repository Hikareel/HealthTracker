<template>
  <div>
    <p class="form-label">{{ formTitle }}</p>
    <p v-for="error in formStatus.errors" class="error" v-bind:key="error">
      {{ error }}
    </p>
    <p class="success">
      {{ formStatus.success }}
    </p>
  </div>
</template>

<script setup lang="ts">
import { formStatus, clearFormStatus } from '@/data/service/sendDataService'
clearFormStatus()
defineProps<{
  formTitle: string
}>()
</script>

<style lang="scss" scoped>
@use '@/assets/form.scss';
</style>