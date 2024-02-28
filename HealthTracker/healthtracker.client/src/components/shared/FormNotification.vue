<template>
  <div>
    <p class="form-label">{{ formTitle }}</p>
    <p v-for="error in formNotification.errors" class="error" v-bind:key="error">
      {{ error }}
    </p>
    <p class="success">
      {{ formNotification.success }}
    </p>
  </div>
</template>

<script setup lang="ts">
import { formNotification, clearNotification } from '@/data/service/sendDataService'
clearNotification()
const props = defineProps<{
    formTitle: string
  }>()
</script>

<style lang="scss" scoped>
@use '@/assets/form.scss';
</style>