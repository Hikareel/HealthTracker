<template>
  <div class="register-main">
    <div class="register-form">
      <p v-for="msg in er" class="error_msg">
        {{ msg }}
      </p>
      <p class="registered">
        {{ isRegistered }}
      </p>
      <Vueform id="form" v-model="formData" @submit="preventSubmit" :float-placeholders="false" :endpoint="false" :display-errors="false" sync>
        <GroupElement name="name" before="Name">
          <TextElement name="FirstName" placeholder="First Name" rules="required|max:100|min:3" :columns="{
            default: 12,
            sm: 6
          }"/>
          <TextElement name="LastName" placeholder="Last Name" rules="required|max:100|min:3" :columns="{
            default: 12,
            sm: 6
          }"/>
        </GroupElement>
        <GroupElement name="email_username">
            <TextElement name="Email" label="Email" placeholder="user@domain.com" 
                          input-type="email" rules="required|email"/>
            <TextElement name="PhoneNumber" label="Phone nr." placeholder="123456789" 
                            input-type="tel" rules="regex:/^(?:[0-9]{9})?$/"/>
            <TextElement name="UserName" 
                          label="Username" rules="required|max:100|min:3"/>
        </GroupElement>
          
        
        <GroupElement name="date">
          <DateElement name="DateOfBirth" label="Birth Date" 
                        display-format="MMMM DD, YYYY" rules="required|before:today"/>
        </GroupElement>
        <GroupElement name="password">
          <TextElement info="Password and Confirm password must match" name="Password" label="Password" placeholder="Password" input-type="password" 
                        rules="required|confirmed|min:6|regex:/^(?=.*[^\w\d])(?=.*\d)(?=.*[A-Z]).+$/"
                        :messages="{
                          regex: 'At least one character of type: alphanumeric, capital letter, number'
                        }"/>
          <TextElement name="Password_confirmation" placeholder="Confirm password" input-type="password" rules="required"/>
        </GroupElement>
        <GroupElement name="controll">
          <ButtonElement
            id="reset_button"
            name="reset" 
            button-label="Clear"
            type="reset"
            align="right"
            :danger="true"
            :resets="true"
            :columns="{
            default: 5,
            sm: 5
            }"
            />
          <ButtonElement
            name="submit" 
            button-label="Register"
            align="left"
            :submits="true"
            :columns="{
            default: 7,
            sm: 7
            }"
            />
        </GroupElement>
      </Vueform>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
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
  Password: '',
  Password_confirmation: ''
});

const preventSubmit = async () => {
  er.value.splice(0, er.value.length)
  isRegistered.value = ''
  formData.value.DateOfBirth = new Date(formData.value.DateOfBirth).toISOString()
  let response
  try {
    const { data } = await axios.post(
      '/api/register',
      JSON.stringify(formData.value),
      {
        headers: {
          "Content-Type": "application/json",
        },
      }
    );
    response = data
    isRegistered.value = response.message
    document.getElementById('reset_button')!.click()
  } catch (error: any) {
    formData.value.Password = ''
    formData.value.Password_confirmation = ''
    error.response.data.forEach((element: { description: string; }) => {
      er.value.push(element.description)
    });
  }
}
</script>

<style scoped>
#form {
  display: flex;
  flex-direction: column;
  max-width: 300px;
  margin: auto;
}
.register-main{
  margin-top: 2rem;
}
.register-form{
  display: block;
  margin: auto;
  background-color: white;
  max-width: calc(300px + 6rem);
  padding-top: 1rem;
  padding-bottom: 1rem;
}
.error_msg {
  color: rgb(231, 48, 48);
  margin: auto;
  max-width: 300px;
}

.registered {
  color: rgb(101, 252, 0);
  margin: auto;
  max-width: 300px;
}
</style>