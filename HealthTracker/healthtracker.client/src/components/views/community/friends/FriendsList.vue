<template>
    <main class="main-list">
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