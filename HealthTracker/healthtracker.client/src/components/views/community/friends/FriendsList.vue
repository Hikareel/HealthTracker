<template>
    <main class="main-list">
        <div class="friends-list">
            <FriendItem v-for="friend in friends" :item="friend" :key="friend.userId" @click="selectFriend(friend)"/>
        </div>
    </main>
</template>

<script lang="ts" setup>
import FriendItem from './FriendItem.vue'
import { type FriendModel } from '@/data/models/friendModel'

defineProps<{
    friends: FriendModel[]
}>();

const emit = defineEmits(['select']);

const selectFriend = (friend: FriendModel) => {
  emit('select', friend);
};
</script>

<style lang="scss" scoped>
.main-list {
    margin-bottom: 1rem;
    display: flex;
    flex-direction: column;
    height: 100%;
    overflow: hidden;
    text-align: center;

    &::after {
        content: '';
        margin-left: auto;
        margin-right: auto;
        width: 90%;
        height: 1px;
        background-color: #d3d3d3;
        bottom: 0;
        margin-top: 0.5rem;
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