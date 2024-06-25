<template>
    <div class="comment"
        :style="{ marginLeft: depth > 0 ? '1em' : '0', backgroundColor: depth % 2 == 0 ? 'rgb(47 37 37)' : 'rgb(37 27 27)' }">
        <div class="comment-content">
            <div class="comment-header">
                <div class="comment-header-name">{{ item.userFirstName }} {{ item.userLastName }}</div>
                <div class="comment-header-time">{{ formatUtcToLocal(item.dateOfCreate) }}</div>
            </div>
            <div class="comment-body">
                <p>{{ item.content }}</p>
            </div>
        </div>
        <div class="comment-footer" v-if="depth < 4">
            <a @click="toggleCommentResponses">{{ responseButtonLabel }}</a>
            <a @click="toggleMoreComments" v-if="comments.length != 0">{{ moreCommentsButtonLabel }}</a>
        </div>

        <div class="add-comment-div" v-if="isResponseClicked">
            <button @click="addComment"><i class='bi bi-send-fill'></i></button>
            <input v-model="commentToAdd" type="text" placeholder="Reply...">
        </div>
        <div v-if="comments.length != 0">
            <div v-if="isMoreCommentsClicked">
                <Comment v-for="comment in comments" :depth="depth + 1" :key="comment.id" :item="comment"
                    :post-id=comment.postId />
            </div>
        </div>
    </div>
</template>

<script lang="ts" setup>
import { ref, onMounted, computed } from 'vue';
import { type IComment } from '@/data/models/postModels'
import { addCommentToParent, getChildComments } from '@/data/service/api/community/postController';

// const pageNumberOfComments = ref(1); //ToDo: gdy API będzie to obsługiwać. 
const comments = ref<IComment[]>([]);
const commentToAdd = ref('');
const isResponseClicked = ref(false)
const isMoreCommentsClicked = ref(false)
const responseButtonLabel = computed(() => isResponseClicked.value ? 'Hide' : 'Reply');
const moreCommentsButtonLabel = computed(() => {
    if (isMoreCommentsClicked.value) return comments.value.length == 1 ? 'Hide comment' : 'Hide comments'
    if (comments.value.length == 0) {
        return ''
    } else if (comments.value.length == 1) {
        return `Show ${comments.value.length} comment`
    } else {
        return `Show ${comments.value.length} comments`
    }
})

const props = defineProps<{
    item: IComment
    postId: number
    depth: number
}>();

onMounted(async () => {
    await getComments(props.item.id);
});

function formatUtcToLocal(inputDate: string) {
    const date = new Date(inputDate);

    const hours = date.getHours().toString().padStart(2, '0');
    const minutes = date.getMinutes().toString().padStart(2, '0');
    const day = date.getDate().toString().padStart(2, '0');
    const month = (date.getMonth() + 1).toString().padStart(2, '0');
    const year = date.getFullYear();

    return `${hours}:${minutes} ${day}-${month}-${year}.`;
}

function toggleCommentResponses() {
    isResponseClicked.value = !isResponseClicked.value;
}

function toggleMoreComments() {
    isMoreCommentsClicked.value = !isMoreCommentsClicked.value;
}

async function getComments(parentCommentId: number | null) {
    const response = await getChildComments(props.postId, parentCommentId);
    if (response != null) {
        comments.value = response;
    }
}

async function addComment() {
    if (commentToAdd.value) {
        const response = await addCommentToParent(props.postId, props.item.id, commentToAdd.value)
        if (response != null) {
            comments.value.unshift(response);
            isMoreCommentsClicked.value = true;
            commentToAdd.value = '';
            isResponseClicked.value = false;
        }
    } else {
        console.log("Can't add an empty comment");
    }
}

</script>

<style lang="scss" scoped>
.comment {
    width: inherit;
    border: 1px 1px 1px 0px solid lightgray;
    border-radius: 10px;
    padding: 1rem;
    margin-top: 0.5rem;

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

            // .comment-header-name {}

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

            // &:active {}
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

            // &:hover {}

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