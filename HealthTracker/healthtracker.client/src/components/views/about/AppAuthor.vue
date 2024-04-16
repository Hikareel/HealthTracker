<template>
    <div class="author-image">
        <img :src="`/src/assets/about/authors/${author.image}`" :alt="`${author.firstName}'s image`">
    </div>
    <div class="description">
        <div class="author-about">
            <p>{{ author.role }}</p>
            <p>{{ author.firstName }} {{ author.lastName }}</p>
        </div>
        <div class="author-links">
            <p>Links:</p>
            <ul>
                <li v-for="link in author.urls" :key="link.url">{{ link.name }}: {{ link.url }}</li>
            </ul>
        </div>

    </div>

</template>

<script lang="ts" setup>
import type { IAuthor } from '@/data/models/authorsData';

defineProps<{
    author: IAuthor
}>();
</script>

<style lang="scss" scoped>
.author-image {
    display: flex;
    align-content: center;
    justify-content: center;
    img {
        border-radius: 50%;
        height: 20vh;
        width: 20vh;
    }
}
.description{
    .author-about{
        text-align: center;
    }
    .author-links{

    }
}
</style>