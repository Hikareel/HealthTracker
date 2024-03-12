<template>
  <main class="trening-planner-page">
    <h1>Look up your workouts</h1>
    <a>Add new workout</a>
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
}
</style>