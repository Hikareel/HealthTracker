<template>
  <main class="community-page">

    <div class="wall">
      <div class="wall-header">
        <div class="search">
          <input placeholder="Search..." class="search-input">
        </div>
      </div>
      <div class="wall-body">
        <!-- POSTs -->
        <div v-for="post in PostData" class="posts">
          <Post :item="post" />
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
      grid-row: 1;
      grid-column: 1;
      border-bottom: 1px solid white;
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
    }

    .wall-body {
      grid-row: 2;
      grid-column: 1;
      height: 90%;
      width: 100%;
      overflow-y: scroll;
      overflow-x: hidden;

      .posts {
        display: flex;
        width: 100%;
        justify-content: center;
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
      max-height: 50%;
      flex-grow: 0;

    }

    .chat {
      flex-grow: 1;
      flex-shrink: 0;
      padding-top: 0.5rem;
    }
  }

  //Do zastanowienia poniższy komentarz \/
  //1. Albo robimy jednego dużego Searchara z wyszukiwaniem znajomych i postów
  //2. Albo ukrywamy jakoś listę zanjomych i ją wysuwamy po wciśnięciu przycisku
  @media (max-height: 590px),
  (max-width: 785px) {
    .wall {
      width: 100%;
    }

    .right-content {
      display: none;
    }
  }
}
</style>