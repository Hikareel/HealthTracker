import { reactive } from "vue";

//Label do wybrania i fieldValue ściągane po API z backendu.
const HomeCardData = reactive([
  {
    name: "Meals",
    link: "/diary",
    description: "Users can record their meals along with calorie and nutritional composition.",
    label:'label',
    fieldValue: ['val1','val2']
  },
  {
    name: "Trainings Planner",
    link: "/planner",
    description: "Ability to create training plans, record physical activity and monitor progress.",
    label:'label',
    fieldValue: ['val1','val2']
  } ,
  {
    name: "Health Check",
    link: "/health",
    description: "Record key health indicators (weight, blood pressure, sugar levels) and visualize changes over time.",
    label:'label',
    fieldValue: ['val1','val2']
  },
  {
    name: "Goals and Progress",
    link: "/goals",
    description: "Set health and fitness goals, track progress, receive notifications of achievements.",
    label:'label',
    fieldValue: ['val1','val2']
  },
  {
    name: "Community",
    link: "/community",
    description: "A forum for users to share experiences, advice and motivational support.",
    label:'label',
    fieldValue: ['val1','val2']
  },
])

interface HomeCardModel {
  name: string;
  description: string;
  label: string;
  fieldValue: string[],
}
export { HomeCardData };
export type { HomeCardModel };
