<template>
  <div class="chat-content">
    <div class="notification">
      <p>Notification</p>
    </div>
    <div class="chat-messinput">
      <div class="chat-messages">
        <div v-for="message in CurrentMessages" :key="message.message"
          :class="['message', message.isYours ? 'own-message' : 'received-message']">
          {{ message.message }}
        </div>
      </div>
      <div class="chat-input">
        <button><i class='bi bi-send-fill'></i></button>
        <input type="text" placeholder="Write message..." />
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { CurrentMessages } from './data/currentMessages'
</script>

<style lang="scss" scoped>
</style>