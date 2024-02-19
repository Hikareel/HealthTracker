import { reactive } from "vue";

const Links = reactive({
  1: {
    name: "Home",
    link: "/",
    icon: "home"
  },
  2: {
    name: "Meals",
    link: "/diary",
    icon: "restaurant"
  },
  3: {
    name: "Trainings Planner",
    link: "/planner",
    icon: "fitness_center"
  } ,
  4: {
    name: "Health Check",
    link: "/health",
    icon: "health_and_safety"
  },
  5: {
    name: "Goals and Progress",
    link: "/goals",
    icon: "emoji_events"
  },
  6: {
    name: "Community",
    link: "/community",
    icon: "groups"
  },
  7: {
    name: "About",
    link: "/about",
    icon: "info"
  }
})

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

interface LinkItem{
  name: String,
  link: String,
  icon: String
}
export { Links, AuthLinks };
export type { LinkItem };
