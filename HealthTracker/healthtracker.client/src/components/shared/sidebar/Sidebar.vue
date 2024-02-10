<template>
  <aside>
    <SidebarItem :content="'Dziennik'"/>
    <SidebarItem :content="'Planer Treningów'"/>
    <SidebarItem :content="'Śledzenie Zdrowia'"/>
    <SidebarItem :content="'Cele i Postepy'"/>
    <SidebarItem :content="'Spoczność i Motywacja'"/>
    <SidebarItem :content="'Zarejestruj'"/>
    <SidebarItem :content="'Zaloguj'"/>
  </aside>
</template>
<script>
import SidebarItem from './SidebarItem.vue'
export default {
  components:{
    SidebarItem,
  }
}
</script>
<style lang="scss">
  aside{
    display: flex;
    flex-direction: column;
    max-width: 300px;
    overflow: hidden;
    min-height: 100vh;
    background-color: #aaa;
    padding: 1rem;
    gap: 20px;
  }
</style>