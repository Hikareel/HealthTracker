<template>
  <main class="post">
    <div class="content">
      <div class="header">
        <p>{{ item.user }}</p>
      </div>
      <div class="main" v-html="safeHtml"></div>
      <div class="footer">
        <!-- Footer content -->
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
</style>