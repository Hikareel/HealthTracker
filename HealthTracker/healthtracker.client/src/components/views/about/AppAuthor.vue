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
                <li v-for="link in author.urls" :key="link.url">
                    <p class="paragraph-link">
                        {{ link.name }}: <a :href="link.url" target="_blank">{{ link.url }}</a>
                    </p>
                </li>
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

.description {
    margin-top: 0.75rem;

    .author-about {
        font-size: larger;
        text-align: center;
    }

    .author-links {
        ul {
            padding-top: 0.25rem;
            list-style-type: disclosure-closed;

            li {
                .paragraph-link {
                    white-space: nowrap;
                    overflow: hidden;
                    text-overflow: ellipsis;

                    a {
                        &:hover {
                            color: gold;
                            text-decoration: underline;
                        }

                        &:active {
                            color: blue;
                            text-decoration: underline;
                        }
                    }
                }
            }
        }

    }
}
</style>