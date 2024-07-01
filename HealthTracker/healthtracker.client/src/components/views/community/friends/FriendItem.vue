<template>
  <main>
    <div class="friend-item">
      <div class="notification">
        <i v-if="friend.newMessagesCount > 0" class="bi bi-circle-fill"></i>
      </div>
      <div class="avatar">
        <i class="bi bi-person-circle"></i>
      </div>
      <div class="person-info">
        <p>{{ friend.firstName }} {{ friend.lastName }}</p>
      </div>
    </div>
  </main>
</template>

<script lang="ts" setup>
import type { FriendModel } from '@/data/models/friendModel'

defineProps<{
  friend: FriendModel
}>()

</script>

<style lang="scss" scoped>
.friend-item{
  display: grid;
  grid-template-columns: repeat(2, 1fr) 10fr;
  grid-column-gap: 0.5rem;
  justify-content: center;
  align-content: center;
  border-radius: 10px 0 0 10px;
  padding: 0.5rem;
  font-size: 16px;
  color: white;
  padding-left: 1rem;

  .notification{
    font-size: 30%;
    align-content: center;
    i {
      color: yellow;
      border-radius: 100%;
      box-shadow: 0px 0px 12px 2px rgba(255, 255, 0, 1);
    }
  }
  .avatar{
    align-content: center;
    font-size: 100%;
  }
  .person-info{
    display: inline-flex;
  }
  &:hover{
    cursor: pointer;
    background-color: rgba(100, 100, 100, 0.2);
  }
}
</style>