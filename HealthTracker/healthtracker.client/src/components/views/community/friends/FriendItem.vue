<template>
  <main class="friend-item">
    <p>{{item.firstname}} {{item.secondname}}</p>
  </main>
</template>
<script lang="ts" setup>
import { defineProps } from 'vue';
import type { FriendsModel } from './data/friendModel'

const props = defineProps<{
  item: FriendsModel
}>()


</script>
<style></style>