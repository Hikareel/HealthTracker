import { reactive } from "vue";

const Links = reactive({
  1: {
    name: "Meals",
    link: "/diary",
    icon: "restaurant"
  },
  2: {
    name: "Trainings Planner",
    link: "/planner",
    icon: "fitness_center"
  } ,
  3: {
    name: "Health Check",
    link: "/health",
    icon: "health_and_safety"
  },
  4: {
    name: "Goals and Progress",
    link: "/goals",
    icon: "emoji_events"
  },
  5: {
    name: "Community",
    link: "/community",
    icon: "forum"
  },
  6: {
    name: "About",
    link: "/about",
    icon: "info"
  }
})
export default Links