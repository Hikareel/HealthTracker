<template>
    <div class="comment" :style="{ marginLeft: depth > 0 ? '1em' : '0' }">
        <div class="comment-content">
            <div class="comment-header">
                <div>{{ item.userId }}</div>
                <div>2024-01-01</div> <!--Zmiana na imię i nazwisko + data stworzenie komentarza-->
            </div>
            <div class="comment-body">
                <p>{{ item.content }}</p>
            </div>
        </div>
        <div class="comment-footer" v-if="depth < 4">
            <a @click="toggleCommentRespone">Respone</a>
            <a @click="toggleMoreComments" v-if="comments.length != 0">Show {{ comments.length }} responses</a>
        </div>

        <div class="add-comment-div" v-if="isResponseClicked">
            <button @click="addCommentToParent"><i class='bi bi-send-fill'></i></button>
            <input v-model="commentToAdd" type="text" placeholder="Respone...">
        </div>
        <div v-if="comments.length != 0">

            <div v-if="isMoreCommentsClicked">
                <Comment v-for="comment in comments" :depth="depth + 1" :key="comment.id" :item="comment"
                    :post-id=comment.postId />
            </div>
        </div>
        <!-- ZAŁADUJ WIĘCEJ KOMENTARZY (paginacja)-->
        <div>

        </div>
    </div>
</template>

<script lang="ts" setup>
import { ref, onMounted } from 'vue';
import { type IComment } from '@/data/models/postModels'
import axios from 'axios';

const pageNumberOfComments = ref(1); //ToDo: gdy API będzie to obsługiwać. 
const comments = ref<IComment[]>([]);
const commentToAdd = ref('');
const isResponseClicked = ref(false)
const isMoreCommentsClicked = ref(false)

const props = defineProps<{
    item: IComment
    postId: number
    depth: number
}>();

onMounted(async () => {
    await getComments(props.item.id);
});

function toggleCommentRespone() {
    isResponseClicked.value = !isResponseClicked.value;
}
function toggleMoreComments() {
    isMoreCommentsClicked.value = !isMoreCommentsClicked.value;
}

async function getComments(parentCommentId: number | null) {
    try {
        const response = await axios.get(`https://localhost:7170/api/users/posts/${props.postId}/comments/${parentCommentId}`);
        comments.value = response.data;
    } catch (error) {
        console.error('Error fetching comments', error);
    }
}

async function addCommentToParent() {
    if (commentToAdd.value) {
        try {
            const response = await axios.post(`https://localhost:7170/api/users/posts/comments/${props.item.id}`, {
                postId: props.postId,
                userId: props.item.userId,
                content: commentToAdd.value
            });
            if (response.status === 201) {
                comments.value.push(response.data);
                isMoreCommentsClicked.value = true
            } else {
                alert(response.status + " " + response.statusText)
            }
        } catch (error) {
            console.error('Błąd podczas dodawania komentarza', error);
        } finally {
            commentToAdd.value = '';
        }
    } else {
        console.log("Pusty komentarz nie może zostać dodany");
    }
}
</script>

<style lang="scss" scoped>
.comment {
    border: 1px solid $success-color;
    border-radius: 10px;
}
</style>