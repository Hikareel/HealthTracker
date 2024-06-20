<template>
    <div :style="{ marginLeft: '1em' }">
        <div>
            <p>{{ item.content }}</p>
        </div>
        <a @click="toggleCommentRespone">Respone</a>
        <div class="add-comment-div" v-if="isResponseClicked">
            <button @click="addCommentToParent"><i class='bi bi-send-fill'></i></button>
            <input v-model="commentToAdd" type="text" placeholder="Respone...">
        </div>
        <div v-if="comments.length != 0">
            <a @click="toggleMoreComments">Show {{ comments.length }} responses</a>
            <div v-if="isMoreCommentsClicked">
                <Comment v-for="comment in comments" :key="comment.id" :item="comment" :post-id=comment.postId />
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
            const response = await axios.post(`https://localhost:7170/api/users/posts/comments/`, {
                postId: props.postId,
                userId: props.item.userId,
                content: commentToAdd.value
            }, {
                params: {
                    parenCommentId: props.item.parentCommentId
                }
            }
            );

            if (response.status === 201) {
                comments.value.push(response.data);
                isMoreCommentsClicked.value = true
            }else{
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

<style lang="scss" scoped></style>