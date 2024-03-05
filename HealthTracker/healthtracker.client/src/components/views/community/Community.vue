<template>
  <main class="community-page">
    <div class="wall">
      <!-- POSTs -->
      <div v-for="post in PostData">
        <Post :item="post" />
      </div> 
    </div>
    <div class="right-content">
      <FriendsList class="list" />
      <Chat class="chat" />
    </div>
  </main>
</template>

<script lang="ts" setup>
import FriendsList from './friends/FriendsList.vue'
import Chat from './chat/Chat.vue'
import Post from './post/Post.vue'
import { PostData } from '@/data/models/postModels';

</script>

<style lang="scss" scoped>
.community-page {
  display: grid;
  grid-template-columns: 3fr 1fr;
  justify-items: stretch;
  align-items: stretch;

  .wall {
    justify-content: center;
    align-items: center;
    width: calc(100vw - 4rem - 20rem);
  }

  .right-content {
    display: flex;
    flex-direction: column;
    gap: 2rem;
    height: calc(100vh - 8rem);
    width: 20rem;
    position: fixed;
    right: 0;
    transition: width 0.3s ease-out;

    .list {
      flex-grow: 1;
    }

    .chat {
      flex-shrink: 0;
      min-height: 20rem;
    }
  }

  //Do zastanowienia poniższy komentarz \/
  //1. Albo robimy jednego dużego Searchara z wyszukiwaniem znajomych i postów
  //2. Albo ukrywamy jakoś listę zanjomych i ją wysuwamy po wciśnięciu przycisku

  // @media (max-width: 768px),
  // (max-height: 768px) {
  //   .right-content {
  //     justify-content: center;

  //     .list {
  //       display: none;
  //     }
  //   }
  // }
  @media (max-height: 550px) {
    .right-content {
      justify-content: center;
      padding-top: 1rem;
      .list {
        display: none;
      }

      .chat {
        top: 0;
        min-height: calc(100vh - 8rem);
      }
    }
  }


}
</style>