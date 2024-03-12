<template>
  <main class="form-page">
    <div class="form">
      <FormStatus formTitle="Add New Workout"/>
      <Vueform class="form-content" @submit="" :float-placeholders="false" :endpoint="false"
        :display-errors="false" sync>
        <GroupElement name="basicInfo" before="Basic Info">
          <TextElement :addons="{
            before: `<i class='bi bi-pencil-fill'></i>`
          }" name="name" placeholder="Name" rules="required|max:100|min:3" :columns="{ xs: 12, sm: 6 }" />
          <TextElement :addons="{
            before: `<i class='bi bi-pencil-fill'></i>`
          }" name="duration" placeholder="hours" rules="required|max:100|min:3" :columns="{ xs: 12, sm: 6 }" />
          <DateElement name="date" label="Date" placeholder="January 01, 2000" display-format="MMMM DD, YYYY"
          rules="before:today" :addons="{
            before: `<i class='bi bi-calendar3'></i>`
          }" />
          <ToggleElement name="done" :labels="{ on: 'Yes', off: 'No' }" align="right" label="Workout done?"
          :columns="{ container: 12, label: 5, wrapper: 12 }"/>
        </GroupElement>
        <GroupElement name="list">
          <ListElement name="exercises">
            <template #default="{ index }">
              <ObjectElement :name="index" :before="'Exercise ' + (index+1)">
                <TextElement :addons="{
                  before: `<i class='bi bi-pencil-fill'></i>`
                }" name="series" placeholder="Series" rules="max:100" :columns="{ xs: 12, sm: 6 }" />
                <TextElement :addons="{
                  before: `<i class='bi bi-pencil-fill'></i>`
                }" name="reps" placeholder="Reps" rules="max:100" :columns="{ xs: 12, sm: 6 }" />
                <TextElement :addons="{
                  before: `<i class='bi bi-pencil-fill'></i>`
                }" name="duration" placeholder="duration" rules="max:100" :columns="{ xs: 12, sm: 6 }" />
              </ObjectElement>
            </template>
          </ListElement>
        </GroupElement>
        <GroupElement name="controll">
          <ButtonElement id="reset_button" name="reset" button-label="<i class='bi bi-arrow-clockwise'></i>" type="reset"
            align="left" :danger="true" :resets="true" size="lg" :columns="{
              container: 6,
              wrapper: 6
            }" />
          <ButtonElement name="submit" button-label="Add" align="right" :submits="true" full size="lg" :columns="{
            container: 6,
            label: 6
          }" />
        </GroupElement>
      </Vueform>
    </div>
  </main>
</template>

<script setup lang="ts">
import FormStatus from '@/components/shared/FormStatus.vue'
</script>

<style lang="scss" scoped>
@use '@/assets/form';
</style>