import { reactive } from "vue";

const AuthLinks = reactive({
  1: {
    name: "Register",
    link: "/register",
    icon: "app_registration"
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