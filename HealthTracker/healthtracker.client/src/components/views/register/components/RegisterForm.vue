<template>
  <div class="register-main">
    <div class="register-form">
      <p class="registration-label">Registration</p>
      <p v-for="msg in er" class="error_msg" v-bind:key="msg">
        {{ msg }}
      </p>
      <p class="registered">
        {{ isRegistered }}
      </p>
      <Vueform id="form" v-model="formData" @submit="preventSubmit" :float-placeholders="false" :endpoint="false" :display-errors="false" sync>
        <GroupElement name="name" before="Name" :add-class="'name_el'">
          <TextElement :addons="{ 
              before: `<i class='bi bi-pencil-fill'></i>`
            }" 
          name="FirstName" placeholder="First Name" rules="required|max:100|min:3" :columns="{ xs: 12, sm: 6 }"/>
          <TextElement :addons="{ 
              before: `<i class='bi bi-pencil-fill'></i>`
            }" 
            name="LastName" placeholder="Last Name" rules="required|max:100|min:3" :columns="{ xs: 12, sm: 6 }"/>
        </GroupElement>
        <GroupElement name="email_username">
            <TextElement name="Email" label="Email" placeholder="user@example.com" 
                          input-type="email" rules="required|email"
                          :addons="{ 
                            before: `<i class='bi bi-envelope-at-fill'></i>`
                          }"/>
            <TextElement name="PhoneNumber" label="Phone nr." placeholder="123456789" 
                            input-type="tel" rules="regex:/^(?:[0-9]{9})?$/"
                            :addons="{ 
                              before: `<i class='bi bi-telephone-fill'></i>`
                            }"/>
            <TextElement name="UserName" placeholder="user_name"
                          label="Username" rules="required|max:100|min:3"
                          :addons="{ 
                            before: `<i class='bi bi-person-fill'></i>`
                          }"/>
        </GroupElement>
          
        
        <GroupElement name="date">
          <DateElement name="DateOfBirth" label="Birth Date" placeholder="January 01, 2000"
                        display-format="MMMM DD, YYYY" rules="required|before:today"
                        :addons="{ 
                          before: `<i class='bi bi-calendar3'></i>`
                        }"/>
        </GroupElement>
        <GroupElement name="password">
          <TextElement info="Password and Confirm password must match" name="Password" label="Password" placeholder="Password" input-type="password" 
                        rules="required|confirmed|min:6|regex:/^(?=.*[^\w\d])(?=.*\d)(?=.*[A-Z]).+$/"
                        :messages="{
                          regex: 'At least one character of type: alphanumeric, capital letter, number'
                        }"
                        :addons="{ 
                          before: `<i class='bi bi-lock-fill'></i>`
                        }" />
          <TextElement name="Password_confirmation" placeholder="Confirm password" input-type="password" rules="required"
                        :addons="{ 
                          before: `<i class='bi bi-lock-fill'></i>`
                        }"/>
        </GroupElement>
        <GroupElement name="controll">
          <ButtonElement
            id="reset_button"
            name="reset" 
            button-label="<i class='bi bi-arrow-clockwise'></i>"
            type="reset"
            align="left"
            :danger="true"
            :resets="true"
            :columns="{
              container: 1,
              wrapper: 1
            }"
            />
          <ButtonElement
            name="submit" 
            button-label="Register"
            align="right"
            :submits="true"
            :columns="{
              container: 11,
              label: 10
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
  margin-top: 1rem;
}
.registration-label{
  text-align: center;
  font-family: Arial, Helvetica, sans-serif;
  font-size: 26px;
  color: #000000;
  font-weight: 800;
  font-variant: small-caps;
}
.register-form{
  margin-top: 1rem;
  margin-bottom: 1rem;
  padding: 1rem 5rem 1rem 5rem;
  background-color:darkgrey;
  width: fit-content;
  border-radius: 50px;
  border: 2px solid #8b8b8b;
  box-shadow: 0 4px 30px rgba(216, 199, 199, 0.7);
  @media (max-width: 550px) {
    padding: 1rem 2rem 1rem 2rem;
  }
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

.name_el{
  @media (max-width: 550px) {
    display: flex;
    flex-direction: column;
  }
}
</style>