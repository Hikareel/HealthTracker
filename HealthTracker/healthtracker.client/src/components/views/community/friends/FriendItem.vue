<template>
  <main>
    <div class="friend-item">
      <p><i class="bi bi-person-circle"></i>{{ item.firstName }} {{ item.lastName }}</p>
    </div>
  </main>
</template>

<script lang="ts" setup>
import type { FriendsModel } from '@/data/models/friendModel'

defineProps<{
  item: FriendsModel
}>()
</script>

<style lang="scss" scoped>
.friend-item{
  justify-content: center;
  align-content: center;
  border-radius: 10px 0 0 10px;
  padding: 0.5rem;
  font-size: 16px;
  color: white;
  &:hover{
    cursor: pointer;
    background-color: rgba(100, 100, 100, 0.2);
  }
  i {
    margin-right: 0.5rem;
  }
}
</style>