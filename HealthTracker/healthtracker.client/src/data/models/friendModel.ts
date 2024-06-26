import { ref } from "vue";

interface FriendModel {
    userId: number;
    firstName: string;
    lastName: string;
    newMessagesCount: number;
}

const friends = ref<FriendModel[]>([]);

export { friends }
export type { FriendModel };
