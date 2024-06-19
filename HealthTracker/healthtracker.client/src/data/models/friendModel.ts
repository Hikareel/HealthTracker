import { ref } from "vue";

interface FriendModel {
    userId: number;
    firstName: string;
    lastName: string;
}

const friends = ref<FriendModel[]>([]);

export { friends }
export type { FriendModel };
