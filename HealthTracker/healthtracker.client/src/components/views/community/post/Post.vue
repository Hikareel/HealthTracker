<template>
  <main class="post">
    <div class="content">
      <div class="header">
        <p>{{ props.post.userFirstName }} {{ props.post.userLastName }}</p>
      </div>
      <div class="main">
        <div v-html="safeHtml"></div>
        <div class="attachment"></div>
      </div>
      <div class="footer">
        <button class="like" @click="likePost">
          <i class="bi bi-hand-thumbs-up-fill"></i>&nbsp;{{ props.post.likes.length }}
        </button>
        <button class="comment" @click="toggleComments">
          <i class='bi bi-chat-dots-fill'></i>&nbsp;{{ commentsCount }}
        </button>
      </div>
      <!--Show after click-->
      <div class="comment-section" v-if="isCommentsVisible">
        <div class="comment-input">
          <button @click="addComment"><i class='bi bi-send-fill'></i></button>
          <input type="text" v-model="commentToAdd" placeholder="Write comment...">
        </div>
        <Comment v-for="comment in comments" :key="comment.id" :item="comment" :depth=0 :post-id=comment.postId />
        <button v-if="isMoreComments" @click="getComments">Load more comments</button>
      </div>
    </div>
  </main>
</template>

<script lang="ts" setup>
import Comment from './Comment.vue'
import { ref, computed, onMounted } from 'vue';
import DOMPurify from 'dompurify';
import MarkdownIt from 'markdown-it';
import { currentPosts, type IPost } from '@/data/models/postModels';
import { user } from '@/data/service/userData';
import axios from 'axios';
import type { IComment } from '@/data/models/postModels';
import { getPostComments } from '@/data/service/api/community/postController';

const props = defineProps<{
  post: IPost
}>();
const comments = ref<IComment[]>([]);
const commentsCount = ref(props.post.amountOfComments)
const isCommentsVisible = ref(false);
const isMoreComments = ref(false);
const commentToAdd = ref('');
const pageNr = ref(0);
const pageSize = 10;

const safeHtml = computed(() => {
  const md = new MarkdownIt();
  const rawHtml = md.render(props.post.content);
  return DOMPurify.sanitize(rawHtml);
});

onMounted(async () => {
  await getComments();
});

function toggleComments() {
  isCommentsVisible.value = !isCommentsVisible.value;
}

async function likePost() {
  const postIndex = currentPosts.value.posts.findIndex(post => post.id === props.post.id);
  const likeIndex = currentPosts.value.posts[postIndex].likes.findIndex((like) => like.userId === user.userId);

  try {
    if (likeIndex > -1) {
      await axios.delete(`https://localhost:7170/api/users/${user.userId}/posts/${props.post.id}/likes`, {
        headers: {
          'Authorization': `Bearer ${user.token}`
        },
      });
      currentPosts.value.posts[postIndex].likes.splice(likeIndex, 1);
    } else {
      const response = await axios.post(`https://localhost:7170/api/users/posts/likes`, {
        userId: user.userId,
        postId: props.post.id
      }, {
        headers: {
          'Authorization': `Bearer ${user.token}`
        }
      });
      currentPosts.value.posts[postIndex].likes.push(response.data);
    }
  } catch (error) {
    console.error('Błąd podczas aktualizacji polubień', error);
  }
}

async function getComments() {
  pageNr.value += 1;
  const recivedData = await getPostComments(props.post.id, pageNr.value, pageSize);
  if (recivedData.comments) {
    recivedData.comments.forEach((element: IComment) => {
      comments.value.push(element);
    });
    if (recivedData.totalCommentsLeft > 0){
      isMoreComments.value = true;
    }else{
      isMoreComments.value = false;
    }
  }
}

async function addComment() {
  if (commentToAdd.value) {
    try {
      const response = await axios.post(`https://localhost:7170/api/users/posts/comments`, {
        postId: props.post.id,
        userId: user.userId,
        content: commentToAdd.value
      }, {
        headers: {
          'Authorization': `Bearer ${user.token}`
        }
      });

      if (response.status === 201) {
        commentsCount.value += 1;
        comments.value.unshift(response.data);
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
.post {
  margin-top: 0.5rem;
  margin-bottom: 0.5rem;
  width: 95%;
  border: 1px solid black;
  border-radius: 1rem;
  background-color: rgba(62, 50, 50, 1);

  .content {
    width: 100%;

    .header {
      border-radius: 1rem 1rem 0 0;
      padding: 0.5rem;
    }

    .main {
      border-top: 2px solid rgb(73, 61, 61);
      padding: 1rem;
    }

    .footer {
      border-radius: 0 0 1rem 1rem;
      text-align: center;
      display: flex;
      height: 1.5rem;

      button {
        background-color: rgb(73, 61, 61);
        color: white;
        width: 100%;
        border: none;
        border-radius: 1rem;

        &:hover {
          background-color: rgb(112, 112, 112);
        }
      }
    }

    .footer button i {
      display: inline-block;
      transition: transform 0.3s ease;
    }

    .footer button:active i {
      transform: scale(1.1);
    }

    .comment-section {
      padding: 1rem;
      width: inherit;

      .comment-input {
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

          // &:active {

          // }
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
  }

}
</style>