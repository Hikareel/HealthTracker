import { reactive } from "vue";

const CurrentMessages = reactive([
])

interface CurrentMessagesModel {
    message: string;
    timeSend: string;
    isYours: boolean;
}

export { CurrentMessages };
export type { CurrentMessagesModel };
