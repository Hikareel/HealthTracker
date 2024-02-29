<template>
  <main>
    <div class="friend-item">
      <p>{{ item.firstname }} {{ item.secondname }}</p>
    </div>
  </main>
</template>

<script lang="ts" setup>
import type { FriendsModel } from './data/friendModel'

defineProps<{
  item: FriendsModel
}>()
</script>

<style lang="scss" scoped>
.friend-item{
  border-bottom: 1px solid black;
  border-radius: 10px 0 0 0;
  padding: 0.5rem;
  font-size: 16px;
  color: white;
  &:hover{
    cursor: pointer;
    background-color: rgba(100, 100, 100, 0.2);
  }
}
</style>