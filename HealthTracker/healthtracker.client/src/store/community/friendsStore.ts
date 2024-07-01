import { defineStore } from "pinia";

interface FriendModel {
  userId: number;
  firstName: string;
  lastName: string;
  newMessagesCount: number;
}

export const useFriendsStore = defineStore("friendData", {
  state: () => ({
    friends: [] as FriendModel[],
  }),
  actions: {
    setFriends(friendsData: FriendModel[]) {
      this.friends = friendsData;
    },
    addFriend(friendData: FriendModel) {
      this.friends.push(friendData);
    },
    setNewMessagesCount(userId: number, count: number) {
      const friend = this.friends.find((f) => f.userId === userId);
      if (friend) {
        friend.newMessagesCount = count;
      }
    },
    resetNewMessagesCount(userId: number) {
      const friend = this.friends.find((f) => f.userId === userId);
      if (friend) {
        friend.newMessagesCount = 0;
      }
    },
    incrementNewMessagesCount(userId: number) {
      const friend = this.friends.find((f) => f.userId === userId);
      if (friend) {
        friend.newMessagesCount++;
      }
    },
  },
});

export type { FriendModel };
