import { reactive } from "vue";

//Pobierane z backendu.
const CurrentMessages = reactive([
    {
        message: "Message1 sadas dasdasd asd as da sd a da sd  d d       assssssssss",
        timeSend: "",
        isYours: true
    },
    {
        message: "bardzo długie zdanie do przetestowaniesssssssssssssssssssssssssssssssssssssssssssssssssss bardzo długie zdanie do przetestowanie bardzo długie zdanie do przetestowanie bardzo długie zdanie do przetestowanie",
        timeSend: "",
        isYours: false
    },
    {
        message: "Message3",
        timeSend: "",
        isYours: true
    },
    {
        message: "Message3",
        timeSend: "",
        isYours: true
    },
    {
        message: "Message3",
        timeSend: "",
        isYours: true
    },
    {
        message: "Message3asdasdddddddddddddddddddddd asd sssssssssssssssssssssssssssssssssssssssssssa das dasd",
        timeSend: "",
        isYours: false
    }
])

interface CurrentMessagesModel {
    message: string;
    timeSend: string;
    isYours: boolean;
}
export { CurrentMessages };
export type { CurrentMessagesModel };
