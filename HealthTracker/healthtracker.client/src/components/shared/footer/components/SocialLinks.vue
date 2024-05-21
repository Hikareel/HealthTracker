<template>
  <div class="social_links">
    <ul>
      <li v-for="link in SocialLinks">
        <a :href="link.link" target="_blank" class="sl" >
          <i :class="link.icon"></i>
          <span class="link_name">{{ link.name }}</span>
        </a>
      </li>
    </ul>
  </div>
</template>

<script setup lang="ts">
import { SocialLinks } from '@/data/models/socialLinks'
</script>

<style lang="scss" scoped>
.social_links{
  width: 30rem;
  @media (max-width: 768px){
    width: 200px;
  }
  ul{
    display: flex;
    gap: 20px;
    list-style: none;
    @media (max-width: 768px){
      .link_name{
        display: none;
      }
      gap: 0px;
    }
  }
  .sl{
    display: grid;
    grid-template-columns: 0.2fr 1fr;
    gap: 10px;
    &:hover{
      color: gold;
      background-color: transparent;
      .link_name {
        text-decoration: underline;
      }
    }
    &:active{
      color: blue;
      .link_name {
        text-decoration: underline;
      }
    }
  }
}
</style>