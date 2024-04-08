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
          <p v-if="friendToChat">{{ friendToChat.firstName }} {{ friendToChat.lastName }}</p>
        </div>
      </div>
      <div class="chat-content">
        <div class="notification">
          <p>Notification</p>
        </div>
        <ChatBox :friendToChat="friendToChat" :current-messages="currentMessages" :connection="connection" />
      </div>

    </div>
  </main>
</template>

<script lang="ts" setup>
import { ref } from "vue";
import ChatBox from './ChatBox.vue'
import type { FriendModel } from "@/data/models/friendModel";


const is_expanded = ref(false)
const ToggleMenu = () => {
  is_expanded.value = !is_expanded.value
}

interface Message {
  id: number,
  text: string
  isYours: boolean;
}

defineProps<{
  currentMessages: Message[];
  connection: any;
  friendToChat: FriendModel | null;
}>();

</script>

<style lang="scss">
@use '@/assets/community/chat';
</style>
