<template>
    <div class="comment" :style="{ marginLeft: depth > 0 ? '1em' : '0' }">
        <div class="comment-content">
            <div class="comment-header">
                <div class="comment-header-name">{{ item.userFirstName }} {{ item.userLastName }}</div>
                <div class="comment-header-time">{{ item.dateOfCreate }}</div>
            </div>
            <div class="comment-body">
                <p>{{ item.content }}</p>
            </div>
        </div>
        <div class="comment-footer" v-if="depth < 4">
            <a @click="toggleCommentRespone">Response</a>
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
                comments.value.unshift(response.data);

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
    width: inherit;
    border: 1px solid $success-color;
    border-radius: 10px;

    .comment-content {
        width: inherit;

        .comment-header {
            padding-top: 0.25rem;
            padding-left: 0.25rem;
            display: flex;
            gap: 0.5rem;
            justify-items: center;
            align-items: baseline;
            width: inherit;

            .comment-header-name {}

            .comment-header-time {
                font-size: 70%;
                opacity: 80%;
            }
        }

        .comment-body {
            width: inherit;
            min-height: 2rem;
            padding: 0.5rem;
        }

        .comment-footer {
            width: inherit;

            .comment-footer a {
                color: yellow;
            }
        }
    }

    .add-comment-div {

        display: flex;
        height: 15%;
        gap: 2px;
        width: inherit;

        button {
            background-color: rgb(73, 61, 61);
            cursor: pointer;
            border: none;
            width: 3.5rem;
            border-radius: 1.5rem;

            &:hover {
                background-color: rgb(112, 112, 112);
            }

            &:active {}
        }

        button i {
            display: inline-block;
            transition: transform 0.3s ease;
        }

        button:active i {
            transform: scale(1.5);
        }

        input {
            height: 2.5rem;
            background-color: rgb(73, 61, 61);
            color: white;
            width: 100%;
            border: 0;
            outline: 0;
            padding-left: 1rem;
            font-weight: 400;
            border-radius: 1.5rem;

            &:hover {}

            &:active {
                border: 0;
            }

            &:focus {
                border: 0;
            }
        }

    }
}
</style>