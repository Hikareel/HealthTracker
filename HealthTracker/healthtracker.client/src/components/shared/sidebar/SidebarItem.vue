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
  interface Item{
    name: String,
    link: String,
    icon: String
  }
  const props = defineProps<{
    item: Item
  }>()
</script>
<style lang="scss">
  aside{
    .menu{
      .item{
        display: flex;
        align-items: center;
        text-decoration: none;
        padding: 0.5rem 1.2rem;
        .text{
          display: none;
        }
        &:hover, &.router-link-exact-active{
          .material-icons, .text{
            color: greenyellow;
          }
        }
        &.router-link-exact-active{
          border-right: 5px solid  greenyellow;;
        }
      }
    }
    .item .text{
      opacity: 0;
      transition: 0.5s ease;
    }
    &.is-expanded{
      .menu{
        .item .text{
          display: flex;
          opacity: 1;
          padding-left: 1rem;
          transition: 0.5s ease;
        }
      }
    }
  }
</style>