import { reactive, computed } from "vue";
import { user } from "../service/userData"

const createLink = (name: string, link: string, icon: string, isAuthRequired: boolean) => {
  return computed(() => ({
    name: name,
    link: link,
    icon: icon,
    isHidden: isAuthRequired ? !user.token : user.token && !(name === "Home" || name === "About")
  }));
};

const Links = reactive({
  home: createLink("Home", "/", "home", false),
  meals: createLink("Meals", "/diary", "restaurant", true),
  trainingPlaner: createLink("Trainings Planner", "/planner", "fitness_center", true),
  health: createLink("Health Check", "/health", "health_and_safety", true),
  goals: createLink("Goals and Progress", "/goals", "emoji_events", true),
  community: createLink("Community", "/community", "groups", true),
  about: createLink("About", "/about", "info", false)
});

const AuthLinks = reactive({
  register: createLink("Register", "/register", "person_add", false),
  login: createLink("Login", "/login", "login", false),
  logout: createLink("Logout", "/logout", "logout", true)
});

interface ILinkItem{
  name: String,
  link: String,
  icon: String,
  isHidden: boolean
}

export { Links, AuthLinks };
export type { ILinkItem };
