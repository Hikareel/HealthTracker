<template>
  <main class="community-page">

    <div class="wall">
      <div class="wall-header">
        <div class="search">
          <input placeholder="Search..." class="search-input">
        </div>
        <div class="wall-body">
          <!-- POSTs -->
          <div v-for="post in PostData">
            <Post :item="post" />
          </div>
        </div>
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
  grid-template-rows: auto 1fr;
  grid-template-columns: 3fr 1fr;
  justify-items: stretch;
  align-items: stretch;

  .wall {
    padding-top: 1rem;
    display: flex;
    flex-direction: column;
    grid-column: 1;
    justify-content: center;
    align-items: center;

    .wall-header{
      //TODO
    }
    .wall-body{
      //TODO
    }
  }

  .right-content {
    display: flex;
    flex-direction: column;
    gap: 2rem;
    grid-column: 2;
    right: 0;
    transition: width 0.3s ease-out;

    .list {
      flex-grow: 1;
    }

    .chat {
      flex-shrink: 0;

    }
  }

  .search {
    display: flex;
    grid-row: 1;
    grid-column: 1;
    padding-top: 1rem;
    justify-content: center;
    align-items: center;
    height: fit-content;

    .search-input {
      width: 10rem;
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

  //Do zastanowienia poniższy komentarz \/
  //1. Albo robimy jednego dużego Searchara z wyszukiwaniem znajomych i postów
  //2. Albo ukrywamy jakoś listę zanjomych i ją wysuwamy po wciśnięciu przycisku
  @media (max-height: 590px),
  (max-width: 785px) {
    .search {
      // width: calc(100vw - 4rem - 4rem);
    }

    .wall {
      // width: calc(100vw - 4rem - 4rem);
    }

    .right-content {
      justify-content: center;
      padding-top: 1rem;
      width: 4rem;

      .list {
        display: none;
      }

      .chat {
        top: 0;
        // min-height: calc(100vh - 8rem);
      }
    }
  }


}
</style>