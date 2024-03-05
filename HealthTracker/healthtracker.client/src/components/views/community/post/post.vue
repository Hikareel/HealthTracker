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
        <button class="comment">
          <i class="bi bi-hand-thumbs-up-fill"></i>
        </button>
        <button class="like">
          <i class='bi bi-send-fill'></i>
        </button>
      </div>
      <!--Show after click-->
      <div class="comment-section">
        <p>comment</p>
      </div>
    </div>
  </main>
</template>

<script lang="ts" setup>
import { computed } from 'vue';
import DOMPurify from 'dompurify';
import MarkdownIt from 'markdown-it';
import type { IPostModel } from '@/data/models/postModels';

const { item } = defineProps<{
  item: IPostModel
}>();

const md = new MarkdownIt();

const safeHtml = computed(() => {
  const rawHtml = md.render(item.markdownText);
  return DOMPurify.sanitize(rawHtml);
});

</script>

<style lang="scss" scoped>

.post{
  width: calc(100vw - 30rem);
  border: 1px solid black;
  border-radius: 1rem;
  .header{
    border-radius: 1rem 1rem 0 0;
    padding: 0.25rem;
    background-color: $error-color;
  }
  .main{
    padding: 0.5rem;
    background-color: lightblue;
  }
  .footer{
    border-radius: 0 0 1rem 1rem;
    background-color: $form-bg;
    text-align: center;
    display: flex;

    button{
      width: 100%;
      border: none;
      border-radius: 1rem;
    }
  }
}

</style>