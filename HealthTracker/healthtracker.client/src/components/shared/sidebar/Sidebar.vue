<template>
  <aside :class="`${is_expanded && 'is-expanded'}`">
    <!-- <h1 class="app_name">
      <router-link to="/">HealthTracker</router-link>
    </h1>
    <SidebarItem v-for="(link, name) in links" :name="name" :link="link"/> -->
    <div class="logo">
      <img src="../../../assets/github.png" alt="Vue"/>
    </div>
    <div class="menu-toggle-wrap">
      <button class="menu-toggle" @click="ToggleMenu">
        <span class="material-icons">
          keyboard_double_arrow_right
        </span>
      </button>
    </div>
    <div class="menu">
      <SidebarItem v-for="(link, name) in links" :name="name" :link="link"/>
    </div>
    <div class="flex"></div>
    <div class="menu">
      <SidebarItem v-for="(link, name) in authLinks" :name="name" :link="link"/>
    </div>
  </aside>
</template>
<script lang="ts" setup>
import SidebarItem from "./SidebarItem.vue";
import Links from "./sidebarLinks";
import AuthLinks from "./sidebarAuthLinks";
import { ref } from "vue";
const links = Links;
const authLinks = AuthLinks;
const is_expanded = ref(false)
const ToggleMenu = () => {
  is_expanded.value = !is_expanded.value
}
</script>
<style lang="scss" scoped>
aside {
  display: flex;
  flex-direction: column;
  width: calc(2rem + 32px);
  min-height: 100vh;
  background-color: #aaa;
  padding: 1rem;
  transition: 0.2s ease-out;

  .flex{
    flex: 1 1 0;
  }

  .menu-toggle-wrap{
    display: flex;
    justify-content: flex-end;
    margin-bottom: 1rem;
    position: relative;
    top: 0;
    transition: 0.2s ease-out;
    .menu-toggle{
      transition: 0.2s ease-out;
      .material-icons{
        font-size: 2rem;
        color: white;
      }
    }
  }

  .menu{
    margin: 0 -1rem;
  }

  &.is-expanded{
    width: 300px;
    .menu-toggle-wrap{
      .menu-toggle{
        transform: rotate(-180deg);
      }
    }
  }

  @media (max-width: 768px) {
    position:fixed;
    z-index: 999;
  }

  .logo {
    margin-bottom: 1rem;
  }
}
</style>
