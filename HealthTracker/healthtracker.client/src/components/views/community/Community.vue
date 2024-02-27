<template>
  <main class="community-page">
    <div class="wall">
      <!-- POSTs -->
    </div>
    <div class="right-content">
      <FriendsList class="friends-list" />
      <Chat class="chat" />
    </div>

  </main>
</template>
<script lang="ts" setup>
import FriendsList from './friends/FriendsList.vue'
import Chat from './chat/chat.vue'

</script>
<style>
.community-page {
  display: grid;
  grid-template-columns: 3fr 1fr;
  justify-items: stretch;
  align-items: stretch;
}

.wall {
  background-color: chocolate;
}

.right-content {
  right: 0;
}

.friends-list {
  display: flex;
  top: 0;

}

.chat {
  display: flex;
  bottom: 0;
}
</style>