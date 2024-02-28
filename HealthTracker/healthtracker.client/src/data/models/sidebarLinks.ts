import { reactive } from "vue";

const Links = reactive({
  1: {
    name: "Home",
    link: "/",
    icon: "home",
    isHidden: false
  },
  2: {
    name: "Meals",
    link: "/diary",
    icon: "restaurant",
    isHidden: localStorage.getItem("token") ? false : true
  },
  3: {
    name: "Trainings Planner",
    link: "/planner",
    icon: "fitness_center",
    isHidden: localStorage.getItem("token") ? false : true
  } ,
  4: {
    name: "Health Check",
    link: "/health",
    icon: "health_and_safety",
    isHidden: localStorage.getItem("token") ? false : true
  },
  5: {
    name: "Goals and Progress",
    link: "/goals",
    icon: "emoji_events",
    isHidden: localStorage.getItem("token") ? false : true
  },
  6: {
    name: "Community",
    link: "/community",
    icon: "groups",
    isHidden: localStorage.getItem("token") ? false : true
  },
  7: {
    name: "About",
    link: "/about",
    icon: "info",
    isHidden: false
  }
})

const AuthLinks = reactive({
  1: {
    name: "Register",
    link: "/register",
    icon: "person_add",
    isHidden: localStorage.getItem("token") ? true : false
  },
  2: {
    name: "Login",
    link: "/login",
    icon: "login",
    isHidden: localStorage.getItem("token") ? true : false
  },
  3: {
    name: "Logout",
    link: "/logout",
    icon: "logout",
    isHidden: localStorage.getItem("token") ? false : true
  },
})

interface LinkItem{
  name: String,
  link: String,
  icon: String,
  isHidden: boolean
}
export { Links, AuthLinks };
export type { LinkItem };
