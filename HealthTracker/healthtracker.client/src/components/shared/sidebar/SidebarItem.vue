<template>
  <div class="sidebar_item">
    <p>
      <router-link class="item" :to="link">
        <span class="material-icons">{{ icon }}</span>
        <span class="text">{{ name }}</span>
      </router-link>
    </p>
  </div>
</template>
<script lang="ts">
  export default{
    props:{
      name: {
        type: String,
        required: true
      },
      link:{
        type: String,
        required: true
      },
      icon:{
        type: String,
        required: true
      }
    }
  }
</script>
<style lang="scss">
  aside{
    .menu{
      .item{
        display: flex;
        align-items: center;
        text-decoration: none;
        padding: 0.5rem 1rem;
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