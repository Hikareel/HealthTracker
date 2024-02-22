<template>
  <div class="register-main">
    <div class="register-form">
      <p class="registration-label">Registration</p><i class='bi bi-pencil'></i>
      <p v-for="msg in er" class="error_msg">
        {{ msg }}
      </p>
      <p class="registered">
        {{ isRegistered }}
      </p>
      <Vueform id="form" v-model="formData" @submit="preventSubmit" :float-placeholders="false" :endpoint="false" :display-errors="false" sync>
        <GroupElement name="name" before="Name">
          <TextElement :addons="{ before: { template: `<i class='bi bi-pencil'></i>` } }" name="FirstName" placeholder="First Name" rules="required|max:100|min:3" :columns="{ default: 6, sm: 6 }"/>

          <TextElement name="LastName" placeholder="Last Name" rules="required|max:100|min:3" :columns="{
            default: 6,
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
  max-width: 300px;
  color: #000000;
}
.registration-label{
  text-align: center;
  font-family: Arial, Helvetica, sans-serif;
  font-size: 26px;
  letter-spacing: 0px;
  word-spacing: 2px;
  color: #000000;
  font-weight: 700;
  text-decoration: none;
  font-style: normal;
  font-variant: small-caps;
  text-transform: none;
  text-shadow: 1px 0px 1px #CCCCCC;

}
.register-main{
  width:100%;
  height: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
}
.register-form{
  margin-top: 1rem;
  margin-bottom: 1rem;
  padding: 1rem 5rem 1rem 5rem;
  background-color:white;
  width: fit-content;
  border-radius: 50px;
  border-bottom: 5px groove #8b8b8b;
  border-top: 5px groove #8b8b8b;
  border-width: 10px;
  box-shadow: 0 4px 30px rgba(216, 199, 199, 0.7);
}
.error_msg {
  color: rgb(209, 0, 0);
  margin: auto;
  max-width: 300px;
}

.registered {
  color: rgb(101, 252, 0);
  margin: auto;
  max-width: 300px;
}
</style>