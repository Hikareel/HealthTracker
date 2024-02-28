<template>
    <div class="form">
        <p class="form-label">Login</p>
        <p v-for="error in formNotification.errors" class="error" v-bind:key="error">
            {{ error }}
        </p>
        <p class="success">
            {{ formNotification.success }}
        </p>
        <Vueform class="form-content" v-model="formData" @submit="sendFormData" :float-placeholders="false"
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
import type { LoginModel } from '@/data/models/formDataModels';
import { preventSubmit, formNotification, clearNotification } from '@/data/service/sendDataService';

clearNotification()

const formData = ref<LoginModel>({
    EmailUserName: '',
    Password: '',
});

const sendFormData = async () => {
    preventSubmit("/login", JSON.stringify(formData.value))
    if(formNotification.value.success === ""){
        formData.value.Password = ''
    }
}
</script>

<style lang="scss" scoped>
@use '@/assets/form';

.login-button {
    margin-top: 1rem;
}
</style>