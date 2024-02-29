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

<style scoped>
  ul{
    display: flex;
    gap: 20px;
    list-style: none;
    @media (max-width: 768px){
      gap: 0px;
      flex-direction: column;
    }
  }
  .sl{
    display: grid;
    grid-template-columns: 0.2fr 1fr;
    gap: 10px;
  }
  .sl:hover{
    color: gold;
    background-color: transparent;
  }
  .sl:active{
    color: blue;
  }
  .sl:hover .link_name,
  .sl:active .link_name {
    text-decoration: underline;
  }
  .social_links{
    width: 30rem;
    @media (max-width: 768px){
      width: 200px;
    }
  }
</style>