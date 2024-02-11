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
    icon: "forum"
  },
  7: {
    name: "About",
    link: "/about",
    icon: "info"
  }
})
export default Links