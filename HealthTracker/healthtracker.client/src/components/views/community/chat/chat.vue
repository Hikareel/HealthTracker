<template>
  <main class="chat">
    <div class="chat-header">
      <p>Friend name: </p>
    </div>
    <div class="chat-messages">
      <div v-for="message in CurrentMessages" :key="message.message"
        :class="['message', message.isYours ? 'own-message' : 'received-message']">
        {{ message.message }}
      </div>
    </div>

    <div class="chat-input">
      <input type="text" placeholder="Write message..." />
      <button><i class='bi bi-send-fill'></i></button>
    </div>
  </main>
</template>

<script lang="ts" setup>
import { CurrentMessages } from './data/currentMessages'
</script>

<style lang="scss">
.chat {
  display: grid;
  grid-template-columns: 1fr;
  grid-template-rows: 1fr 6fr 2fr;
  background-color: red;
  border-radius: 15px 0px 0px 15px;
}

.chat-header {
  display: inline-grid;
  color: white;
  text-align: center;
  align-items: end;
}

.chat-messages {
  overflow-y: auto;
  scrollbar-width: thin;
  scrollbar-color: #888 #f0f0f0;
  height: 8rem;
  margin: 1rem 1rem 0rem 1rem;
  padding: 5px;
  background-color: grey;
}

.message {
  display: flex;
  overflow-wrap: anywhere;
  margin-bottom: 10px;
  padding: 5px;
  border-radius: 10px;
  max-width: 60%;
  width: fit-content;
  color: black;
}

.own-message {
  justify-content: end;
  margin-left: auto;
  background-color: #dcf8c6;
}

.received-message {
  justify-content: start;
  background-color: #fff;
}


.chat-input {
  display: flex;
  margin-left: 1rem;
  margin-bottom: 0.5rem;
  margin-right: 1rem;
  gap: 2px;
}

.chat-input input {
  flex-grow: 1;
  height: 2rem;
}

.chat-input button {
  cursor: pointer;
  background-color: white;
  height: 2rem;
  width: 2rem;
}

.chat-messages::-webkit-scrollbar {
  width: 8px;
}

.chat-messages::-webkit-scrollbar-track {
  background: #f0f0f0;
}

.chat-messages::-webkit-scrollbar-thumb {
  background: #888;
}

.chat-messages::-webkit-scrollbar-thumb:hover {
  background: #555;
}
</style>
