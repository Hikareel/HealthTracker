import { useFriendsStore } from './friendsStore';
import { type FriendModel } from "../community/friendsStore";
import { defineStore } from "pinia";
import { useUserStore } from "../account/auth";

interface IMessage {
  id: number;
  text: string;
  isYours: boolean;
  isReaded: boolean;
}

interface IChat {
  messages: IMessage[];
  friendToChat: FriendModel | null;
  isChatExpanded: boolean;
  pageNumber: number;
  pageSize: number;
}

export const useChatStore = defineStore("chatData", {
  state: (): IChat => ({
    messages: [],
    friendToChat: null,
    isChatExpanded: false,
    pageNumber: 1,
    pageSize: 10,
  }),
  actions: {
    setChatData(chatData: IChat) {
      this.messages = chatData.messages;
      this.friendToChat = chatData.friendToChat;
      this.isChatExpanded = chatData.isChatExpanded,
      this.pageNumber = chatData.pageNumber;
      this.pageSize = chatData.pageSize;
    },
    setMessagesFromAPI(
      messageData: { id: number; text: string; userIdFrom: number; isReaded: boolean }[]
    ) {
      const userStore = useUserStore();
      this.messages = messageData.map(
        (message: { id: number; text: string; userIdFrom: number; isReaded: boolean }) => ({
          id: message.id,
          text: message.text,
          isYours: message.userIdFrom === userStore.userId,
          isReaded: message.isReaded
        })
      );
    },
    setMessages(messageData: IMessage[]) {
      this.messages = messageData;
    },
    addMessagesFromAPI(
      messageData: { id: number; text: string; userIdFrom: number; isReaded: boolean }[],
      userId: number | null
    ) {
      const newMessages = messageData.map(
        (message: { id: number; text: string; userIdFrom: number; isReaded: boolean }) => ({
          id: message.id,
          text: message.text,
          isYours: message.userIdFrom === userId,
          isReaded: message.isReaded
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
      const friendsStore = useFriendsStore();
      const isYours = userFrom === userId;
      this.messages.push({
        id: id,
        text: message,
        isYours: isYours,
        isReaded: isYours
      });
      if (!isYours) {
        friendsStore.incrementNewMessagesCount(userFrom);
      }
    },
    clearUserData() {
      this.messages = [];
      this.friendToChat = null;
      this.isChatExpanded = false;
      this.pageNumber = 1;
      this.pageSize = 10;
    },
    setFriendToChat(friend: FriendModel){
      this.friendToChat = friend;
    }
  },
});