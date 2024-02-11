<template>
  <aside :class="`${is_expanded && 'is-expanded'}`">
    <div class="control">
      <div class="logo">
          <img src="../../../assets/github.png" alt="Logo"/>
      </div>
      <div class="menu-toggle-wrap">
        <button class="menu-toggle" @click="ToggleMenu">
          <span class="material-icons">
            keyboard_double_arrow_right
          </span>
        </button>
      </div>
    </div>
    <div class="menu">
      <SidebarItem v-for="obj in Links" v-bind="obj"/>
    </div>
    <div class="flex"></div>
    <div class="menu">
      <SidebarItem v-for="obj in AuthLinks" v-bind="obj"/>
    </div>
  </aside>
</template>
<script lang="ts" setup>
import SidebarItem from "./SidebarItem.vue";
import Links from "./sidebarLinks";
import AuthLinks from "./sidebarAuthLinks";
import { ref } from "vue";
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

  .control{
    display: flex;
    flex-direction: column;
    .menu-toggle-wrap{
      top: 0;
      align-self: center;
      margin-right: 0;
      transition: 0.2s ease-out;
      .menu-toggle{
        transition: 0.5s ease-out;
        background-color: #aaa;
        border: none;
        cursor: pointer;
        .material-icons{
          font-size: 2rem;
          color: white;
        }
      }
    }
    .logo {
      margin-bottom: 1rem;
    }
  }

  .menu{
    margin: 0 -1rem;
  }

  &.is-expanded{
    width: 300px;
    .control{
      flex-direction: row;
      justify-content: space-between;
      .menu-toggle-wrap{
        justify-items: flex-end;
        transition: 0.5s ease-out;
        .menu-toggle{
          // transform: rotate(-180deg);
          transform: scaleX(-1);
          transition: 0.5s ease-out;
        }
      }
    }
  }

  @media (max-width: 768px) {
    position:fixed;
    z-index: 999;
    &.is-expanded{
      width: 250px;
    }

  }
}
</style>
