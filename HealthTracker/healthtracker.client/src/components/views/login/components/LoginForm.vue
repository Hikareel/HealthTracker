<template>
    <div class="login-main">
        <div class="login-form">
            <p class="login-label">Login</p>
            <p v-for="msg in er" class="error_msg" v-bind:key="msg">
                {{ msg }}
            </p>
            <p class="logged">
                {{ isLogged }}
            </p>
            <Vueform id="form" v-model="formData" @submit="preventSubmit" :float-placeholders="false" :endpoint="false"
                :display-errors="false" sync>
                <GroupElement name="email_username">
                    <TextElement name="EmailUserName" label="Email or username" placeholder="user@example.com"
                        rules="required" :addons="{
                            before: `<i class='bi bi-pencil-fill'></i>`
                        }" />
                </GroupElement>
                <GroupElement name="password">
                    <TextElement name="Password" label="Password" placeholder="Password" input-type="password"
                        rules="required|min:6|regex:/^(?=.*[^\w\d])(?=.*\d)(?=.*[A-Z]).+$/" :addons="{
                            before: `<i class='bi bi-lock-fill'></i>`
                        }" />
                </GroupElement>
                <GroupElement name="controll">
                    <ButtonElement  class="ButtonElement-class" name="submit" button-label="Login" align="center" :submits="true" full size="lg" :columns="{container: 12, label: 0, wrapper: 12}" />
                </GroupElement>
                <GroupElement name="control2">
                    <ButtonElement button-label="Forgot password?"  :columns="{default:6 }" full size="sm" secondary/>
                    <ButtonElement href="/register" button-label="Register" :columns="{default:6 }" full size="sm" secondary/>
                </GroupElement>
            </Vueform>
        </div>
    </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import type { LoginModel } from '../data/loginDataModel';
import axios from 'axios';


const er = ref<string[]>([])
const isLogged = ref("")

const formData = ref<LoginModel>({
    EmailUserName: '',
    Password: '',
});

const preventSubmit = async () => {
    er.value.splice(0, er.value.length)
    isLogged.value = ''
    let response
    try {
        const { data } = await axios.post(
            '/api/login',
            JSON.stringify(formData.value),
            {
                headers: {
                    "Content-Type": "application/json",
                },
            }
        );
        response = data
        isLogged.value = response.message
    } catch (error: any) {
        formData.value.Password = ''
        error.response.data.forEach((element: { description: string; }) => {
            er.value.push(element.description)
        });
    }
}
</script>
    
<style>
#form {
    max-width: fit-content;
    color: #000000;
    margin-top: 1rem;
}

.ButtonElement-class{
    margin-top:1rem;
}

.login-label {
    text-align: center;
    font-family: Arial, Helvetica, sans-serif;
    font-size: 26px;
    color: #000000;
    font-weight: 800;
    font-variant: small-caps;
}

.login-main {
    display: flex;
    justify-content: center;
    align-items: center;
}

.login-form {
    padding: 1rem 5rem 2rem 5rem;
    background-color: rgb(182, 152, 152);
    border-radius: 1rem;
    box-shadow: 0 0 15px rgba(216, 199, 199, 0.7);

    @media (max-width: 550px) {
        padding: 1rem 2rem 1rem 2rem;
    }
}

.error_msg {
    color: rgb(209, 0, 0);
    margin: auto;
    max-width: 300px;
}

.logged {
    color: rgb(101, 252, 0);
    margin: auto;
    max-width: 300px;
}</style>