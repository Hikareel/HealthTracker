<template>
  <div v-if="isLoading" style="height: 10vh;">
    <LoadingScreen/>
  </div>
  <div v-else-if="profile != null">
    <h1>{{ profile.firstName }} {{ profile.lastName }}</h1>
    <h2>Personal information:</h2>
    <p>Email: {{ profile.email }}</p>
    <p>PhoneNumber: {{ profile.phoneNumber }}</p>
    <h2>In the HealthTracker from:</h2>
    <p>{{ formatUtcToLocal(profile.dateOfCreate) }}</p>
    <h2>Birthday:</h2>
    <p>{{ formatUtcToLocal(profile.dateOfBirth) }}</p>
    



  </div>
  <div v-else>
    <p>404 User not found!</p>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useRoute } from 'vue-router';
import { type IProfile, getProfileById } from '@/service/api/account/profileController';
import LoadingScreen from '../../../shared/LoadingScreen.vue'

const profile = ref<IProfile | null>(null);
const isLoading = ref(false);
const route = useRoute();

onMounted(async () => {
  isLoading.value = true;
  const userId = route.params.id.toString();
  const fetchedProfile = await getProfileById(userId);

  if (fetchedProfile != null) {
    profile.value = fetchedProfile;
  }
  isLoading.value = false;
});

function formatUtcToLocal(inputDate: string) {
    const date = new Date(inputDate);

    const day = date.getDate().toString().padStart(2, '0');
    const month = (date.getMonth() + 1).toString().padStart(2, '0');
    const year = date.getFullYear();

    return `${day}-${month}-${year}r.`;
}
</script>
