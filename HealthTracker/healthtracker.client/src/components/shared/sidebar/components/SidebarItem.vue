<template>
  <div class="sidebar_item" v-if="!item.isHidden">
    <p>
      <router-link class="item" :to="`${item.link}`">
        <span class="material-icons">{{ item.icon }}</span>
        <span class="text">{{ item.name }}</span>
      </router-link>
    </p>
  </div>
</template>

<script lang="ts" setup>
import type { ILinkItem } from '@/data/models/sidebarLinks';
defineProps<{
  item: ILinkItem
}>()
</script>

<style lang="scss" scoped>
</style>