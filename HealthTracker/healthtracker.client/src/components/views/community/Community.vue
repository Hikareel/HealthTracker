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
        <div v-for="post in PostData">
          <Post :item="post" class="post"/>
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
    display: flex;
    flex-direction: column;
    grid-column: 1;
    // height:  100%;
    padding: 1rem;
    align-items: center;


    .wall-header{
      border-bottom: 1px solid white;
      width: 100%;
      position: sticky;
      top: 0;
      background-color: inherit;
      z-index: 10;
    }
    .wall-body{
      overflow-y:scroll;
      overflow-x: hidden;
      flex: 1;

      .post{
        margin:1rem 2rem 1rem 2rem;
      }
    }
  }

  .right-content {
    display: flex;
    flex-direction: column;
    height: 100%;
    right: 0;
    transition: width 0.3s ease-out;

    .list{
      height:50%;
      padding-bottom: 0.5rem;
    }
    .chat{
      padding-top: 0.5rem;
      height:50%;
    }
  }

  .search {
    display: flex;
    grid-row: 1;
    grid-column: 1;
    padding: 1rem;
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

    .right-content {
      justify-content: center;
      padding-top: 1rem;
      width: 4rem;

      .list {
        display: none;
      }
    }
  }


}
</style>