<template>
    <p>{{ author.firstName }}</p>
</template>

<script lang="ts" setup>
import type { IAuthor } from '@/data/models/authorsData';

defineProps<{
    author: IAuthor
}>();
</script>

<style lang="scss" scoped>

</style>