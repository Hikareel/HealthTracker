<template>
    <main class="main-list">
        <div class="search">
            <input placeholder="Search..." class="search-input">
        </div>
        <div class="friends-list">
            <FriendItem v-for="obj in FriendsData" :item="obj" :key="obj"/>
        </div>
    </main>
</template>
<script lang="ts" setup>
import FriendItem from './FriendItem.vue'
import { FriendsData } from './data/friendModel'
</script>
<style scope>
.main-list {
    text-align:center;
}

.friends-list {
    margin-top: 1rem;
    max-height: 20rem;
    overflow: auto;
    -ms-overflow-style: none;
    scrollbar-width: none;
}

.friends-list::-webkit-scrollbar { 
    display: none;
}

.search{

}

.search-input {
    width: 10rem;
    box-sizing: border-box;
    border: 2px solid #ccc;
    border-radius: 8px;
    font-size: 16px;
    background-color: white;
    background-image: url('/src/assets/search.svg');
    background-position: center left+10px;
    background-repeat: no-repeat;
    padding: 12px 20px 12px 40px;
    transition: width 0.4s ease-in-out;
}

.search-input:focus {
    width: 90%;
}
</style>