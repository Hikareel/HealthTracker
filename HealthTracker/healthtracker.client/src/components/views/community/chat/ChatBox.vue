<template>
  <div class="chat-messinput">
    <div class="messages" ref="messagesContainer" @scroll="handleScroll">
      <div v-for="message in chatStore.messages"
        :class="['message', message.isYours ? 'own-message' : 'received-message']" :key="message.id">
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
import { ref, defineProps, onMounted, nextTick, watch } from 'vue';
import { useUserStore } from '@/store/account/auth';
import { getMessagesWithFriend } from '@/data/service/api/community/chatController';
import { useChatStore } from '@/store/community/chatStore';

const userStore = useUserStore();
const chatStore = useChatStore();
const messagesContainer = ref();
const messageToSend = ref('');

const props = defineProps<{
  connection: any;
}>();

onMounted(async () => {
  chatStore.pageNumber = 1;
  await loadMessages();
  await nextTick(() => {
    messagesContainer.value.scrollTop = messagesContainer.value.scrollHeight;
  });

});

const handleScroll = async () => {
  if (messagesContainer.value.scrollTop === 0 && chatStore.pageNumber > 1) {
    await loadMessages();
  }
};

watch(
  () => chatStore.friendToChat,
  async () => {
    chatStore.pageNumber = 1;
    chatStore.setMessages([]);
    await loadMessages();
    await nextTick(() => {
    messagesContainer.value.scrollTop = messagesContainer.value.scrollHeight;
  });
  },
)


async function loadMessages() {
  if (chatStore.friendToChat == null) return;
  const actualScrollHeight = messagesContainer.value.scrollHeight;
  const response = await getMessagesWithFriend(chatStore.friendToChat.userId, chatStore.pageNumber, chatStore.pageSize);
  if (response.length > 0) {
    const userStore = useUserStore()
    chatStore.addMessagesFromAPI(response, userStore.userId);
    chatStore.pageNumber++;

    await nextTick();
    messagesContainer.value.scrollTop += (messagesContainer.value.scrollHeight - actualScrollHeight);
  }
}

async function sendMessage() {
  if (messageToSend.value.trim() !== '' && chatStore.friendToChat !== null) {
    try {
      if (userStore.userId) {
        await props.connection.invoke("SendMessageToUser", userStore.userId, chatStore.friendToChat.userId, messageToSend.value);
        messageToSend.value = '';
        nextTick().then(() => {
          messagesContainer.value.scrollTop = messagesContainer.value.scrollHeight;
        });
      }
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