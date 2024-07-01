import { useUserStore } from "@/store/account/auth";
import apiClient from "../axios";

const getFriendList = async () => {
  const userStore = useUserStore();
  const response = await apiClient
    .get(`/api/users/${userStore.userId}/friends`, {
      headers: {
        Authorization: `Bearer ${userStore.token}`,
      },
    })
    .catch((error) => {
      console.log(error);
      return null;
    });

  return response?.data;
};

export { getFriendList };
