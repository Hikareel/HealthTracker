<template>
  <div class="form" :class="{ 'is-registering': isRegistering }">
    <FormStatus formTitle="Registration" />
    <Vueform class="form-content" v-model="formData" @submit="sendFormData" :float-placeholders="false"
      :endpoint="false" :display-errors="false" sync>
      <GroupElement name="name" before="Name">
        <TextElement :addons="{
    before: `<i class='bi bi-pencil-fill'></i>`
  }" name="FirstName" placeholder="First Name" rules="required|max:100|min:3" :columns="{ xs: 12, sm: 6 }" />
        <TextElement :addons="{
    before: `<i class='bi bi-pencil-fill'></i>`
  }" name="LastName" placeholder="Last Name" rules="required|max:100|min:3" :columns="{ xs: 12, sm: 6 }" />
      </GroupElement>
      <GroupElement name="email_username">
        <TextElement name="Email" label="Email" placeholder="user@example.com" input-type="email" rules="required|email"
          :addons="{
    before: `<i class='bi bi-envelope-at-fill'></i>`
  }" />
        <TextElement name="PhoneNumber" label="Phone nr." placeholder="123456789" input-type="tel"
          rules="regex:/^(?:[0-9]{9})?$/" :addons="{
    before: `<i class='bi bi-telephone-fill'></i>`
  }" />
        <TextElement name="UserName" placeholder="user_name" label="Username"
          rules="required|max:100|min:3|regex:/^[^@/\\]*$/" :addons="{
    before: `<i class='bi bi-person-fill'></i>`
  }" :messages="{
    regex: 'Username cannot contain the @, /, or \\ symbols'
  }" />
      </GroupElement>
      <GroupElement name="date">
        <DateElement name="DateOfBirth" label="Birth Date" placeholder="January 01, 2000" display-format="MMMM DD, YYYY"
          rules="required|before:today" :addons="{
    before: `<i class='bi bi-calendar3'></i>`
  }" />
      </GroupElement>
      <GroupElement name="password">
        <TextElement info="Password and Confirm password must match" name="Password" label="Password"
          placeholder="Password" input-type="password"
          rules="required|confirmed|min:6|regex:/^(?=.*[^\w\d])(?=.*\d)(?=.*[A-Z]).+$/" :messages="{
    regex: 'At least one character of type: alphanumeric, capital letter, number'
  }" :addons="{
    before: `<i class='bi bi-lock-fill'></i>`
  }" />
        <TextElement name="Password_confirmation" placeholder="Confirm password" input-type="password" rules="required"
          :addons="{
    before: `<i class='bi bi-lock-fill'></i>`
  }" />
      </GroupElement>
      <GroupElement name="controll">
        <ButtonElement id="reset_button" name="reset" button-label="<i class='bi bi-arrow-clockwise'></i>" type="reset"
          align="left" :danger="true" :resets="true" size="lg" :columns="{
    container: 3,
    wrapper: 3
  }" />
        <ButtonElement name="submit" button-label="Register" align="right" :submits="true" full size="lg" :columns="{
    container: 9,
    label: 9
  }" />
      </GroupElement>
    </Vueform>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import type { IRegisterModel } from '@/data/models/formDataModels';
import FormStatus from '@/components/shared/FormStatus.vue'
import { preventSubmit } from '@/data/service/api/account/sendDataService'

const isRegistering = ref(false)

const formData = ref<IRegisterModel>({
  Email: "",
  UserName: "",
  FirstName: "",
  LastName: "",
  PhoneNumber: "",
  DateOfBirth: "",
  Password: "",
  Password_confirmation: ""
});

const sendFormData = async () => {
  isRegistering.value = true;
  try {
    formData.value.DateOfBirth = new Date(formData.value.DateOfBirth).toISOString()
    await preventSubmit("/register", JSON.stringify(formData.value))
  } finally {
    formData.value.Password = ""
    formData.value.Password_confirmation = ""
    isRegistering.value = false;
  }
}
</script>
<style lang="scss" scoped>
.form.is-registering {
  opacity: 0.5;
  pointer-events: none;
}
</style>