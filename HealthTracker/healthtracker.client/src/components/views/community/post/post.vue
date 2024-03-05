<template>
  <main class="post">
    <div class="content">
      <div class="header">
        <p>{{ item.user }}</p>
      </div>
      <div class="main">
        {{ item.markdownText }}
      </div>
      <div class="footer">

      </div>
    </div>
  </main>
</template>

<script lang="ts" setup>
import type { IPostModel } from '@/data/models/postModels';

defineProps<{
  item: IPostModel
}>()

</script>

<style lang="scss" scoped>
</style>