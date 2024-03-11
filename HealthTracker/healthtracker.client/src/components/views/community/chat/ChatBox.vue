<template>
  <div class="chat-messinput">
    <div class="messages">
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
</template>

<script setup lang="ts">
import { CurrentMessages } from '@/data/models/currentMessages'
</script>

<style lang="scss" scoped>
.chat-messinput {
  grid-column: 2/3;
  background-color: rgb(199, 167, 167);
  border-radius: 0 0 0 10px;
  height: inherit;

  .messages {
    overflow-y: auto;
    scrollbar-width: thin;
    scrollbar-color: #888 #f0f0f0;
    height: 85%;
    padding: 5px;
    background-color: rgb(199, 167, 167);
    border-bottom: 1px solid rgb(182, 152, 152);


    &::-webkit-scrollbar {
      width: 8px;
    }

    &::-webkit-scrollbar-track {
      background: #f0f0f0;
    }

    &::-webkit-scrollbar-thumb {
      background: #888;
    }

    &::-webkit-scrollbar-thumb:hover {
      background: #555;
    }

    .message {
      display: flex;
      overflow-wrap: anywhere;
      margin-bottom: 10px;
      margin-right: 1rem;
      margin-left: 1rem;
      padding: 8px;
      border-radius: 10px;
      border: 1px solid;
      max-width: 60%;
      width: fit-content;
      color: black;
    }

    .own-message {
      justify-content: end;
      margin-left: auto;
      background-color: #6fc5ff;
      border-color: #67b7ec;
    }

    .received-message {
      justify-content: start;
      background-color: #fff;
      border-color: rgb(221, 221, 221);
    }
  }

  .chat-input {
    display: flex;
    height: 15%;
    gap: 2px;
    background-color: rgb(199, 167, 167);

    button {
      cursor: pointer;
      background-color: rgb(199, 167, 167);
      border: none;
      width: 3.5rem;
      border-radius: 1.5rem;

      &:hover {
        background-color: rgb(182, 152, 152);
      }

      &:active {
        background-color: rgb(182, 152, 152);
      }
    }

    button i {
      display: inline-block;
      transition: transform 0.3s ease;
    }

    button:active i {
      transform: scale(1.5);
    }

    input {
      width: 100%;
      background-color: rgb(199, 167, 167);
      border: 0;
      outline: 0;
      padding-left: 1rem;
      font-weight: 900;

      &:hover {
        background-color: rgb(182, 152, 152);
      }

      &:active {
        background-color: rgb(182, 152, 152);
        border: 0;
      }

      &:focus {
        background-color: rgb(182, 152, 152);
        border: 0;
        transition: border-radius 0.3s ease-in;
        border-radius: 1.5rem;
      }
    }
  }
}
</style>