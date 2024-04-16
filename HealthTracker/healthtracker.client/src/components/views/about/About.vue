<template>
  <main class="about-page">
    <div class="info">
      <div class="name">
        HealthTracker - Health and Fitness Monitoring App
      </div>
      <div class="description">
        HealthTracker is a web application designed for individuals who care about their health and physical activity.
        It allows users to monitor their daily diet, physical activities, training progress, and health status by
        tracking key indicators such as weight, blood pressure, blood sugar levels, and more.
      </div>
      <div class="features">
        <ul>
          <li>Diet Journal: Users can log their meals along with caloric and nutritional content.</li>
          <li>Workout Planner: Enables the creation of workout plans, logging physical activities, and monitoring
            progress.</li>
          <li>Health Tracking: Record key health indicators (weight, blood pressure, blood sugar) and visualize changes
            over time.</li>
          <li>Goals and Progress: Set health and fitness goals, track progress, and receive notifications about
            achievements.</li>
          <li>Community and Motivation: A forum for users to share experiences, tips, and motivational support.</li>
        </ul>

      </div>
    </div>
    <div class="authors-container">
      <div v-for="author in authors" :key="author.image" class="author">
        <AppAuthor :author="author" />
      </div>
    </div>

  </main>
</template>

<script lang="ts" setup>
import { authors } from '@/data/models/authorsData'
import AppAuthor from '../about/AppAuthor.vue'
</script>

<style lang="scss" scoped>
.about-page {
  .authors-container {
    display: flex;
    flex-direction: row;
    flex-wrap: wrap;
    justify-content: space-evenly;
    align-items: stretch;
    align-content: center;


    .author {
      padding: 0.5rem;
      border: 1px solid #eee;
    }
  }
}
</style>