<template>
  <main class="post">
    <div class="content">
      <div class="header">
        <p>{{ item.userFirstName }} {{ item.userLastName }}</p>
      </div>
      <div class="main">
        <div v-html="safeHtml"></div>
        <div class="attachment"></div>
      </div>
      <div class="footer">
        <button class="like" @click="likePost">
          <i class="bi bi-hand-thumbs-up-fill"></i>&nbsp;{{ item.likes.length }}
        </button>
        <button class="comment" @click="toggleComments">
          <i class='bi bi-chat-dots-fill'></i>&nbsp;{{ item.comments.length }}
        </button>
      </div>
      <!--Show after click-->
      <div class="comment-section" v-if="isCommentsVisible">
        <button @click="addComment"><i class='bi bi-send-fill'></i></button>
        <input type="text" v-model="commentToAdd" placeholder="Write comment...">
        <Comment v-for="comment in item.comments" :key="comment.id" :item="comment" :depth=0 :post-id=comment.postId />
      </div>
    </div>
  </main>
</template>

<script lang="ts" setup>
import Comment from './Comment.vue'
import { ref, computed } from 'vue';
import DOMPurify from 'dompurify';
import MarkdownIt from 'markdown-it';
import { currentPosts, type IPost } from '@/data/models/postModels';
import { user } from '@/data/service/userData';
import axios from 'axios';

const { item } = defineProps<{
  item: IPost
}>();

const isCommentsVisible = ref(false);
const commentToAdd = ref('');

const safeHtml = computed(() => {
  const md = new MarkdownIt();
  const rawHtml = md.render(item.content);
  return DOMPurify.sanitize(rawHtml);
});

function toggleComments() {
  isCommentsVisible.value = !isCommentsVisible.value;
}

async function likePost() {
  const postIndex = currentPosts.value.posts.findIndex(post => post.id === item.id);
  const likeIndex = currentPosts.value.posts[postIndex].likes.findIndex((like) => like.userId === user.userId);

  try {
    if (likeIndex > -1) {
      await axios.delete(`https://localhost:7170/api/users/${user.userId}/posts/${item.id}/likes`);
      currentPosts.value.posts[postIndex].likes.splice(likeIndex, 1);
    } else {
      const response = await axios.post(`https://localhost:7170/api/users/posts/likes`, {
        userId: user.userId,
        postId: item.id
      });
      currentPosts.value.posts[postIndex].likes.push(response.data);
    }
  } catch (error) {
    console.error('Błąd podczas aktualizacji polubień', error);
  }
}

async function addComment() {
  if (commentToAdd.value) {
    try {
      const response = await axios.post(`https://localhost:7170/api/users/posts/comments`, {
        postId: item.id,
        userId: user.userId,
        content: commentToAdd.value
      }
      );

      if (response.status === 201) {
        item.comments.push(response.data);
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
  }
}
</style>