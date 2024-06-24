import { reactive, computed } from "vue";
import { useUserStore } from "@/store/account/auth";

const createLink = (name: string, link: string, icon: string, isAuthRequired: boolean) => {
  return computed(() => {
    const userStore = useUserStore();
    return {
      name: name,
      link: link,
      icon: icon,
      isHidden: isAuthRequired ? !userStore.token : userStore.token && !(name === "Home" || name === "About")
    };
  });
};

const getLinks = () => {
  return reactive({
    home: createLink("Home", "/", "home", false),
    meals: createLink("Meals", "/diary", "restaurant", true),
    trainingPlaner: createLink("Trainings Planner", "/planner", "fitness_center", true),
    health: createLink("Health Check", "/health", "health_and_safety", true),
    goals: createLink("Goals and Progress", "/goals", "emoji_events", true),
    community: createLink("Community", "/community", "groups", true),
    about: createLink("About", "/about", "info", false)
  });
};

const getAuthLinks = () => {
  return reactive({
    register: createLink("Register", "/register", "person_add", false),
    login: createLink("Login", "/login", "login", false),
    logout: createLink("Logout", "/logout", "logout", true)
  });
};

export { getLinks, getAuthLinks };
