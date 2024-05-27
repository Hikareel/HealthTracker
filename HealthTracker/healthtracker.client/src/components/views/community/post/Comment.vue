<template>
    <!-- DODAJ KOMENTARZ -->
    <div class="add-comment">
        <p>komentarz: {{ depth }}</p>
    </div>
    <!-- KOMENTARZ -->
    <div class="add-comment">
        
    </div>
    <!-- ROZWIŃ KOMENTARZE DZIECI (jeśli istnieją) -->
    <Comment v-for="comment in comments" :key="comment.id" :item="comment" :depth=depth+1  :post-id=comment.postId />
    <!-- ZAŁADUJ WIĘCEJ KOMENTARZY (jak będzie już paginacja)-->
</template>

<script lang="ts" setup>
import { ref, onMounted } from 'vue';
import { type IComment } from '@/data/models/postModels'
import axios from 'axios';
import { user } from '@/data/service/userData';

const props = defineProps<{
    item: IComment
    postId: number
    depth: number
}>();

const comments = ref<IComment[]>([]);

onMounted(async () => {
    await getComments(props.item.id);
});

async function getComments(parentCommentId: number | null) {
    try {
        console.log(`https://localhost:7170/api/users/posts/${props.postId}/comments/${parentCommentId}`);
        const response = await axios.get(`https://localhost:7170/api/users/posts/${props.postId}/comments/${parentCommentId}`);
        comments.value = response.data;
        console.log(response.data);
    } catch (error) {
        console.error('Error fetching comments', error);
    }
}
</script>

<style lang="scss" scoped></style>