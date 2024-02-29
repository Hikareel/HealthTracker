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
      <SidebarItem v-for="obj in Links" :item="obj" :key="obj.name"/>
    </div>
    <div class="flex"></div>
    <div class="menu">
      <SidebarItem v-for="obj in AuthLinks" :item="obj" :key="obj.name"/>
    </div>
  </aside>
</template>

<script lang="ts" setup>
import SidebarItem from "./components/SidebarItem.vue";
import {Links, AuthLinks} from "@/data/models/sidebarLinks";
import { ref } from "vue";
const is_expanded = ref(false)
const ToggleMenu = () => {
  is_expanded.value = !is_expanded.value
}
</script>

<style lang="scss">
@use "@/assets/sidebar";
</style>
