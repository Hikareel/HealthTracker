<template>
  <main class="chat">
    <div class="content" :class="`${is_expanded && 'is-expanded'}`">
      <div class="chat-header">
        <div class="menu-toggle-wrap">
          <button class="menu-toggle" @click="ToggleMenu">
            <span class="material-icons">
              keyboard_double_arrow_right
            </span>
          </button>
        </div>
        <div class="chat-header-label">
          <p>Friend</p>
        </div>
      </div>
      <ChatBox />
    </div>
  </main>
</template>

<script lang="ts" setup>
import { ref } from "vue";
import ChatBox from './ChatBox.vue'
const is_expanded = ref(false)
const ToggleMenu = () => {
  is_expanded.value = !is_expanded.value
}
</script>

<style lang="scss">
.chat {
  display: grid;
  grid-template-columns: 1fr;
  grid-template-rows: 1fr 6fr 2fr;
  background-color: rgb(168, 124, 124);
  border-radius: 15px 0px 0px 15px;
  .content {
    background-color: rgb(168, 124, 124);
    position: fixed;
    right: calc(-25vw + 4rem);
    width: 25vw;
    height: 20rem;
    transition: right 0.5s ease-out;
    border-radius: 1rem 0% 0% 1rem;
    .chat-header {
      height: 3rem;
      display: grid;
      grid-template-columns: 4rem 1fr;
      grid-template-rows: 2rem;
      .menu-toggle-wrap {
        top: 0;
        .menu-toggle {
          padding: 1rem;
          background-color: rgba(0, 0, 0, 0);
          border: none;
          cursor: pointer;
          transition: 0.5s ease-out;
          transform: scaleX(-1);
          .material-icons {
            font-size: 2rem;
            color: white;
          }
        }
      }
      .chat-header-label {
        display: flex;
        align-items: center;
        height: 4rem;
        font-size: 2rem;
        color: white;
      }
    }
    .chat-content {
      display: grid;
      grid-template-columns: 4rem 1fr;
      transition: 0.5s ease-out;
      .notification {
        display: flex;
        justify-content: center;
        align-items: center;
        writing-mode: vertical-rl;
        grid-column: 1;
        opacity: 1;
        visibility: visible;
        width: 4rem;
        transition: width 0.5s ease-out, opacity 0.5s ease-out;
      }
      .chat-messinput {
        grid-column: 2 / 3;
        padding-left: 1rem;
        padding-top: 1rem;
        .chat-messages {
          overflow-y: auto;
          scrollbar-width: thin;
          scrollbar-color: #888 #f0f0f0;
          height: 11rem;
          padding: 5px;
          border-radius: 10px 0 0 0;
          background-color: rgb(199, 167, 167);
          transition: 0.5s ease-out;
          &::-webkit-scrollbar {
            width: 8px;
          }
          &::-webkit-scrollbar-track {
            background: #f0f0f0;
          }
          &::-webkit-scrollbar-thumb {
            background: #888;
          }
          &::-webkit-scrollbar-thumb:hover {
            background: #555;
          }
          .message {
            display: flex;
            overflow-wrap: anywhere;
            margin-bottom: 10px;
            padding: 8px;
            border-radius: 10px;
            max-width: 60%;
            width: fit-content;
            color: black;
          }
          
          .own-message {
            justify-content: end;
            margin-left: auto;
            background-color: #dcf8c6;
          }
          
          .received-message {
            justify-content: start;
            background-color: #fff;
          }
        }
        .chat-input {
          background-color: rgb(199, 167, 167);
          display: flex;
          height: 2rem;
          gap: 2px;
          border-radius: 10px;
          button {
            cursor: pointer;
            background-color: rgb(199, 167, 167);
            border: none;
            width: 2rem;
            border-radius: 0 0 0 10px;
          }
          input {
            width: 100%;
            background-color: rgb(199, 167, 167);
            border: none;
            &:hover {
              border: 1px solid rgb(0, 182, 206);
            }
          }
        }
      }
    }
    &.is-expanded {
      right: 0;
      .chat-header{
        .menu-toggle-wrap{
          .menu-toggle {
            transform: scaleX(1);
            transition: 0.5s ease-out;
          }
        }
      }
      .chat-content {
        grid-template-columns: 0 1fr;
        .notification {
          transform: translateX(-100%);
          transition: width 0.5s ease-out, opacity 0.5s ease-out;
          transition: width 2.5s ease, opacity 2.5s ease;
          opacity: 0;
          visibility: hidden;
          width: 0;
        }
      }
    }
  }
}
</style>
