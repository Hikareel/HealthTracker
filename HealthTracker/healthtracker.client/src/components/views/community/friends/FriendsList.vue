<template>
    <main class="friends">
        <div class="search">
            <input placeholder="Search..." class="search-input">
        </div>
        <div class="friends-list">
            <FriendItem v-for="obj in FriendsData" :item="obj" :key="obj" />
        </div>
    </main>
</template>
<script lang="ts" setup>
import FriendItem from './FriendItem.vue'
import { FriendsData } from './data/friendModel'
</script>
<style scope>
.friends {
    padding: 1rem;
    justify-content:center;
    text-align:center;
}

.friends-list {}
.search{
    
}

.search-input {
    width: 10rem;
    box-sizing: border-box;
    border: 2px solid #ccc;
    border-radius: 4px;
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