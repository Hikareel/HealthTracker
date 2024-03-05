<template>
  <main class="post">
    <div class="content">
      <div class="header">
        <p>{{ item.user }}</p>
      </div>
      <div class="main">
        <div v-html="safeHtml"></div>
        <div class="attachment"></div>
      </div>
      <div class="footer">
        <button class="like">
          <i class="bi bi-hand-thumbs-up-fill"></i>
        </button>
        <button class="comment" @click="toggleComments">
          <i class='bi bi-send-fill'></i>
        </button>
      </div>
      <!--Show after click-->
      <div class="comment-section" v-if="isCommentsVisible">
        <p>comment</p>
      </div>
    </div>
  </main>
</template>

<script lang="ts" setup>
import { ref, computed } from 'vue';
import DOMPurify from 'dompurify';
import MarkdownIt from 'markdown-it';
import type { IPostModel } from '@/data/models/postModels';

const { item } = defineProps<{
  item: IPostModel
}>();

const md = new MarkdownIt();
const isCommentsVisible = ref(false);

const safeHtml = computed(() => {
  const rawHtml = md.render(item.markdownText);
  return DOMPurify.sanitize(rawHtml);
});

function toggleComments() {
  isCommentsVisible.value = !isCommentsVisible.value;
}

</script>

<style lang="scss" scoped>
.post {
  width: calc(100vw - 30rem);
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