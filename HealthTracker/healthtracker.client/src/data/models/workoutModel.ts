import { reactive } from "vue";

interface IExerciseType {
  caloriesBurned: number | null,
  isTimeBased: boolean,
  name: string
}

interface IExercise {
  series: number | null,
  reps: number | null,
  duration: number | null,
  exerciseType: IExerciseType
}

interface IWorkout {
  type: string,
  duration: number | null,
  done: boolean,
  date: Date | null
  exercises: Array<IExercise>
}

const w1: IWorkout = {
  type: "Kalistenika",
  duration: 2,
  done: true,
  date: new Date("2024-03-09"),
  exercises: [
    {
      series: 4,
      reps: 8,
      duration: 0.5,
      exerciseType: {
        caloriesBurned: 500,
        isTimeBased: false,
        name: "Podciągnięcia"
      }
    },
    {
      series: 3,
      reps: 5,
      duration: null,
      exerciseType: {
        caloriesBurned: 500,
        isTimeBased: false,
        name: "MuscleUp"
      }
    },
    {
      series: 5,
      reps: 15,
      duration: null,
      exerciseType: {
        caloriesBurned: 500,
        isTimeBased: false,
        name: "Dipy"
      }
    },
    {
      series: 4,
      reps: 20,
      duration: null,
      exerciseType: {
        caloriesBurned: 500,
        isTimeBased: false,
        name: "Pompki"
      }
    },
    {
      series: 6,
      reps: 12,
      duration: null,
      exerciseType: {
        caloriesBurned: 500,
        isTimeBased: false,
        name: "Przysiady"
      }
    }
  ]
}
const w2: IWorkout = {
  type: "Full Body Workout",
  duration: 2,
  done: true,
  date: new Date("2024-03-11"),
  exercises: [
    {
      series: 4,
      reps: 8,
      duration: 0.5,
      exerciseType: {
        caloriesBurned: 500,
        isTimeBased: false,
        name: "Martwy Ciąg"
      }
    },
    {
      series: 3,
      reps: 5,
      duration: null,
      exerciseType: {
        caloriesBurned: 500,
        isTimeBased: false,
        name: "Wyciskanie na ławce"
      }
    },
    {
      series: 5,
      reps: 15,
      duration: null,
      exerciseType: {
        caloriesBurned: 500,
        isTimeBased: false,
        name: "Przysiady"
      }
    },
    {
      series: 4,
      reps: 20,
      duration: null,
      exerciseType: {
        caloriesBurned: 500,
        isTimeBased: false,
        name: "Wiosłowanie"
      }
    },
    {
      series: 6,
      reps: 12,
      duration: null,
      exerciseType: {
        caloriesBurned: 500,
        isTimeBased: false,
        name: "Biceps + Triceps"
      }
    }
  ]
}
const w3: IWorkout = {
  type: "Push Pull",
  duration: 2,
  done: false,
  date: new Date("2024-03-13"),
  exercises: [
    {
      series: 4,
      reps: 8,
      duration: 0.5,
      exerciseType: {
        caloriesBurned: 500,
        isTimeBased: false,
        name: "Przysiady"
      }
    },
    {
      series: 3,
      reps: 5,
      duration: null,
      exerciseType: {
        caloriesBurned: 500,
        isTimeBased: false,
        name: "Podciągnięcia"
      }
    },
    {
      series: 5,
      reps: 15,
      duration: null,
      exerciseType: {
        caloriesBurned: 500,
        isTimeBased: false,
        name: "Wyciskanie żołnierksie"
      }
    },
    {
      series: 4,
      reps: 20,
      duration: null,
      exerciseType: {
        caloriesBurned: 500,
        isTimeBased: false,
        name: "Wyciąg górny"
      }
    },
    {
      series: 6,
      reps: 12,
      duration: null,
      exerciseType: {
        caloriesBurned: 500,
        isTimeBased: false,
        name: "Wypychanie"
      }
    }
  ]
}

const Workouts = reactive({
  1: w1,
  2: w2,
  3: w3
})

export { Workouts }
export type { IWorkout, IExercise, IExerciseType }
