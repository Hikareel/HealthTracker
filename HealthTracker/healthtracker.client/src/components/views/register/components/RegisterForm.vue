<template>
    <Vueform id="form" method="POST" endpoint="/send" ref="form$">
      <GroupElement name="" before="Name">
        <TextElement name="first_name" placeholder="First Name"/>
        <TextElement name="last_name" placeholder="Last Name" />
      </GroupElement>
      <GroupElement name="">
        <TextElement name="email" label="Email" placeholder="user@domain.com" input-type="email"/>
        <TextElement name="phone" label="Phone nr." placeholder="123456789" input-type="tel"/>
      </GroupElement>
      <GroupElement name="">
        <DateElement name="date" label="Birth Date" display-format="MMMM DD, YYYY"/>
      </GroupElement>
      <GroupElement name="">
        <TextElement name="password" label="Password" input-type="password"/>
        <TextElement name="password_confirm" label="Confirm Password" input-type="password"/>
      </GroupElement>
      <ButtonElement 
        name="submit" 
        button-label="Register"
        :submits="true"
        align="center"/>
    </Vueform>
</template>

<script setup lang="ts">
import { ref, onMounted, nextTick } from 'vue'
import type { RegisterModel } from '../data/registerDataModel'
import type { VueformComponent } from '@vueform/vueform';
const form$ = ref<VueformComponent | null>(null)

</script>

<style scoped>
  #form{
    display: flex;
    flex-direction: column;
    width: fit-content;
    margin: auto;
  }
</style>