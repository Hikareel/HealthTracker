<template>
  <div class="sidebar_item">
    <p>
      <router-link class="item" :to="`${item.link}`">
        <span class="material-icons">{{ item.icon }}</span>
        <span class="text">{{ item.name }}</span>
      </router-link>
    </p>
  </div>
</template>
<script lang="ts" setup>
  import type { LinkItem } from '../data/sidebarLinks';
  const props = defineProps<{
    item: LinkItem
  }>()
</script>
<style lang="scss">
aside{
  .menu{
    .item{
      color: white;
      display: flex;
      align-items: center;
      text-decoration: none;
      padding: 0.5rem 1.2rem;

      &:hover, &.router-link-exact-active{
        .material-icons, .text{
          color:gold;
        }
      }

      &.router-link-exact-active{
        border-right: 5px solid white;
      }

      .text{
        padding-left: 1rem;
        transition: opacity 0.5s ease, font-size 0.35s ease;
        font-size: 0;
        opacity: 0;
      }
    }
  }

  &.is-expanded{
    .menu{
      .item .text{
        transition: font-size 0.5s ease;
        padding-left: 1rem;
        opacity: 1;
        font-size: medium;
      }
    }
  }
}


</style>