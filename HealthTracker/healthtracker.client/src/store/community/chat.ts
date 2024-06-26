import type { FriendModel } from "@/data/models/friendModel";
import { defineStore } from "pinia";
import { useUserStore } from "../account/auth";

interface IMessage {
  id: number;
  text: string;
  isYours: boolean;
}

interface ICurrentMessages {
  messages: IMessage[];
  friendToChat: FriendModel | null;
  pageNumber: number;
  pageSize: number;
}

export const useChatStore = defineStore("chatData", {
  state: (): ICurrentMessages => ({
    messages: [],
    friendToChat: null,
    pageNumber: 1,
    pageSize: 10,
  }),
  actions: {
    setChatData(chatData: ICurrentMessages) {
      this.messages = chatData.messages;
      this.friendToChat = chatData.friendToChat;
      this.pageNumber = chatData.pageNumber;
      this.pageSize = chatData.pageSize;
    },
    setMessagesFromAPI(
      messageData: { id: number; text: string; userIdFrom: number }[]
    ) {
      const userStore = useUserStore();
      this.messages = messageData.map(
        (message: { id: number; text: string; userIdFrom: number }) => ({
          id: message.id,
          text: message.text,
          isYours: message.userIdFrom === userStore.userId,
        })
      );
    },
    setMessages(messageData: IMessage[]) {
      this.messages = messageData;
    },
    addMessagesFromAPI(
      messageData: { id: number; text: string; userIdFrom: number }[],
      userId: number | null
    ) {
      const newMessages = messageData.map(
        (message: { id: number; text: string; userIdFrom: number }) => ({
          id: message.id,
          text: message.text,
          isYours: message.userIdFrom === userId,
        })
      ).reverse();
      this.messages = [...newMessages, ...this.messages];
    },
    addMessageFromChatHub(
      id: number,
      message: string,
      userFrom: number,
      userTo: number,
      userId: number | null
    ) {
      const isYours = userFrom === userId;
      this.messages.push({
        id: id,
        text: message,
        isYours: isYours,
      });
    },
    clearUserData() {
      this.messages = [];
      this.friendToChat = null;
      this.pageNumber = 1;
      this.pageSize = 10;
    },
    setFriendToChat(friend: FriendModel){
      this.friendToChat = friend;
    }
  },
});