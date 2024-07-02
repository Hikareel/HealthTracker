import { useUserStore } from "@/store/account/auth";
import apiClient from "../axios";

interface IProfile {
  id: number;
  firstName: string;
  lastName: string;
  userName: string;
  email: string;
  phoneNumber: string;
  dateOfBirth: string;
  dateOfCreate: string;
}

async function getProfileById(userId: string) {
  const userStore = useUserStore();
  try {
    const response = await apiClient.get(`api/users/${userId}`, {
      headers: {
        Authorization: `Bearer ${userStore.token}`,
      },
    });
    const profile: IProfile = response.data;
    return profile;
  } catch (error) {
    console.error("Error fetching user:", error);
    return null;
  }
}

export type { IProfile };
export { getProfileById };
