<template>
  <div class="chat-messinput">
    <div class="messages">
      <div v-for="message in currentMessages" :key="message.id"
        :class="['message', message.isYours ? 'own-message' : 'received-message']">
        {{ message.text }}
      </div>
    </div>
    <div class="chat-input">
      <button @click="sendMessage"><i class='bi bi-send-fill'></i></button>
      <input type="text" v-model="messageToSend" placeholder="Write message..." @keyup.enter="sendMessage" />
    </div>
  </div>
</template>

<script setup lang="ts">
import type { FriendModel } from '@/data/models/friendModel';
import { ref, defineProps } from 'vue';

interface Message {
  id: number,
  text: string;
  isYours: boolean;
}

const props = defineProps<{
  currentMessages: Message[];
  connection: any;
  friendToChat: FriendModel | null;
}>();

const messageToSend = ref('');


async function sendMessage() {
  if (messageToSend.value.trim() !== '' && props.friendToChat !== null) {
    try {
      await props.connection.invoke("SendMessageToUser", localStorage.getItem('userId'), props.friendToChat.userId, messageToSend.value); // SendMessages userIdFrom, userIdTo, text
      messageToSend.value = '';
    } catch (err) {
      console.error(err);
    }
  }
}
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