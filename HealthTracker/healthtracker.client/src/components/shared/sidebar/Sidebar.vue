<template>
  <aside :class="`${is_expanded && 'is-expanded'}`">
    <div class="control">
      <div class="logo">
          <img src="../../../assets/LogoHT.png" class="logo-img" alt="Logo"/>
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
      <SidebarItem v-for="obj in Links" :item="obj"/>
    </div>
    <div class="flex"></div>
    <div class="menu">
      <SidebarItem v-for="obj in AuthLinks" :item="obj"/>
    </div>
  </aside>
</template>
<script lang="ts" setup>
import SidebarItem from "./components/SidebarItem.vue";
import {Links, AuthLinks} from "./data/sidebarLinks";
import { ref } from "vue";
const is_expanded = ref(false)
const ToggleMenu = () => {
  is_expanded.value = !is_expanded.value
}
</script>
<style lang="scss" scoped>
aside {
  position: fixed;
  display: flex;
  flex-direction: column;
  width: calc(2rem + 32px);
  min-height: 100vh;
  background-color: #aaa;
  padding: 1rem;
  transition: width 0.3s ease-out;
  z-index: 999;

  .flex{
    flex: 1 1 0;
  }

  .control{
    display: flex;
    flex-direction: column;
    .menu-toggle-wrap{
      top: 0;
      //align-self: center;
      margin-right: 0;
      //transition: 0.2s ease-out;
      .menu-toggle{
        //transition: 0.5s ease-out;
        background-color: #aaa;
        border: none;
        cursor: pointer;
        .material-icons{
          font-size: 2rem;
          color: white;
        }
      }
    }

    .logo-img{
      width: 2rem;
      margin: 0;
    }
    .logo {
      //margin-bottom: 1rem;
      text-align: center;
    }
  }

  .menu{
    margin: 0 -1rem;
  }

  &.is-expanded{
    width: 300px;
    .control{
      
      //flex-direction: row;
      //justify-content: space-between;
      //align-items: left;
      //margin-bottom: 1rem;
      .menu-toggle-wrap{
        .menu-toggle{
          transform: scaleX(-1);
          transition: 0.5s ease-out;
        }
      }
    }
    .logo{
      margin-bottom: auto;
    }
    @media (max-width: 768px){
      border-radius: 0 20px 20px 0;
    }
  }

  @media (max-width: 768px) {
    position:fixed;
    z-index: 999;

  }
}
</style>
