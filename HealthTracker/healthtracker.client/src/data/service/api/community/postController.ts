import { useUserStore } from "@/store/account/auth";
import apiClient from "../axios";

const getPostOnWall = async (pageNumber: number, pageSize: number) => {
  const userStore = useUserStore();
  if (!userStore.userId) {
    console.log("No user ID provided");
    return null;
  }

  try {
    const response = await apiClient.get(
      `/api/users/${userStore.userId}/wall/posts`,
      {
        headers: {
          Authorization: `Bearer ${userStore.token}`,
        },
        params: {
          pageNumber: pageNumber,
          pageSize: pageSize,
        },
      }
    );
    return response.data;
  } catch (error) {
    console.error("Error fetching posts:", error);
    return null;
  }
};

const getPostComments = async (
  postId: number,
  pageNumber: number,
  pageSize: number
) => {
  const userStore = useUserStore();
  try {
    if (!postId) {
      return;
    }
    const response = await apiClient.get(
      `/api/users/posts/${postId}/comments`,
      {
        headers: {
          Authorization: `Bearer ${userStore.token}`,
        },
        params: {
          pageNumber: pageNumber,
          pageSize: pageSize,
        },
      }
    );
    return response.data;
  } catch (error) {
    console.error(error);
  }
};

export { getPostOnWall, getPostComments };
