import { reactive } from "vue";

interface IUser {
  userId: number | null;
  userName: string | null;
  firstName: string | null;
  lastName: string | null;
  email: string | null;
  loginTime: string | null;
  token: string | null;
}

const user = reactive<IUser>({
  userId: null,
  userName: null,
  firstName: null,
  lastName: null,
  email: null,
  loginTime: null,
  token: null,
});

const updateUser = () => {
  console.log("Updating user...")
  const userString = localStorage.getItem("user");
  if (userString) {
    const data = JSON.parse(userString);
    user.userId = data.userId;
    user.userName = data.userName;
    user.firstName = data.firstName;
    user.lastName = data.lastName;
    user.email = data.email;
    user.loginTime = data.loginTime;
    user.token = data.token;
  } 
  else {
    user.userId = null;
    user.userName = null;
    user.firstName = null;
    user.lastName = null;
    user.email = null;
    user.loginTime = null;
    user.token = null;
  }
};

export { user, updateUser };
