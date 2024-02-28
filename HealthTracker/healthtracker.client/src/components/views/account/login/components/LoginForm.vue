<template>
    <div class="form">
        <p class="form-label">Login</p>
        <p v-for="msg in er" class="error" v-bind:key="msg">
            {{ msg }}
        </p>
        <p class="success">
            {{ isLogged }}
        </p>
        <Vueform class="form-content" v-model="formData" @submit="preventSubmit" :float-placeholders="false"
            :endpoint="false" :display-errors="false" sync>
            <GroupElement name="email_username">
                <TextElement name="EmailUserName" label="Email or username" placeholder="user@example.com" rules="required"
                    :addons="{
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
                <ButtonElement id="reset_button" name="reset" type="reset" :resets="true" hidden />
                <ButtonElement class="login-button" name="submit" button-label="Login" align="center" :submits="true" full
                    size="lg" :columns="{ container: 12, label: 0, wrapper: 12 }" />
            </GroupElement>
            <GroupElement name="control2">
                <ButtonElement name="forgot" button-type="anchor" href="/login/pass-reset" button-label="Forgot password?"
                    :columns="{ default: 6 }" full size="sm" secondary />
                <ButtonElement name="register" href="/register" button-label="Register" :columns="{ default: 6 }" full
                    size="sm" secondary button-type="anchor" />
            </GroupElement>
        </Vueform>
    </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import type { LoginModel } from '@/data/models/loginDataModel';
import axios from 'axios';
import router from '../../../../../router'


const er = ref<string[]>([])
const isLogged = ref("")

const formData = ref<LoginModel>({
    EmailUserName: '',
    Password: '',
});

const preventSubmit = async () => {
    er.value.splice(0, er.value.length)
    isLogged.value = ''
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
        localStorage.setItem("token", data.token)
        isLogged.value = "User is logged"
        router.push('/').then(() =>{
            window.location.reload()
        });
    } catch (error: any) {
        formData.value.Password = ''
        error.response.data.forEach((element: { description: string; }) => {
            er.value.push(element.description)
        });
    }
}
</script>

<style lang="scss" scoped>
@use '@/assets/form';

.login-button {
    margin-top: 1rem;
}
</style>