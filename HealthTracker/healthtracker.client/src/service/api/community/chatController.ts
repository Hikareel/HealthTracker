import { useUserStore } from "@/store/account/auth";
import apiClient from "../axios";

const getMessagesWithFriend = async (
    friendId: number | null,
    pageNumber: number,
    pageSize: number
) => {
  const userStore = useUserStore();
  const response = await apiClient.get(`/api/users/messages/${userStore.userId}/${friendId}/`, {
    headers: {
      'Authorization': `Bearer ${userStore.token}`
    }, 
    params: {
      pageNumber: pageNumber,
      pageSize: pageSize
    }
  })
    .catch((error) => {
      console.log(error);
      return null;
    });
  return response?.data;
};

export { getMessagesWithFriend };
