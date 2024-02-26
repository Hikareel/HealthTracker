<template>
  <main class="home-page">
    <div class="main-view">
      <RouterLink v-for="(link, index) in Object.values(Links).slice(1, -1)" :key="index" :to="link.link" class="segment">
        <HomeMainField class="home-main-field" :title="link.name" :description="formData[link.name].Description"
          :label="formData[link.name].Label" :fieldValue="formData[link.name].FieldValue" />
      </RouterLink>
    </div>
  </main>
</template>

<script lang="ts" setup>
import { ref, computed } from 'vue';
import HomeMainField from './components/HomeMainField.vue';
import { Links } from '../../shared/sidebar/data/sidebarLinks';
import type { HomeFieldPassModel } from './data/homeFieldPassDataModel';

const formData = ref<Record<string, HomeFieldPassModel>>({});

const initializedFormData = computed(() => {
  const slicedLinks = Object.values(Links).slice(1, -1);
  const newFormData: Record<string, HomeFieldPassModel> = {};
  slicedLinks.forEach(link => {
    newFormData[link.name] = {
      Title: link.name,
      Description: getDescription(link.name),
      Label: getLabel(link.name),
      FieldValue: getValues(link.name)
    };
  });
  return newFormData;
});

formData.value = initializedFormData.value;

//trzeba będzie pobierać z API
function getDescription(name: string) {
  switch (name) {

    case 'Meals':
      return 'Users can record their meals along with calorie and nutritional composition.'
    case 'Trainings Planner':
      return 'Ability to create training plans, record physical activity and monitor progress.'
    case 'Health Check':
      return 'Record key health indicators (weight, blood pressure, sugar levels) and visualize changes over time.'
    case 'Goals and Progress':
      return 'Set health and fitness goals, track progress, receive notifications of achievements.'
    case 'Community':
      return 'A forum for users to share experiences, advice and motivational support.'
    default:
      return '--'
  }
}

function getLabel(name: string) {
  return 'label'
}

function getValues(name: string) {
  return ['val1', 'val2']
}
</script>


<style scope>
.segment {
  margin: 1rem;
}

.home-main-field:hover {
  cursor: pointer;
}

.home-main-field:click {
  cursor: progress;
}

.segment:hover {
  background-color: transparent;
  text-decoration: none;
}

.main-view {
  display: flex;
  flex-wrap: wrap;
  text-align: center;
  justify-content: center;
  align-items: center;
  margin-top: 1rem;
}

@media (max-width: 600px) {
  .main-view {
    flex-direction: column;
  }
}

.home-page {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 100%;
  width: 100%;
}
</style>