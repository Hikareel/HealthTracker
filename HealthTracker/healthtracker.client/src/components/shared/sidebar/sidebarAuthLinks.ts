import { reactive } from "vue";

const AuthLinks = reactive({
  1: {
    name: "Register",
    link: "/register",
    icon: "person_add"
  },
  2: {
    name: "Login",
    link: "/login",
    icon: "login"
  },
  3: {
    name: "Logout",
    link: "/logout",
    icon: "logout"
  },
})
export default AuthLinks