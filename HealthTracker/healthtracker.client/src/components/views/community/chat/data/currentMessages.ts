import { reactive } from "vue";

//Pobierane z backendu.
const CurrentMessages = reactive([
    {
        message: "Message1",
        timeSend: "",
        isYours: true
    },
    {
        message: "Message2",
        timeSend: "",
        isYours: false
    },
    {
        message: "Message3",
        timeSend: "",
        isYours: true
    }
])

interface CurrentMessagesModel {
    message: string;
    timeSend: string;
    isYours: boolean;
}
export { CurrentMessages };
export type { CurrentMessagesModel };
