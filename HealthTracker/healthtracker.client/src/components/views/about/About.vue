<template>
  <main class="about-page">
    <div class="info">
      <div class="name">
        HealthTracker - Health and Fitness Monitoring
      </div>
      <div class="description">
        <p>&emsp;HealthTracker is a web application designed for individuals who care about their health and physical
          activity.
          It allows users to monitor their daily diet, physical activities, training progress, and health status by
          tracking key indicators such as weight, blood pressure, blood sugar levels, and more.</p>
      </div>
      <h1><i class="bi bi-caret-down-fill"></i>&ensp;Features&ensp;<i class="bi bi-caret-down-fill"></i></h1>
      <div class="features">
        <ul>
          <li><span class="feature-title">Diet Journal:</span> Users can log their meals along with caloric and
            nutritional content.</li>
          <li><span class="feature-title">Workout Planner:</span> Enables the creation of workout plans, logging
            physical activities, and
            monitoring
            progress.</li>
          <li><span class="feature-title">Health Tracking:</span> Record key health indicators (weight, blood pressure,
            blood sugar)
            and visualize changes
            over time.</li>
          <li><span class="feature-title">Goals and Progress:</span> Set health and fitness goals, track progress, and
            receive
            notifications about
            achievements.</li>
          <li><span class="feature-title">Community and Motivation:</span> A forum for users to share experiences,
            tips, and
            motivational support.</li>
        </ul>

      </div>
    </div>
    <h1><i class="bi bi-caret-down-fill"></i>&ensp;Authors&ensp;<i class="bi bi-caret-down-fill"></i></h1>
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
h1 {
  display: flex;
  justify-content: center;
  font-size: xx-large;
  margin-top: 1rem;
}

.about-page {
  color: white;
  padding: 1rem;

  .info {
    display: flex;
    flex-direction: column;
    gap: 1rem;
    margin-bottom: 2rem;

    .name {
      display: flex;
      justify-content: center;
      font-size: xx-large;
    }

    .description {
      font-size: large;
    }

    .features {
      .feature-title {
        color: coral;
        font-size: large;
        font-weight: 600;
      }
    }
  }

  .authors-container {
    margin-top: 1rem;
    display: flex;
    flex-direction: row;
    flex-wrap: wrap;
    justify-content: space-evenly;
    align-items: stretch;
    align-content: center;

    .author {
      background-color: rgba(139, 139, 139, 0.164);
      padding: 1rem 1.5rem 1rem 1.5rem;
      border-radius: 4% 0 0 0;
      border-right: 5px inset rgba(139, 139, 139, 0.575);
      border-bottom: 5px outset rgba(139, 139, 139, 0.589);
      -webkit-box-shadow: 5px 5px 10px 0px rgba(139, 139, 139, 0.9);
      -moz-box-shadow: 5px 5px 10px 0px rgba(139, 139, 139, 0.9);
      box-shadow: 5px 5px 10px 0px rgba(139, 139, 139, 0.9);
      width: 30vw;
    }
  }

  @media (max-height: 590px),
  (max-width: 785px) {
    .authors-container {
      flex-direction: column;
      gap: 2rem;
    }
  }
}
</style>