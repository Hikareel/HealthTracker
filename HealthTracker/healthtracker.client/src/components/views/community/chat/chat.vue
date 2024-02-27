<template>
  <main class="chat">
    <div class="chat-header">
      <p>Friend name: </p>
    </div>
    <div class="chat-messages">
      <p>Messages</p>
    </div>
    <div class="chat-input">
      <input type="text" placeholder="Wpisz wiadomość..." />
      <button>Send</button>
    </div>
  </main>
</template>

<script lang="ts" setup>

</script>

<style>
.chat {
  display: grid;
  grid-template-columns: 1fr;
  grid-template-rows: 1fr 6fr 2fr;
  background-color: red;
  border-radius: 15px 0px 0px 15px;
}

.chat-header {
  display:inline-grid;
  color: white;
  text-align: center;
  align-items: end;
}

.chat-messages {
  overflow-y: auto;
  margin: 1rem 1rem 0rem 1rem;
  padding: 5px;
  background-color:grey;
}

.chat-input {
  display: flex;
  margin-left: 1rem;
  margin-bottom: 0.5rem;
  margin-right: 1rem;
  gap: 2px;
}

.chat-input input {
  flex-grow: 1;
  border-radius: 0px 0px 0px 15px;
}

.chat-input button {
  cursor: pointer;
  background-color: white;
  border-radius: 5px;
}
</style>
