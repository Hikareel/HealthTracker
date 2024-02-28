<template>
  <main class="community-page">
    <div class="wall">
      <!-- POSTs -->
      <p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p><p>test</p>
    </div>
    <div class="right-content">
      <FriendsList />
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
  justify-content: center;
  align-items: center;
}

.right-content {
  position: fixed;
  display: flex;
  flex-direction: column;
  gap: 2rem;
  height: 100vh;
  width: 25vw;
  right:0;
}

.friends-list {
}

.chat {
}
</style>