<template>
  <main>
    <div class="workout" v-for="workout in Workouts">
      <h1 class="title">{{ workout.type  }}</h1>
      <hr>
      <h3 class="date" v-if="workout.date">
        {{ (workout.done ? workoutDateLabel[0] : workoutDateLabel[1]) + workout.date.toLocaleDateString() }}
      </h3>
      <h3 class="missingDate" v-else>Uzupełnij datę treningu</h3>
      <p class="time">Workout time: {{ workout.duration  }} hours</p>
      <h3 class="exerciseList">Exercises:</h3>
      <div class="exercise" v-for="exercise in workout.exercises">
        <p>{{ exercise.exerciseType.name  }}</p>
        <p>{{ exercise.series }} / {{ exercise.reps }}</p>
      </div>
    </div>
  </main>
</template>

<script setup lang="ts">
import { Workouts } from '@/data/models/workoutModel'
const workoutDateLabel = [
  "Done at: ",
  "Planned at: "
]
</script>

<style lang="scss" scoped>
main{
  display: flex;
  text-align: center;
  gap: 20px;
  min-width: 320px;
  flex-wrap: wrap;
  margin: 1rem;
  justify-content: center;
  .workout{
    display: flex;
    flex-direction: column;
    width: 320px;
    padding: 1rem;
    gap: 0.25rem;
    border: 1px solid orange;
    border-radius: 2rem;
    .title{
      text-wrap: pretty;
    }
    .missingDate{
      color: $error-color;
    }
    .exercise{
      border-bottom: 0.5px dashed;
      display: flex;
      flex-direction: row;
      justify-content:space-between;
    }
  }
  @media (max-width: 768px) {
    flex-direction: column;
  }
}
</style>