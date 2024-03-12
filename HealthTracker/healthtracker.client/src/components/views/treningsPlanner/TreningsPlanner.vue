<template>
  <main class="trening-planner-page">
    <h1>Look up your workouts</h1>
    <RouterLink to="/planner/add-workout" class="addWorkoutButton">
      Add new workout
    </RouterLink>
    <WorkoutsList />
  </main>
</template>

<script lang="ts" setup>
import WorkoutsList from './components/WorkoutsList.vue'
</script>

<style lang="scss" scoped>
.trening-planner-page{
  display: flex;
  flex-direction: column;
  gap: 20px;
  align-items: center;
  .addWorkoutButton{
    text-decoration: none;
    color: black;
    background-color: lightgreen;
    padding: 0.5rem;
    border: 1px solid lightgreen;
    border-radius: 1rem;
  }
}
</style>