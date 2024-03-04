<template>
    <main class="main-list">
        <div class="search">
            <input placeholder="Search..." class="search-input">
        </div>
        <div class="friends-list">
            <FriendItem v-for="obj in FriendsData" :item="obj"/>
        </div>
    </main>
</template>

<script lang="ts" setup>
import FriendItem from './FriendItem.vue'
import { FriendsData } from '@/data/models/friendModel'
</script>

<style lang="scss" scoped>
.main-list {
    display: flex;
    flex-direction: column;
    max-height: 100vh;
    overflow: hidden;
    text-align: center;
    .search{
        padding-top: 1rem;
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
    .friends-list {
        margin-top: 1rem;
        overflow-y: auto;
        -ms-overflow-style: none;
        scrollbar-width: none;
        &::-webkit-scrollbar { 
            display: none;
        }
        flex-grow: 1;
    }
}
</style>