import { ref } from "vue";
import type { Ref } from "vue";
import type { FriendModel } from "./friendModel";

const currentMessages: Ref<ICurrentMessages> = ref({
  messages: [],
  friendToChat: null,
  pageNumber: 1,
  pageSize: 10,
});

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

export { currentMessages };
export type { IMessage, ICurrentMessages };
