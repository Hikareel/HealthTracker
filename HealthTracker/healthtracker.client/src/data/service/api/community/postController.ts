import { user } from "@/data/service/userData";
import axios from "axios";

const getPostOnWall = async (pageNumber: number, pageSize: number) => {
  if (!user.userId) {
    console.log("No user ID provided");
    return null;
  }

  try {
    const response = await axios.get(
      `https://localhost:7170/api/users/${user.userId}/wall/posts`,
      {
        headers: {
          Authorization: `Bearer ${user.token}`,
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
  try {
    if (!postId) {
      return;
    }
    const response = await axios.get(
      `https://localhost:7170/api/users/posts/${postId}/comments`,
      {
        headers: {
          Authorization: `Bearer ${user.token}`,
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
