<template>
  <main class="community-page">

    <div class="wall" :class="`${is_mobile_expanded && 'is_mobile_expanded'}`">
      <div class="wall-header">
        <div class="search">
          <input placeholder="Search..." class="search-input">
        </div>
        <div class="friends-button">
          <button class="menu-toggle" @click="ToggleMobile">
            <span class="material-icons">
              keyboard_double_arrow_down
            </span>
          </button>
        </div>
      </div>
      <div class="mobile-expander">
        <FriendsList :friends="friends" @select="friendSelected" class="list-mobile" />
        <ChatBox :friendToChat="currentMessages.friendToChat" :messages="currentMessages.messages"
          :connection="connection" class="chat-mobile" />
      </div>
      <div class="wall-body">
        <!-- POSTs -->
        <div v-for="post in PostData" class="posts">
          <Post :item="post" />
        </div>
      </div>
    </div>

    <div class="right-content">
      <FriendsList :friends="friends" @select="friendSelected" class="list" />
      <Chat :friendToChat="currentMessages.friendToChat" :messages="currentMessages.messages" :connection="connection"
        class="chat" />
    </div>

  </main>
</template>

<script lang="ts" setup>
import FriendsList from './friends/FriendsList.vue'
import { type FriendModel } from '@/data/models/friendModel'
import Chat from './chat/Chat.vue'
import ChatBox from './chat/ChatBox.vue';
import Post from './post/Post.vue'
import { PostData } from '@/data/models/postModels';
import { ref, onMounted } from "vue";
import axios from 'axios';
import { HubConnectionBuilder } from '@microsoft/signalr';
import { user } from '../../../data/service/userData'
import { currentMessages } from '@/data/models/messageModel';
import { v4 as uuidv4 } from 'uuid';

const is_mobile_expanded = ref(false)
const ToggleMobile = () => {
  is_mobile_expanded.value = !is_mobile_expanded.value
}
const friends = ref<FriendModel[]>([]);

const friendSelected = (friend: FriendModel) => {
  currentMessages.value.friendToChat = friend;
  getCurrentUsersMessagesWithFriend(friend.userId);
};

let connection = new HubConnectionBuilder()
  .withUrl("https://localhost:7170/chatHub", {
    accessTokenFactory: () => user.token ?? ""
  })
  .build();

onMounted(async () => {
  getFriendList();
  connectToChatHub();
});

async function connectToChatHub() {
  try {
    if (!user.userId) {
      return;
    }

    await connection.start();
    console.log("Connected to Chat");
    connection.on("ReceiveMessage", (userFrom, userTo, message) => {
      console.log(message)
      const isYours = userFrom === user.userId;
      currentMessages.value.messages.push({
        id: uuidv4(),
        text: message,
        isYours: isYours
      });
    });
  } catch (err) {
    console.error("Error connecting to Chat:", err);
  }
}

async function getCurrentUsersMessagesWithFriend(friendId: number) {
  //Obsłuzyć może jakoś różne status cody.
  try {
    const response = await axios.get(`https://localhost:7170/api/users/messages/${user.userId}/${friendId}/`, {
      params: {
        pageNumber: currentMessages.value.pageNumber,
        pageSize: currentMessages.value.pageSize
      }
    });

    const messages = response.data.map((message: { id: number; text: string; userIdFrom: number; }) => ({
      id: message.id,
      text: message.text,
      isYours: message.userIdFrom === user.userId
    })).reverse();
    currentMessages.value.messages = messages

    console.log(currentMessages.value)
  } catch (error) {
    console.error(error);
  }
}

async function getFriendList() {
  try {
    if (!user.userId) {
      return;
    }
    const response = await axios.get(`https://localhost:7170/api/users/${user.userId}/friends`);
    friends.value = response.data;
  } catch (error) {
    console.error(error);
  }
}

</script>

<style lang="scss" scoped>
.community-page {
  position: fixed;
  display: flex;
  height: calc(100% - 4rem);
  width: calc(100% - 4rem);
  justify-items: stretch;
  align-items: stretch;

  .wall {
    display: flex;
    flex-direction: column;
    grid-column: 1;
    align-items: center;
    width: 80%;
    height: 100%;

    .wall-header {
      display: flex;
      grid-row: 1;
      grid-column: 1;
      border-bottom: 1px solid #d3d3d3;
      width: 100%;
      height: 10%;
      position: sticky;
      top: 0;
      background-color: inherit;
      z-index: 10;
      align-content: center;

      .search {
        display: flex;
        grid-row: 1;
        grid-column: 1;
        padding: 0.5rem;
        justify-content: center;
        align-items: center;
        height: 100%;
        width: 100%;

        .search-input {
          height: 100%;
          width: 35%;
          box-sizing: border-box;
          border: 2px solid #ccc;
          border-radius: 8px;
          font-size: 16px;
          background-color: white;
          background-image: url('/src/assets/search.svg');
          background-position: center left 10px;
          background-repeat: no-repeat;
          justify-content: center;
          text-align: center;
          padding: 12px 20px 12px 40px;
          transition: width 0.4s ease-in-out;

          &:focus {
            width: 90%;
          }
        }
      }

      .friends-button {
        display: none;
        justify-content: center;
        align-content: center;
        flex-wrap: wrap;
        padding: 0.5rem;

        .friends-button button {
          height: fit-content;
          width: fit-content;
        }

        .menu-toggle {
          .material-icons {
            transition: 0.5s ease-out;
          }
        }
      }
    }

    .mobile-expander {
      height: 0;
      overflow: hidden;
      opacity: 0;
      transition: height 0.5s ease, opacity 0.5s ease;

      .list-mobile {
        height: 50%;
        margin-bottom: 0;
      }

      .chat-mobile {
        height: 50%;
      }
    }

    .wall-body {
      grid-row: 2;
      grid-column: 1;
      height: 100%;
      width: 100%;
      overflow-y: scroll;
      overflow-x: hidden;
      transition: height 0.5s ease-out;

      .posts {
        display: flex;
        width: 100%;
        justify-content: center;
      }
    }

    &.is_mobile_expanded {
      .mobile-expander {
        height: 100%;
        opacity: 1;
        transition: 0.7s ease-in;
      }

      .wall-body {
        height: 0;
        transition: height 0.7s ease-in;
      }

      .wall-header .friends-button .menu-toggle .material-icons {
        transform: scaleY(-1);
        transition: 0.5s ease-out;
      }
    }
  }

  .right-content {
    top: 0;
    width: 25%;
    display: flex;
    justify-content: space-between;
    flex-direction: column;
    height: 100%;
    right: 0;
    transition: width 0.3s ease-out;

    .list {
      padding-bottom: 0.5rem;
      flex-grow: 1;
    }

    .chat {
      flex-shrink: 0;
      height: 20rem;
    }
  }

  @media (max-height: 590px),
  (max-width: 785px) {
    .wall {
      width: 100%;

      .wall-header {
        .friends-button {
          display: flex;
        }
      }
    }

    .right-content {
      display: none;
    }
  }
}
</style>