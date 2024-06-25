<template>
  <main class="chat">
    <div class="content" :class="`${is_expanded && 'is-expanded'}`">
      <div class="chat-header">
        <div class="menu-toggle-wrap">
          <button class="menu-toggle" @click="ToggleMenu">
            <span class="material-icons">
              keyboard_double_arrow_right
            </span>
          </button>
        </div>
        <div class="chat-header-label">
          <p v-if="chatStore.friendToChat">{{ chatStore.friendToChat.firstName }} {{ chatStore.friendToChat.lastName }}</p>
        </div>
      </div>
      <div class="chat-content">
        <div class="notification">
          <p>Notification</p>
        </div>
        <ChatBox :connection="connection"/>
      </div>

    </div>
  </main>
</template>

<script lang="ts" setup>
import { ref } from "vue";
import ChatBox from './ChatBox.vue'
import { useChatStore } from '@/store/community/chat'

const chatStore = useChatStore();
const is_expanded = ref(false)
const ToggleMenu = () => {
  is_expanded.value = !is_expanded.value
}

defineProps<{
  connection: any;
}>();

</script>

<style lang="scss">
@use '@/assets/community/chat';
</style>
