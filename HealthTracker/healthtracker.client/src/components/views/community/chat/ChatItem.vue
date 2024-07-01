<template>
	<main class="chat">
		<div class="content" :class="`${chatStore.isChatExpanded && 'is-expanded'}`">
			<div class="chat-header">
				<div class="menu-toggle-wrap">
					<button class="menu-toggle" @click="toggleMenu">
						<span class="material-icons">
							keyboard_double_arrow_right
						</span>
					</button>
				</div>
				<div class="chat-header-label">
					<p v-if="chatStore.friendToChat">{{ chatStore.friendToChat.firstName }} {{
			chatStore.friendToChat.lastName }}</p>
				</div>
			</div>
			<div class="chat-content">
				<div class="notification">
					<p>{{ notificationLabel }}</p>
				</div>
				<ChatBox />
			</div>

		</div>
	</main>
</template>

<script lang="ts" setup>
import { computed } from "vue";
import ChatBox from './ChatBox.vue'
import { useChatStore } from "@/store/community/chatStore";

const chatStore = useChatStore();

const notificationLabel = computed(() => {
	const count = chatStore.friendToChat?.newMessagesCount;
	return count > 1 ? `${count} new messages` :
		count === 1 ? "New message" : "";
});

function toggleMenu() {
	chatStore.isChatExpanded = !chatStore.isChatExpanded;
}

</script>

<style lang="scss">
@use '@/assets/community/chat';
</style>
