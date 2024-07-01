import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { useUserStore } from "@/store/account/auth";
import { useChatStore } from "@/store/community/chatStore";

let connection: HubConnection | null = null;

function getConnection(){
  const userStore = useUserStore();
  if (connection != null) {
    return connection;
  }
  connection = new HubConnectionBuilder()
  .withUrl("https://localhost:7170/chatHub", {
    accessTokenFactory: () => userStore.token ?? "",
  })
  .build();
  
  return connection;
}

async function connectToChatHub() {
  const chatStore = useChatStore();
  const userStore = useUserStore();

  if (!userStore.userId || !userStore.token) {
    return;
  }

  try {  
    const connection = getConnection();
    await connection.start();
    console.log("Connected to Chat");
    connection.on("ReceiveMessage", (id, userFrom, userTo, message) => {
      chatStore.addMessageFromChatHub(
        id,
        message,
        userFrom,
        userTo,
        userStore.userId
      );
    });
  } catch (err) {
    console.error("Error connecting to Chat:", err);
  }
}

async function sendMesssage(messageToSend: string){
  const userStore = useUserStore();
  const chatStore = useChatStore();
  const connection = getConnection();
  
  if (chatStore.friendToChat != null){
    await connection.invoke("SendMessageToUser", userStore.userId, chatStore.friendToChat.userId, messageToSend);
  }
}

export { connectToChatHub, sendMesssage };