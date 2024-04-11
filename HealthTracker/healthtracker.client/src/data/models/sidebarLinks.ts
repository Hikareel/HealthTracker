import { reactive } from "vue";
import { user } from "../service/userData"

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
    isHidden: user.token ? false : true
  },
  3: {
    name: "Trainings Planner",
    link: "/planner",
    icon: "fitness_center",
    isHidden: user.token ? false : true
  } ,
  4: {
    name: "Health Check",
    link: "/health",
    icon: "health_and_safety",
    isHidden: user.token ? false : true
  },
  5: {
    name: "Goals and Progress",
    link: "/goals",
    icon: "emoji_events",
    isHidden: user.token ? false : true
  },
  6: {
    name: "Community",
    link: "/community",
    icon: "groups",
    isHidden: user.token ? false : true
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
    isHidden: user.token ? false : true
  },
  2: {
    name: "Login",
    link: "/login",
    icon: "login",
    isHidden: user.token ? false : true
  },
  3: {
    name: "Logout",
    link: "/logout",
    icon: "logout",
    isHidden: user.token ? false : true
  },
})

interface ILinkItem{
  name: String,
  link: String,
  icon: String,
  isHidden: boolean
}
export { Links, AuthLinks };
export type { ILinkItem };
