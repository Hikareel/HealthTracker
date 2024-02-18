<template>
    <p v-for="msg in er" class="error_msg">
      {{ msg }}
    </p>
    <p class="registered">
      {{ isRegistered }}
    </p>
    <Vueform id="form" v-model="formData" :on-submit="preventSubmit" :display-errors="false" sync>
      <GroupElement name="name" before="Name">
        <TextElement v-model="formData.FirstName" name="FirstName" 
                      placeholder="First Name" rules="required|max:100"/>
        <TextElement v-model="formData.LastName" name="LastName" 
                      placeholder="Last Name" rules="required|max:100"/>
      </GroupElement>
      <GroupElement name="email_username">
        <TextElement v-model="formData.Email" name="Email" label="Email" placeholder="user@domain.com" 
                      input-type="email" rules="required|email"/>
        <TextElement v-model="formData.UserName" name="UserName" 
                      label="Username" rules="required|max:100"/>
      </GroupElement>
      <GroupElement name="dob_phone">
        <DateElement v-model="formData.DateOfBirth" name="DateOfBirth" label="Birth Date"
                      display-format="MMMM DD, YYYY" rules="required"/>
        <TextElement v-model="formData.PhoneNumber" name="PhoneNumber" label="Phone nr." 
                      placeholder="123456789" input-type="tel" rules='regex:/^(?:[0-9]{9})?$/'
                      />
      </GroupElement>
      <GroupElement name="password">
        <TextElement v-model="formData.password" name="password" label="Password" input-type="password" 
                      rules="required|confirmed|min:6|regex:/^(?=.*[^\w\d])(?=.*\d)(?=.*[A-Z]).+$/"
                      :messages="{
                        regex: 'At least one character of type: alphanumeric, capital letter, number'
                      }"/>
        <TextElement v-model="formData.password_confirmation" name="password_confirmation" 
                      label="Confirm Password" input-type="password" rules="required"/>
      </GroupElement>
      <GroupElement name="controll">
        <ButtonElement
          id="reset_button"
          name="reset" 
          button-label="Reset"
          :danger="true"
          :resets="true"
          align="center"/>
        <ButtonElement
          name="submit" 
          button-label="Register"
          :submits="true"
          align="center"/>
      </GroupElement>
    </Vueform>
</template>

<script setup lang="ts">
import { ref  } from 'vue';
import type { RegisterModel } from '../data/registerDataModel';
import axios from 'axios';

const er = ref<string[]>([])
const isRegistered = ref("")

const formData = ref<RegisterModel>({
  Email: '',
  UserName: '',
  FirstName: '',
  LastName: '',
  PhoneNumber: '',
  DateOfBirth: '',
  password: '',
  password_confirmation: ''
});

const preventSubmit = async () => {
  formData.value.DateOfBirth = new Date(formData.value.DateOfBirth).toISOString()
  let response
  try{
    const {data} = await axios.post(
      '/api/register',
      JSON.stringify(formData.value),
      {
        headers: {
          "Content-Type": "application/json",
        },
      }
    );
    response = data
  } catch (error: any){
    error.response.data.forEach((element: { description: string; }) => {
      er.value.push(element.description)
    });
  }
  if(response.message == "User registered successfully"){
    isRegistered.value = "User registered successfully"
    er.value.splice(0, er.value.length)
    document.getElementById('reset_button')!.click()
  }
}
</script>

<style scoped>
  #form{
    display: flex;
    flex-direction: column;
    max-width: 300px;
    margin: auto;
  }
  .error_msg{
    color: rgb(231, 48, 48);
    margin: auto;
    max-width: 300px;
  }
  .registered{
    color: rgb(101, 252, 0);
    margin: auto;
    max-width: 300px;
  }
</style>