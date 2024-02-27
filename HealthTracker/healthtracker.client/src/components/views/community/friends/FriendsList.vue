<template>
    <main class="friends">
        <div class="search">
            <p>serach:</p>
            <input>
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
<style>
.friends {
    background-color: blue;
}
.friends-list{

}
</style>